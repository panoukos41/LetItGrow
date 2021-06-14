using LetItGrow.Microservice.Common.Requests;

namespace LetItGrow.Microservice.Common.Validators
{
    public abstract class BaseDeleteValidator<T> : BaseValidator<T>
        where T : BaseDelete
    {
        protected BaseDeleteValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();

            RuleFor(x => x.ConcurrencyStamp)
                .ValidConcurrencyStamp();
        }
    }
}