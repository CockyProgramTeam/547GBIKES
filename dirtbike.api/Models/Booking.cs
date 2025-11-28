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

    public string? Cartid { get; set; }

    public string? Reservationtype { get; set; }

    public string? Reservationstatus { get; set; }

    public string? Reversetransactionid { get; set; }

    public double? Cancellationrefund { get; set; }

    public string? CartDetailsJson { get; set; }

    public int? Totalcartitems { get; set; }

    public string? Reference { get; set; }

    public string? SubReference { get; set; }

    public int? Adults { get; set; }

    public int? Children { get; set; }

    public DateTime? ResStart { get; set; }

    public DateTime? ResEnd { get; set; }

    public int? Tentsites { get; set; }
}
