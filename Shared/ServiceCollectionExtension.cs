using Microsoft.Extensions.DependencyInjection;

namespace Shared
{
    public static class ServiceCollectionExtension
    {
        public static IHttpClientBuilder AddSdkClient<TClient, TImplementation>(this IServiceCollection services, int timeOutInSeconds = 30, bool useRetryPolicy = false)
            where TClient : class
            where TImplementation : class, TClient
        {

            // Overall timeout across all tries
            IHttpClientBuilder httpClientBuilder = services.AddHttpClient<TClient, TImplementation>(o => o.Timeout = TimeSpan.FromSeconds(timeOutInSeconds));
          
            return httpClientBuilder;
        }
    }
}
