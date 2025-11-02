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

    public string? Shopid { get; set; }

    public string? Parkid { get; set; }

    public double? Subtotal { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ResStart { get; set; }

    public DateTime? ResEnd { get; set; }

    public string? Qrcodeurl { get; set; }

    public string? Reservationcode { get; set; }

    public string? Memberid { get; set; }

    public string? Rewardsprovider { get; set; }

    public byte[]? Parkname { get; set; }
}
