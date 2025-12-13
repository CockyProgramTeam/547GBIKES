using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Userlog
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public int? Hashid { get; set; }

    public string? Hashedpassword { get; set; }

    public string? Loginstatus { get; set; }

    public string? Description { get; set; }

    public string? Uiorigin { get; set; }
}
