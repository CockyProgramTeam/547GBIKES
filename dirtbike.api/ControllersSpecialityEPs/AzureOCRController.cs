using Azure;
using Azure.AI.FormRecognizer.DocumentAnalysis;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class OcrController : ControllerBase
{
    private readonly DocumentAnalysisClient _ocr;
    private readonly HttpClient _http;

    public OcrController(DocumentAnalysisClient ocr, IHttpClientFactory httpFactory)
    {
        _ocr  = ocr;
        _http = httpFactory.CreateClient();
    }

    [HttpGet("extract")]
    public async Task<IActionResult> Extract([FromQuery] string blobUrl)
    {
        if (string.IsNullOrWhiteSpace(blobUrl))
            return BadRequest("Missing blobUrl");

        try
        {
            // 1) OCR
            var result = await _ocr.AnalyzeDocumentFromUriAsync(
                WaitUntil.Completed,
                "prebuilt-read",
                new Uri(blobUrl)
            );

            var allLines = result.Value.Pages
                .SelectMany(p => p.Lines)
                .Select(l => (l.Content ?? "").Trim())
                .Where(l => !string.IsNullOrEmpty(l))
                .ToList();

            var allText = string.Join(" ", allLines);

            // 2) certName cleanup
            string certName = allLines
                .FirstOrDefault(l => l.Contains("certified", StringComparison.OrdinalIgnoreCase))
                ?? allLines.FirstOrDefault() ?? "";
            certName = Regex.Replace(certName, @"[\r\n]+", " ").Trim();
            string keyName = Regex
                .Replace(certName, @"^.*?:\s*", "", RegexOptions.IgnoreCase)
                .Trim();

            // 3) certHolderName flip
            string certHolderName = "";
            var marker = allLines
                .FirstOrDefault(l => l.Contains("has successfully passed all requirements", StringComparison.OrdinalIgnoreCase));
            if (marker != null)
            {
                int idx = allLines.IndexOf(marker);
                if (idx > 0)
                {
                    certHolderName = Regex.Replace(allLines[idx - 1], @"[\r\n]+", " ").Trim();
                    var parts = certHolderName.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2)
                        certHolderName = $"{parts[1].Trim()} {parts[0].Trim()}";
                }
            }

            // 4) dates — scan line-by-line with multiple keywords
            string issueDate  = "";
            string expiryDate = "";

            var issueRegex = new Regex(
                @"(?i)^(?:earned on|earned:)[:\s]*(\w+\s+\d{1,2},\s+\d{4})",
                RegexOptions.Compiled);
            var expiryRegex = new Regex(
                @"(?i)^(?:expires on|expired on|expiration on|expiration date)[:\s]*(\w+\s+\d{1,2},\s+\d{4})",
                RegexOptions.Compiled);

            foreach (var line in allLines)
            {
                if (issueDate == null)
                {
                    var m = issueRegex.Match(line);
                    if (m.Success && DateTime.TryParse(m.Groups[1].Value, out var dtI))
                        issueDate = dtI.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                }

                if (expiryDate == null)
                {
                    var m = expiryRegex.Match(line);
                    if (m.Success && DateTime.TryParse(m.Groups[1].Value, out var dtE))
                        expiryDate = dtE.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                }

                if (issueDate != null && expiryDate != null)
                    break;
            }

            // 5) Fetch catalogue
            var catalogueUrl = "https://cockyapiv3-bugudue8akcsbacz.westus3-01.azurewebsites.net/api/Certcalogue";
            var catalog = await _http.GetFromJsonAsync<List<CertCatalogueItem>>(catalogueUrl);

            // 6) Matching
            string matchedCert = "";
            if (catalog != null && catalog.Any())
            {
                var cleaned = catalog.Select(c =>
                {
                    var desc = c.Description.Trim() ?? "";
                    var noPrefix = Regex.Replace(desc,
                        @"^Microsoft Certified:\s*", "",
                        RegexOptions.IgnoreCase);
                    var noSuffix = Regex.Replace(noPrefix,
                        @"\s*–\s*(Professional|Associate|Specialty).*",
                        "", RegexOptions.IgnoreCase);
                    var baseName = Regex.Replace(noSuffix,
                        @"\s*\(.*?\)", "").Trim();
                    return new { Original = desc, Base = baseName };
                }).ToList();

                var direct = cleaned.FirstOrDefault(x =>
                    x.Base.Equals(keyName, StringComparison.OrdinalIgnoreCase) ||
                    x.Base.IndexOf(keyName, StringComparison.OrdinalIgnoreCase) >= 0);

                if (direct != null)
                {
                    matchedCert = direct.Original;
                }
                else
                {
                    var best = cleaned
                        .Select(x => new {
                            x.Original,
                            x.Base,
                            Score = Similarity(keyName.ToLower(), x.Base.ToLower())
                        })
                        .OrderByDescending(x => x.Score)
                        .First();

                    if (best.Score > 0.5)
                        matchedCert = best.Original;
                }
            }

            return Ok(new {
                certHolderName,
                certName,
                issueDate,
                expiryDate,
                matchedCert
            });
        }
        catch (RequestFailedException)  { throw; }
        catch (UriFormatException)       { throw; }
        catch (Exception )             
        { 
            throw; 
        }
    }

    private static double Similarity(string a, string b)
    {
        if (string.IsNullOrEmpty(a) || string.IsNullOrEmpty(b))
            return 0;
        int dist = Levenshtein(a, b);
        return 1.0 - (double)dist / Math.Max(a.Length, b.Length);
    }

    private static int Levenshtein(string s, string t)
    {
        int n = s.Length, m = t.Length;
        var d = new int[n + 1, m + 1];
        if (n == 0) return m;
        if (m == 0) return n;
        for (int i = 0; i <= n; i++) d[i, 0] = i;
        for (int j = 0; j <= m; j++) d[0, j] = j;
        for (int i = 1; i <= n; i++)
            for (int j = 1; j <= m; j++)
            {
                int cost = s[i - 1] == t[j - 1] ? 0 : 1;
                d[i, j] = Math.Min(
                    Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                    d[i - 1, j - 1] + cost
                );
            }
        return d[n, m];
    }

    private class CertCatalogueItem
    {
        public int    Id          { get; set; }
        public required string Description { get; set; }
    }
}
