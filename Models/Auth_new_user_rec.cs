using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuildsByBrickwellNew.Models;

public class Auth_new_user_rec
{
    [Key]
    public byte? ProductId { get; set; }

    public string? ProductName { get; set; }

    public string? ImgLink { get; set; }
}
