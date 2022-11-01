namespace Rtl.TvMazeScrapper.Domain.Interfaces.Cache;

public interface ICacheableMediatrQuery
{
    bool BypassCache { get; }
    string CacheKey { get; }
    TimeSpan? SlidingExpiration { get; }
}