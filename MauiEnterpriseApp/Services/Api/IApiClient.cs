namespace MauiEnterpriseApp.Services.Api
{
    public interface IApiClient
    {
        Task<ApiResult<TResponse>> GetAsync<TResponse>(
            string relativeUrl,
            ApiRequestOptions? options = null,
            CancellationToken cancellationToken = default);

        Task<ApiResult<TResponse>> PostAsync<TRequest, TResponse>(
            string relativeUrl,
            TRequest body,
            ApiRequestOptions? options = null,
            CancellationToken cancellationToken = default);
    }
}
