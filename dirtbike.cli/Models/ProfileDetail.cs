using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class ProfileDetail
{
    public int Id { get; set; }

    public string? picturefilepath { get; set; }

    public string? profileurl { get; set; }
}
