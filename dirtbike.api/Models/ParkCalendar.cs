using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class ParkCalendar
{
    public int Id { get; set; }

    public string ParkId { get; set; } = null!;

    public int CustomerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? TransactionId { get; set; }

    public string? BookId { get; set; }

    public int? QtyAdults { get; set; }

    public int? QtyChildren { get; set; }

    public string? ParkGuid { get; set; }
}
