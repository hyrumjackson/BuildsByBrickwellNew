using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuildsByBrickwellNew.Models;

public class Item_based_rec
{
    [Key]
    public int? ProductId { get; set; }

    public int? RecommendedProductId1 { get; set; }

    public int? RecommendedProductId2 { get; set; }

    public int? RecommendedProductId3 { get; set; }

    public int? RecommendedProductId4 { get; set; }

    public int? RecommendedProductId5 { get; set; }
}
