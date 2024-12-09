using ElderlyCareSupport.Server.Services.Interfaces;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class ClockService : IClock
    {
       public DateTime Now => DateTime.Now;
       public DateTime NowUtc => DateTime.UtcNow;
    }
}
