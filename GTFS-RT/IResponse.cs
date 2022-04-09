using GTFSRT;
using System.Net;

namespace GTFS_RT
{
    public interface IResponse<T> where T : class
    {
        HttpStatusCode StatusCode { get; }
        T? Content { get; }
    }
}
