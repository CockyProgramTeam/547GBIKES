using System;
using System.Collections.Generic;

namespace dirtbike.api.Models;

public partial class ParkReview
{
    public int Id { get; set; }

    public int ParkId { get; set; }

    public int UserId { get; set; }

    public string Description { get; set; } = null!;

    public int Stars { get; set; }

    public string DatePosted { get; set; } = null!;

    public string? DateApproved { get; set; }

    public string? DateDenied { get; set; }

    public string? ReasonDescription { get; set; }

    public int? ReviewManagerId { get; set; }

    public string? Useridasstring { get; set; }

    public string? Displayname { get; set; }

    public string? Fullname { get; set; }

    public bool? Active { get; set; }
}
