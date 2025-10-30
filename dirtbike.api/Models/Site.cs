using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Site
{
    public int Id { get; set; }

    public string? Branchid { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Postalzip { get; set; }

    public string? Fax { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Region { get; set; }

    public string? Instanceid { get; set; }

    public string? Parkid { get; set; }
}
