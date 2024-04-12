using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuildsByBrickwellNew.Models;

public class Item_based_rec
{
    [Key]
    public byte? ProductId { get; set; }

    public byte? RecommendedProductId1 { get; set; }

    public byte? RecommendedProductId2 { get; set; }

    public byte? RecommendedProductId3 { get; set; }

    public byte? RecommendedProductId4 { get; set; }

    public byte? RecommendedProductId5 { get; set; }
}
