using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Batch
{
    public int Id { get; set; }

    public string Batchname { get; set; } = null!;

    public string Filelocationpath { get; set; } = null!;

    public int? Batchtype { get; set; }

    public int? Batchstatus { get; set; }

    public DateTime? Batchstart { get; set; }

    public DateTime? Batchend { get; set; }

    public int? Qtystart { get; set; }

    public int? Qtyend { get; set; }

    public int? Qtyexpected { get; set; }

    public int? Qtyactual { get; set; }

    public int? Qtyerror { get; set; }

    public int? Qty { get; set; }
}
