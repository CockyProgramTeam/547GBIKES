using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Linq;

namespace PARKAPITESTER
{
public class ParkApiClient
{
    private readonly HttpClient _httpClient;

    public ParkApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://parksapi.547bikes.info/");
    }

    public static async Task<List<ParkDto>> GetFirst10ParksAsync()
    {
        using var client = new HttpClient { BaseAddress = new Uri("https://parksapi.547bikes.info/") };
        var parks = await client.GetFromJsonAsync<List<ParkDto>>("api/Parks");
        return parks?.Take(10).ToList() ?? new List<ParkDto>();
    }
}

public class ParkDto
{
    public int ParkId { get; set; }
    public string? name { get; set; }
    public string? address { get; set; }
}}