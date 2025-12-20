using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Usernotice
{
    public int Id { get; set; }

    public int Userid { get; set; } = 0;

    public string? Useridstring { get; set; } = null!;

    public string? Description { get; set; }

    public string? Noticetype { get; set; }

    public string? Emailgwtype { get; set; }

    public DateTime? NoticeDatetime { get; set; }

    public int? uid { get; set; }
}
