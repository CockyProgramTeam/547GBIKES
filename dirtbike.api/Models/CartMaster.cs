using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class CartMaster
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? CartsCount { get; set; }

    public int? CartsCancelled { get; set; }

    public int? CartsActive { get; set; }

    public string? CartsActiveList { get; set; }

    public string? Loyaltyid { get; set; }

    public string? Loyaltyvendor { get; set; }

    public string? Useridstring { get; set; }
}
