using LetItGrow.Identity.Common.Validators;
using LetItGrow.Identity.User.Commands;

namespace LetItGrow.Identity.User.Validators
{
    public class RoleRemoveValidator : BaseValidator<RoleRemove>
    {
        public RoleRemoveValidator()
        {
            RuleFor(x => x.UserId)
                .ValidId();

            RuleFor(x => x.RoleName)
                .ValidName();
        }
    }
}