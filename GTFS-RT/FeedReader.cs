using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ProtoBuf;

namespace GTFS_RT
{
    public abstract class FeedReader : IFeedReader
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FeedReader> _logger;
        public FeedReader(HttpClient httpClient, ILogger<FeedReader> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IResponse<T>> GetFeedAsync<T>(String uri, CancellationToken cancellationToken)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, uri);
                var response = await this._httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

                    return new Response<T>(response.StatusCode, Serializer.Deserialize<T>(stream));
                }
                else
                {
                    return new Response<T>(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"Get request to {this._httpClient.BaseAddress}/{uri} failed.");
                throw;
            }
        }
    }
}
