using System;
using System.Collections.Generic;

namespace ElderlyCareSupport.Server.Models;

public partial class FeeConfiguration
{
    public decimal FeeId { get; init; }

    public string FeeName { get; init; } = null!;

    public decimal FeeAmount { get; init; }
}
