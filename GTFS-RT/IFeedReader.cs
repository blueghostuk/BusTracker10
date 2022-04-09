using System.Net;

namespace GTFS_RT
{
    public interface IFeedReader
    {
        Task<IResponse<T>> GetFeedAsync<T>(String uri, CancellationToken cancellationToken); 
    }
}
