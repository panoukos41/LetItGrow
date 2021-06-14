using LetItGrow.Identity.Common.Validators;
using LetItGrow.Identity.User.Commands;

namespace LetItGrow.Identity.User.Validators
{
    public class RoleAddValidator : BaseValidator<RoleAdd>
    {
        public RoleAddValidator()
        {
            RuleFor(x => x.UserId)
                .ValidId();

            RuleFor(x => x.RoleName)
                .ValidName();
        }
    }
}