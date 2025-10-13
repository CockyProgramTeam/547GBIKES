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

    public string? StartDate { get; set; }

    public string? EndDate { get; set; }

    public int? IsActive { get; set; }
}
