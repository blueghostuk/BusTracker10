using GTFSRT;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WebClientHelper;

namespace TFWMFeedReader
{
    internal class TFWMFeedReader : WebClient, ITFWMFeedReader
    {
        private readonly TFWMOptions _options;

        public TFWMFeedReader(HttpClient httpClient, ILogger<TFWMFeedReader> logger, IOptions<TFWMOptions> options)
            : base(httpClient, logger)
        {
            _options = options.Value;
        }

        public async Task<FeedMessage?> GetFeedMessageAsync(CancellationToken cancellationToken)
        {
            var feedMessage = await GetAsync<FeedMessage>($"{_options.Endpoint}?app_id={_options.ApiUser}&app_key={_options.ApiKey}", cancellationToken);
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
}
