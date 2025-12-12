using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class TaxtableState
{
    public int Id { get; set; }

    public string State { get; set; } = null!;

    public double? CorporateTaxRate { get; set; }

    public double? IndividualTaxRate { get; set; }
}
