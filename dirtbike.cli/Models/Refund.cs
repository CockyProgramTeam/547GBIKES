using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Refund
{
    public int RefundId { get; set; }

    public int BookingId { get; set; }

    public string? PaymentMethod { get; set; }

    public string? CardType { get; set; }

    public string? CardLast4 { get; set; }

    public string? CardExpDate { get; set; }

    public double AmountPaid { get; set; }

    public string? PaymentDate { get; set; }

    public string? TransactionId { get; set; }

    public string? Useridasstring { get; set; }

    public string? Transtype { get; set; }

    public string? Fullname { get; set; }

    public string? ParkName { get; set; }

    public string? State { get; set; }

    public int? ParkId { get; set; }

    public double? AmountRefunded { get; set; }

    public string? RefundTransactionId { get; set; }
}
