using LetItGrow.Identity.Common.Queries;

namespace LetItGrow.Identity.Common.Validators
{
    public abstract class BaseGetValidator<T, TResponse> : BaseValidator<T>
        where T : BaseGet<TResponse>
    {
        protected BaseGetValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();
        }
    }
}