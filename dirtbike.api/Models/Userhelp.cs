using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Userhelp
{
    public int Id { get; set; }

    public string? Ticketid { get; set; }

    public int? Emplid { get; set; }

    public string? Descr { get; set; }

    public int? Severity { get; set; }

    public int? Userid { get; set; }

    public string? Email { get; set; }

    public string? Fullname { get; set; }

    public string? Bestcontactnumber { get; set; }

    public string? Replied { get; set; }

    public string? Repliedmanagerid { get; set; }

    public string? Repliedmanagerphone { get; set; }

    public string? Repliedmanageremail { get; set; }

    public DateTime? Ticketdate { get; set; }

    public DateTime? Responsedate { get; set; }
}
