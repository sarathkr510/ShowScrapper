using System.Net;
using Polly;

namespace Rtl.TvMazeScrapper.Infrastructure.Http.Retry;

public static class PolicyProvider
{
    public static IAsyncPolicy<HttpResponseMessage> Get()
    {
        var bulkheadPolicy = Policy.BulkheadAsync<HttpResponseMessage>(10, Int32.MaxValue);
        var timeoutPolicy = Policy.TimeoutAsync(TimeSpan.FromSeconds(180));
        var policy = Policy
            .Handle<HttpRequestException>()
            .OrResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.TooManyRequests)
            .WaitAndRetryAsync(5, retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .WrapAsync(bulkheadPolicy);

        return timeoutPolicy.WrapAsync(policy);
    }
}