using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Park
{
    public int ParkId { get; set; }

    public string? Name { get; set; }

    public string? Address { get; set; }

    public string? Phone { get; set; }

    public string? Region { get; set; }

    public double? TrailLengthMiles { get; set; }

    public string? Difficulty { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public string? Description { get; set; }

    public double? DayPassPriceUsd { get; set; }
}
