namespace WebClientHelper
{
    public interface IWebClient
    {
        Task<IResponse<T>> GetAsync<T>(String uri, CancellationToken cancellationToken) where T : class;
    }
}
