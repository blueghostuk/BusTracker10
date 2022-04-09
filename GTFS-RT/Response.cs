using GTFSRT;
using System.Net;

namespace GTFS_RT
{
    internal class Response<T> : IResponse<T> where T : class
    {
        public HttpStatusCode StatusCode { get; }

        public T? Content { get; }

        public Response(HttpStatusCode statusCode, T? content = null)
        {
            this.StatusCode = statusCode;
            this.Content = content;
        }
    }
}
