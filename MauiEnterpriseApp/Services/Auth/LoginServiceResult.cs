using MauiEnterpriseApp.Models.Auth;

namespace MauiEnterpriseApp.Services.Auth
{
    public class LoginServiceResult
    {
        public bool IsSuccess { get; }
        public bool IsTechnicalError { get; }
        public string? MessageCode { get; }
        public string? TechnicalErrorCode { get; }
        public string? RawMessage { get; }
        public LoginResponse? Data { get; }

        private LoginServiceResult(
            bool isSuccess,
            bool isTechnicalError,
            string? messageCode,
            string? technicalErrorCode,
            string? rawMessage,
            LoginResponse? data)
        {
            IsSuccess = isSuccess;
            IsTechnicalError = isTechnicalError;
            MessageCode = messageCode;
            TechnicalErrorCode = technicalErrorCode;
            RawMessage = rawMessage;
            Data = data;
        }

        public static LoginServiceResult Success(LoginResponse? data) =>
            new LoginServiceResult(
                isSuccess: true,
                isTechnicalError: false,
                messageCode: null,
                technicalErrorCode: null,
                rawMessage: null,
                data: data);

        public static LoginServiceResult BusinessFailure(string? messageCode, string? rawMessage) =>
            new LoginServiceResult(
                isSuccess: false,
                isTechnicalError: false,
                messageCode: messageCode,
                technicalErrorCode: null,
                rawMessage: rawMessage,
                data: null);

        public static LoginServiceResult TechnicalFailure(string? technicalErrorCode, string? rawError) =>
            new LoginServiceResult(
                isSuccess: false,
                isTechnicalError: true,
                messageCode: null,
                technicalErrorCode: technicalErrorCode,
                rawMessage: rawError,
                data: null);
    }
}
