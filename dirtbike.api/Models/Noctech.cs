using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Noctech
{
    public int Id { get; set; }

    public string Userid { get; set; } = null!;

    public string? Employeeid { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public int? Sms { get; set; }

    public string? Techaddress1 { get; set; }

    public string? Techaddress2 { get; set; }

    public string? Techcity { get; set; }

    public string? Techstate { get; set; }

    public string? Techzip { get; set; }

    public string? Fullname { get; set; }
}
