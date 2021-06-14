using LetItGrow.Microservice.Common.Requests;

namespace LetItGrow.Microservice.Common.Validators
{
    public abstract class BaseFindValidator<T, TResponse> : BaseValidator<T>
        where T : BaseFind<TResponse>
    {
        protected BaseFindValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();
        }
    }
}