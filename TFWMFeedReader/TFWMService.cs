using GTFSRT;
using Microsoft.Extensions.Logging;

namespace TFWMFeedReader
{
    internal class TFWMService : ITFWMService
    {
        private readonly ITFWMFeedReader _feedReader;
        private readonly ILogger<ITFWMService> _logger;

        public TFWMService(ITFWMFeedReader feedReader, ILogger<ITFWMService> logger)
        {
            _feedReader = feedReader ?? throw new ArgumentNullException(nameof(feedReader));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private IEnumerable<FeedEntity> GetTripsForRoute(FeedMessage message, string routeId)
        {
            return message
                .Entities
                .Where(e => e.TripUpdate?.Trip?.RouteId == routeId);
        }

        public async Task<IEnumerable<FeedEntity>> GetTripsForRouteAsync(string routeId, CancellationToken cancellationToken)
        {
            var feed = await _feedReader.GetFeedMessageAsync(cancellationToken).ConfigureAwait(false);
            if (feed != null)
            {
                return GetTripsForRoute(feed, routeId);
            }
            {
                _logger.LogInformation("Failed to get details for route {0}.", routeId);
                return Enumerable.Empty<FeedEntity>();
            }
        }
    }
}
