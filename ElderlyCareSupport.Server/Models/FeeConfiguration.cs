using System;
using System.Collections.Generic;

namespace ElderlyCareSupport.Server.Models;

public partial class FeeConfiguration
{
    public decimal FeeId { get; set; }

    public string FeeName { get; set; } = null!;

    public decimal FeeAmount { get; set; }

    public byte[] RowVersion { get; set; } = null!;

    public string? Description { get; set; }
}
