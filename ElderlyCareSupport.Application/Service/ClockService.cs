using ElderlyCareSupport.Application.IService;

namespace ElderlyCareSupport.Application.Service
{
    public class ClockService : IClock
    {
        public DateTime Now => DateTime.Now;
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
