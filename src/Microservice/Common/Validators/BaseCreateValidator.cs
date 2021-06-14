using LetItGrow.Microservice.Common.Requests;

namespace LetItGrow.Microservice.Common.Validators
{
    public class BaseCreateValidator<T, TResponse> : BaseValidator<T>
        where T : BaseCreate<TResponse>
    {
    }
}