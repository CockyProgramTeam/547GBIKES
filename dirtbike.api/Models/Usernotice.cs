using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Usernotice
{
    public int Id { get; set; }

    public string Userid { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? NoticeDatetime { get; set; }
}
