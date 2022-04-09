using GTFSRT;

namespace TFWMFeedReader
{
    public interface ITFWMService
    {
        Task<IEnumerable<FeedEntity>> GetTripsForRouteAsync(String routeId, CancellationToken cancellationToken);
    }
}
