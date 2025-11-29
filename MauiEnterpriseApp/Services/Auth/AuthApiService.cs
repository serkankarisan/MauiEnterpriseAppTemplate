using MauiEnterpriseApp.Models.Api;
using MauiEnterpriseApp.Models.Auth;
using MauiEnterpriseApp.Services.Api;

namespace MauiEnterpriseApp.Services.Auth
{
    public class AuthApiService : IAuthService
    {
        private readonly IApiClient _apiClient;

        public AuthApiService(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task<LoginServiceResult> LoginAsync(string email, string password)
        {
            var request = new LoginRequest
            {
                Email = email,
                Password = password
            };

            // 1) HTTP / teknik katman
            var apiResult = await _apiClient.PostAsync<LoginRequest, ApiEnvelope<LoginResponse>>(
                "api/auth/login",
                request);

            // 1-a) HTTP / network / serialization hatası
            if (!apiResult.IsSuccess)
            {
                return LoginServiceResult.TechnicalFailure(
                    technicalErrorCode: apiResult.ErrorCode,
                    rawError: apiResult.RawError);
            }

            // 2) İş sonucu katmanı
            var envelope = apiResult.Data;

            if (envelope == null)
            {
                return LoginServiceResult.TechnicalFailure(
                    technicalErrorCode: "EMPTY_ENVELOPE",
                    rawError: null);
            }

            if (!envelope.Success)
            {
                // İşlemsel hata: örn. wrong password, user locked vs.
                // Backend'ten gelen MessageCode'u UI'de AppResources ile eşleyebiliriz.
                return LoginServiceResult.BusinessFailure(
                    messageCode: envelope.MessageCode,
                    rawMessage: envelope.Message);
            }

            // 3) Tam başarı
            return LoginServiceResult.Success(envelope.Data);
        }
    }
}
