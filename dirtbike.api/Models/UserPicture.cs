using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class UserPicture
{
    public int Id { get; set; }

    public int? Userid { get; set; }

    public string? Useridstring { get; set; }

    public string? Activepictureurl { get; set; }

    public byte[]? Somepicture { get; set; }
}
