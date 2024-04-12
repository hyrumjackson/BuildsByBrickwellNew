using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuildsByBrickwellNew.Models;

public class High_rated_rec
{
    [Key]
    public int? ProductId { get; set; }

    public float? Rating { get; set; }

    public int? Qty { get; set; }

    public string? Name { get; set; }

    public string? ImgLink { get; set; }
}
