using LetItGrow.Identity.Common.Validators;
using LetItGrow.Identity.User.Commands;

namespace LetItGrow.Identity.User.Validators
{
    public class CreateUserValidator : BaseValidator<CreateUser>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.UserName)
                .ValidName();

            RuleFor(x => x.Password)
                .ValidPassword();
        }
    }
}