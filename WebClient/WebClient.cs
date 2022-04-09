using Microsoft.Extensions.Logging;
using ProtoBuf;

namespace WebClientHelper
{
    public abstract class WebClient : IWebClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WebClient> _logger;
        public WebClient(HttpClient httpClient, ILogger<WebClient> logger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IResponse<T>> GetAsync<T>(String uri, CancellationToken cancellationToken) where T : class
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
