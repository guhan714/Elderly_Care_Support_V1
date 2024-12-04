using ElderlyCareSupport.Server.Services.Interfaces;

namespace ElderlyCareSupport.Server.Services.Implementations
{
    public class ClockService : IClock
    {
        public DateTime GetDateTime()
        {
            return DateTime.UtcNow;
        }
    }
}
