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

    public int? IsActive { get; set; }

    public string? SiteId { get; set; }

    public int? State { get; set; }

    public int? Global { get; set; }

    public int? Qtyadults { get; set; }

    public int? Qtychildren { get; set; }

    public int? National { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}
