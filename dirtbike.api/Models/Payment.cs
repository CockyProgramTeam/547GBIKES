using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int BookingId { get; set; }

    public string? PaymentMethod { get; set; }

    public string? CardType { get; set; }

    public string? CardLast4 { get; set; }

    public string? CardExpDate { get; set; }

    public double? AmountPaid { get; set; }

    public string? PaymentDate { get; set; }

    public string? TransactionId { get; set; }

    public string? Useridasstring { get; set; }

    public string? Transtype { get; set; }
}
