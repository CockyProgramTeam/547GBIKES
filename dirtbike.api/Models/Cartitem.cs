using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Cartitem
{
    public int Id { get; set; }

    public int? Cartid { get; set; }

    public DateTime? Cartitemdate { get; set; }

    public string? Itemvendor { get; set; }

    public string? Itemdescription { get; set; }

    public double? Itemextendedprice { get; set; }

    public int? Itemqty { get; set; }

    public double? Itemtotals { get; set; }

    public int? Salescatid { get; set; }

    public string? Productid { get; set; }
}
