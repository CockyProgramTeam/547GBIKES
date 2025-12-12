using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string Uid { get; set; } = null!;

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? BillingAddress { get; set; }

    public string Login { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? UserStatus { get; set; }
}
