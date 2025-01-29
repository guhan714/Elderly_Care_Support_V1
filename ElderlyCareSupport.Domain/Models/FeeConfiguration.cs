namespace ElderlyCareSupport.Domain.Models;

public class FeeConfiguration
{
    public decimal FeeId { get; set; }

    public string FeeName { get; set; } = null!;

    public decimal FeeAmount { get; set; }
    
    public string? Description { get; set; }
}
