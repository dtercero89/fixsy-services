using FixsyWebApi.DTO;

namespace FixsyWebApi.Data.Extensions
{
    public static class ResponseBaseExtension
    {
        public static T AddValidationMessage<T>(this ResponseBase response, string message)
            where T : ResponseBase
        {
            response.ValidationErrorMessage = message;
            return (T)response;
        }

        public static T AddSuccesMessage<T>(this ResponseBase response, string message)
            where T : ResponseBase
        {
            response.SuccessMessage = message;
            return (T)response;
        }
    }
}
