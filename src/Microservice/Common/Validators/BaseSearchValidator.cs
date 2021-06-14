using LetItGrow.Microservice.Common.Requests;

namespace LetItGrow.Microservice.Common.Validators
{
    public class BaseSearchValidator<T, TResponse> : BaseValidator<T>
        where T : BaseSearch<TResponse>
    {
    }
}