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

    public string? Description { get; set; }

    public double? DayPassPriceUsd { get; set; }

    public decimal? Longitude { get; set; }

    public decimal? Latitude { get; set; }

    public string? Trailmapurl { get; set; }

    public string? Parklogourl { get; set; }

    public int? Maxvisitors { get; set; }

    public int? Currentvisitors { get; set; }

    public int? Currentvisitorschildren { get; set; }

    public int? Currentvisitorsadults { get; set; }

    public int? Maxcampsites { get; set; }

    public int? Columns { get; set; }

    public string? State { get; set; }
}
