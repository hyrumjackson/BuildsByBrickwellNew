using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuildsByBrickwellNew.Models;

public partial class LineItem
{
    [Key]
    public int? TransactionId { get; set; }

    public int? ProductId { get; set; }

    public int? Qty { get; set; }

    public int? Rating { get; set; }
}
