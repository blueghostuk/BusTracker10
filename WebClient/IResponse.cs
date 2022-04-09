using System.Net;

namespace WebClientHelper
{
    public interface IResponse<T> where T : class
    {
        HttpStatusCode StatusCode { get; }
        T? Content { get; }
    }
}
