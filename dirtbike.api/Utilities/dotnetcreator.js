dotnet new console -n ParkConverter
cd ParkConverter
2. ✅ Replace Program.cs with this code:
csharp
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text.Json;

class Park
{
    public string Name { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string Bicycling { get; set; }
    public string Camping { get; set; }
    public string DayPassCost { get; set; }
    public string WeekPassCost { get; set; }
    public bool IsNationalPark { get; set; }
    public bool IsStatePark { get; set; }
}

class Program
{
    static void Main()
    {
        string csvPath = "parks.csv";
        string jsonPath = "parks.json";

        var lines = File.ReadAllLines(csvPath);
        var parks = new List<Park>();

        for (int i = 1; i < lines.Length; i++)
        {
            var fields = lines[i].Split(',');

            var park = new Park
            {
                Name = fields[0],
                Address = fields[1],
                City = fields[3],
                State = fields[4],
                Zip = fields[5],
                Latitude = double.Parse(fields[6], CultureInfo.InvariantCulture),
                Longitude = double.Parse(fields[7], CultureInfo.InvariantCulture),
                Bicycling = fields[8],
                Camping = fields[9],
                DayPassCost = fields[10],
                WeekPassCost = fields[11],
                IsNationalPark = fields[0].Contains("National Park"),
                IsStatePark = fields[0].Contains("State Park")
            };

            parks.Add(park);
        }

        var json = JsonSerializer.Serialize(parks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(jsonPath, json);

        Console.WriteLine($"parks.json created with {parks.Count} entries.");
    }
}
3. ✅ Add your CSV file
Place your parks.csv file in the same directory as the .csproj file.

4. ✅ Run the program
bash
dotnet run