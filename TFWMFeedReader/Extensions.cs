using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TFWMFeedReader
{
    public static class Extensions
    {
        private const string HttpClientName = "TFWM";

        public static IServiceCollection AddTFWMFeedReader(this IServiceCollection services)
        {
            services.AddHttpClient(HttpClientName, (sp, c) =>
            {
                var options = sp.GetRequiredService<IOptions<TFWMOptions>>();
                c.BaseAddress = new Uri(options.Value.BaseUri);
            });
            return services.AddTransient<ITFWMFeedReader, TFWMFeedReader>((sp) =>
            {
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(HttpClientName);

                return new TFWMFeedReader(httpClient, sp.GetRequiredService<ILogger<TFWMFeedReader>>(), sp.GetRequiredService<IOptions<TFWMOptions>>());
            });
        }

        public static IServiceCollection AddTFWMService(this IServiceCollection services)
        {
            return services.AddTransient<ITFWMService, TFWMService>();
        }
    }
}
