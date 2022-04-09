using GTFSRT;

namespace TFWMFeedReader
{
    public interface ITFWMFeedReader
    {
        Task<FeedMessage?> GetFeedMessageAsync(CancellationToken cancellationToken);
    }
}