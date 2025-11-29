using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MauiEnterpriseApp.Services.Api
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _serializerOptions;

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = false
            };
        }

        public async Task<ApiResult<TResponse>> GetAsync<TResponse>(
            string relativeUrl,
            ApiRequestOptions? options = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, relativeUrl);
                ApplyRequestOptions(request, options);

                using var response = await _httpClient.SendAsync(request, cancellationToken);
                var statusCode = response.StatusCode;

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    return ApiResult<TResponse>.Failure(
                        errorCode: "HTTP_ERROR",
                        statusCode: statusCode,
                        rawError: errorContent);
                }

                var data = await response.Content.ReadFromJsonAsync<TResponse>(_serializerOptions, cancellationToken);

                if (data == null)
                {
                    return ApiResult<TResponse>.Failure(
                        errorCode: "EMPTY_RESPONSE",
                        statusCode: statusCode);
                }

                return ApiResult<TResponse>.Success(data, statusCode);
            }
            catch (Exception ex)
            {
                return ApiResult<TResponse>.Failure(
                    errorCode: "NETWORK_ERROR",
                    statusCode: System.Net.HttpStatusCode.InternalServerError,
                    rawError: ex.Message);
            }
        }

        public async Task<ApiResult<TResponse>> PostAsync<TRequest, TResponse>(
            string relativeUrl,
            TRequest body,
            ApiRequestOptions? options = null,
            CancellationToken cancellationToken = default)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Post, relativeUrl)
                {
                    Content = JsonContent.Create(body, options: _serializerOptions)
                };

                ApplyRequestOptions(request, options);

                using var response = await _httpClient.SendAsync(request, cancellationToken);
                var statusCode = response.StatusCode;

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    return ApiResult<TResponse>.Failure(
                        errorCode: "HTTP_ERROR",
                        statusCode: statusCode,
                        rawError: errorContent);
                }

                var data = await response.Content.ReadFromJsonAsync<TResponse>(_serializerOptions, cancellationToken);

                if (data == null)
                {
                    return ApiResult<TResponse>.Failure(
                        errorCode: "EMPTY_RESPONSE",
                        statusCode: statusCode);
                }

                return ApiResult<TResponse>.Success(data, statusCode);
            }
            catch (Exception ex)
            {
                return ApiResult<TResponse>.Failure(
                    errorCode: "NETWORK_ERROR",
                    statusCode: System.Net.HttpStatusCode.InternalServerError,
                    rawError: ex.Message);
            }
        }

        private static void ApplyRequestOptions(HttpRequestMessage request, ApiRequestOptions? options)
        {
            if (options == null)
                return;

            foreach (var header in options.Headers)
            {
                request.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            if (options.Auth is { Scheme: not ApiAuthScheme.None } auth)
            {
                switch (auth.Scheme)
                {
                    case ApiAuthScheme.Bearer:
                        if (!string.IsNullOrWhiteSpace(auth.Token))
                        {
                            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth.Token);
                        }
                        break;

                    case ApiAuthScheme.Basic:
                        if (!string.IsNullOrWhiteSpace(auth.UserName) && auth.Password != null)
                        {
                            var raw = $"{auth.UserName}:{auth.Password}";
                            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(raw));
                            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", base64);
                        }
                        break;

                    case ApiAuthScheme.Custom:
                        if (!string.IsNullOrWhiteSpace(auth.CustomSchemeName) &&
                            !string.IsNullOrWhiteSpace(auth.Token))
                        {
                            request.Headers.Authorization = new AuthenticationHeaderValue(auth.CustomSchemeName, auth.Token);
                        }
                        break;
                }
            }
        }
    }
}
