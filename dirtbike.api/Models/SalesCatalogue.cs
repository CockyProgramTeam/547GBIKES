using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class SalesCatalogue
{
    public int SalesCatalogueId { get; set; }

    public int ParkId { get; set; }

    public string? ServiceType { get; set; }

    public string? ServiceName { get; set; }

    public string? Description { get; set; }

    public double Price { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public int? IsActive { get; set; }

    public string? SiteId { get; set; }

    public byte[]? National { get; set; }

    public int? State { get; set; }

    public int? Global { get; set; }
}
