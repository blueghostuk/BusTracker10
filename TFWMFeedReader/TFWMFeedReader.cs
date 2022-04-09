using GTFS_RT;
using GTFSRT;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace TFWMFeedReader
{
    internal class TFWMFeedReader : FeedReader, ITFWMFeedReader
    {
        private readonly TFWMOptions _options;

        public TFWMFeedReader(HttpClient httpClient, ILogger<TFWMFeedReader> logger, IOptions<TFWMOptions> options)
            : base(httpClient, logger)
        {
            _options = options.Value;
        }

        public async Task<FeedMessage?> GetFeedMessageAsync(CancellationToken cancellationToken)
        {
            var feedMessage = await GetFeedAsync<FeedMessage>($"{_options.Endpoint}?app_id={_options.AppId}&app_key={_options.AppKey}", cancellationToken);
            if (feedMessage != null && feedMessage.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return feedMessage.Content;
            }
            else
            {
                return null;
            }
        }
    }



    public static class TFWMFeedReaderExtensions
    {
        public static IServiceCollection AddTFWMFeedReader(this IServiceCollection collection, IOptions<TFWMOptions> options)
        {
            return services
                .AddHttpClient("TFWM", options.Value.BaseUri)
                .AddTransient<ITFWMFeedReader, TFWMFeedReader>();
        }
    }
}
