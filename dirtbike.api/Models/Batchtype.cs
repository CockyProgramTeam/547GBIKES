using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Batchtype
{
    public int Id { get; set; }

    public string Batchtypename { get; set; } = null!;

    public string? Region { get; set; }

    public string? Instance { get; set; }
}
