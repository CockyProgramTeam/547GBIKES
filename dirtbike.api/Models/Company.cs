using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Company
{
    public int Id { get; set; }

    public string Companyname { get; set; } = null!;

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Postalzip { get; set; }

    public string? Tfnumber { get; set; }

    public string? Localnumber { get; set; }

    public string? Emailfrontdesk { get; set; }

    public string? Emailsupport { get; set; }

    public string? Faxhr { get; set; }

    public string? Faxsecurity { get; set; }

    public string? Faxcorp { get; set; }

    public string? Dynamicsid { get; set; }

    public string? Oracleid { get; set; }

    public string? Alohaid { get; set; }
}
