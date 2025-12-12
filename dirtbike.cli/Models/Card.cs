using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Card
{
    public int CardId { get; set; }

    public string Uid { get; set; } = null!;

    public string? CardType { get; set; }

    public string? CardVendor { get; set; }

    public string? CardLast4 { get; set; }

    public string? CardExpDate { get; set; }

    public string? BillingZip { get; set; }

    public int? IsActive { get; set; }

    public string? Cardbtn { get; set; }

    public string? Fullname { get; set; }
}
