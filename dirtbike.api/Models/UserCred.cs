using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public class UserCred
{
    public int Id { get; set; }

    public int UserId { get; set; }  // Foreign key linking to `User` table

    public required string EncryptedPassword { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
