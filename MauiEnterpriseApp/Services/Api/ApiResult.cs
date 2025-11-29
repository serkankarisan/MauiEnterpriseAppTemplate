using System.Net;

namespace MauiEnterpriseApp.Services.Api
{
    /// <summary>
    /// API çağrılarının ortak sonuç modeli.
    /// Bu katmanda kullanıcıya gösterilecek mesajlar değil, teknik bilgi ve kodlar taşınır.
    /// UI'da gösterilecek stringler AppResources üzerinden üretilmelidir.
    /// </summary>
    public class ApiResult<T>
    {
        public bool IsSuccess { get; init; }
        public T? Data { get; init; }
        public string? ErrorCode { get; init; } // HTTP_ERROR, NETWORK_ERROR, ...
        public string? RawError { get; init; }
        public HttpStatusCode StatusCode { get; init; }

        public static ApiResult<T> Success(T data, HttpStatusCode statusCode) =>
            new ApiResult<T>
            {
                IsSuccess = true,
                Data = data,
                StatusCode = statusCode
            };

        public static ApiResult<T> Failure(string errorCode, HttpStatusCode statusCode, string? rawError = null) =>
            new ApiResult<T>
            {
                IsSuccess = false,
                ErrorCode = errorCode,
                RawError = rawError,
                StatusCode = statusCode
            };
    }
}
