using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class ParkCalendarDay
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public int? Month { get; set; }

    public int? Day { get; set; }

    public int? Year { get; set; }

    public int? Adults { get; set; }

    public int? Children { get; set; }

    public int? Parkid { get; set; }

    public string? Parkguid { get; set; }
}
