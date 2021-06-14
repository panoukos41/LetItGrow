using LetItGrow.Microservice.Common.Models;
using LetItGrow.Microservice.Common.Requests;

namespace LetItGrow.Microservice.Common.Validators
{
    public class BaseUpdateValidator<T> : BaseUpdateValidator<T, ModelUpdate>
        where T : BaseUpdate
    {
    }

    public class BaseUpdateValidator<T, TResponse> : BaseValidator<T>
        where T : BaseUpdate<TResponse>
    {
        public BaseUpdateValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();

            RuleFor(x => x.ConcurrencyStamp)
                .ValidConcurrencyStamp();
        }
    }
}