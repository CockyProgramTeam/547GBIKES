using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int? CartId { get; set; }

    public string? Uid { get; set; }

    public int? ParkId { get; set; }

    public string? ItemType { get; set; }

    public string? ItemDescription { get; set; }

    public int? Quantity { get; set; }

    public double? UnitPrice { get; set; }

    public double? TotalPrice { get; set; }

    public DateOnly? DateAdded { get; set; }

    public int? IsCheckedOut { get; set; }

    public string? Paymentid { get; set; }

    public string? Bookinginfo { get; set; }

    public int? Totalcartitems { get; set; }

    public int? Multipleitems { get; set; }

    public double? Johnstotals { get; set; }

    public double? Transactiontotal { get; set; }

    public string? Parkname { get; set; }

    public DateTime? ResStart { get; set; }

    public DateTime? ResEnd { get; set; }

    public int? Adults { get; set; }

    public int? Children { get; set; }

    public int? Tentsites { get; set; }
}
