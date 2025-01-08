namespace ElderlyCareSupport.Server.DTOs
{
    public class FeeConfigurationDto
    {
        public decimal FeeId { get; init; }
        public string FeeName { get; init; } = null!;
        public decimal FeeAmount { get; init; }
        public string? Description { get; set; }
    }
}