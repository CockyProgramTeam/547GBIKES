using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class SalesSession
{
    public int SalesSessionId { get; set; }

    public string Uid { get; set; } = null!;

    public string SessionStart { get; set; } = null!;

    public string? SessionEnd { get; set; }

    public int? CartId1 { get; set; }

    public int? CartId2 { get; set; }

    public int? CartId3 { get; set; }

    public int? CartId4 { get; set; }

    public int? CartId5 { get; set; }
}
