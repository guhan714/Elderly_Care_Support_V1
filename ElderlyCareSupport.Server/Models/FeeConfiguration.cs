using System;
using System.Collections.Generic;

namespace ElderlyCareSupport.Server.Models;

public partial class FeeConfiguration
{
    public decimal FeeId { get; set; }

    public string FeeName { get; set; } = null!;

    public decimal FeeAmount { get; set; }
}
