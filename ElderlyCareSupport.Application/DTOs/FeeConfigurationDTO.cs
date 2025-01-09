namespace ElderlyCareSupport.Application.DTOs
{
    public class FeeConfigurationDto
    {
        public decimal FeeId { get; init; }
        public string FeeName { get; init; } = null!;
        public decimal FeeAmount { get; init; }
        public string? Description { get; set; }
    }
}