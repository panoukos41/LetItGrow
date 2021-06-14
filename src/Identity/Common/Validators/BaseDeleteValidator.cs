using LetItGrow.Identity.Common.Commands;

namespace LetItGrow.Identity.Common.Validators
{
    public abstract class BaseDeleteValidator<T> : BaseValidator<T>
        where T : BaseDelete
    {
        protected BaseDeleteValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();

            RuleFor(x => x.Rev)
                .ValidRev();
        }
    }
}