using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public string Uid { get; set; } = null!;

    public int ParkId { get; set; }

    public string? ItemType { get; set; }

    public string? ItemDescription { get; set; }

    public int Quantity { get; set; }

    public double UnitPrice { get; set; }

    public double? TotalPrice { get; set; }

    public string? DateAdded { get; set; }

    public int? IsCheckedOut { get; set; }

    public string? Paymentid { get; set; }

    public string? Bookinginfo { get; set; }
}
