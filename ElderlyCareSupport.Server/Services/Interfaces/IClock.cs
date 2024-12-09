namespace ElderlyCareSupport.Server.Services.Interfaces
{
    public interface IClock
    {
        DateTime Now { get; }
        DateTime NowUtc { get; }
    }
}
