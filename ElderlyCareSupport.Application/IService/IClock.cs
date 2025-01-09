namespace ElderlyCareSupport.Application.IService
{
    public interface IClock
    {
        DateTime Now { get; }
        DateTime NowUtc { get; }
    }
}
