/*using System;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Enterprise.Models;

namespace somecontrollers.Controllers
{
    public static class ImageEndpoints
    {
        public static void MapImageEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/ImageGen").WithTags("ImageGen");

            group.MapPost("/generate", async ([FromBody] ImageQuery input, CeContext context, HttpClient client) =>
            {
                var endpointBase = "https://strit-mg412w3h-swedencentral.openai.azure.com";
                var deployment = "dall-e-3";
                var apiKey = "BUrJOwQrvIKPYPQ4UAuyMFibZBXryClbpBgxCKZNWhEP16grMQIYJQQJ99BIACfhMk5XJ3w3AAAAACOGdkz5";
                var apiVersion = "2024-02-01";

                var url = $"{endpointBase}/openai/deployments/{deployment}/images/generations?api-version={apiVersion}";

                var payload = new
                {
                    model = "dall-e-3",
                    prompt = input.Prompt,
                    n = 1,
                    size = "1024x1024",
                    style = "vivid",
                    quality = "standard"
                };

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("api-key", apiKey);

                string imageUrl = "error";

                try
                {
                    var response = await client.PostAsync(url,
                        new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        var resultJson = await response.Content.ReadAsStringAsync();
                        var result = JsonDocument.Parse(resultJson);

                        if (result.RootElement.TryGetProperty("data", out var data) && data.GetArrayLength() > 0)
                        {
                            imageUrl = data[0].GetProperty("url").GetString() ?? "error";
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Optional: log error
                    Console.WriteLine($"Image generation failed: {ex.Message}");
                }

                input.ImageUrl = imageUrl;
                input.Timestamp = DateTime.UtcNow;

                context.ImageQueries.Add(input);
                await context.SaveChangesAsync();

                return Results.Ok(input);
            })
            .WithName("GenerateImage")
            .WithOpenApi();
        }
    }
}*/
