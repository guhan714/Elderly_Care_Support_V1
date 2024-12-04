namespace ElderlyCareSupport.Server.DTOs
{
    public class FeeConfigurationDTO
    {
        public decimal FeeId { get; set; }
        public string FeeName { get; set; } = null!;
        public decimal FeeAmount { get; set; }
    }
}