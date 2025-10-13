using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public string Uid { get; set; } = null!;

    public string? BillingTelephoneNumber { get; set; }

    public string? CreditCardType { get; set; }

    public string? CreditCardLast4 { get; set; }

    public string? CreditCardExpDate { get; set; }

    public int? QuantityAdults { get; set; }

    public int? QuantityChildren { get; set; }

    public string? CustomerBillingName { get; set; }

    public double? TotalAmount { get; set; }

    public string? TransactionId { get; set; }

    public int? ParkId { get; set; }

    public string? ParkName { get; set; }
}
