using FluentValidation;

namespace LetItGrow.Identity.Common.Validators
{
    public static class ValidatorExtensions
    {
        /// <summary>
        /// Validates the length of an id. It must be 11 characters.
        /// </summary>
        public static IRuleBuilderOptions<T, string> ValidId<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotEmpty();
        }

        /// <summary>
        /// Validates a concurrencytoken by calling NotEmpty.
        /// </summary>
        public static IRuleBuilderOptions<T, TStamp> ValidRev<T, TStamp>(this IRuleBuilder<T, TStamp> ruleBuilder)
        {
            return ruleBuilder.NotEmpty();
        }

        /// <summary>
        /// Validates a Name. It can't be empty and it can have a maximum length of 50.
        /// </summary>

        public static IRuleBuilderOptions<T, string> ValidPassword<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty().WithMessage("Your password cannot be empty")
                .MinimumLength(8)
                .MaximumLength(60)
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!?*.).");
        }

        /// <summary>
        /// Validates a Name. It can't be empty and it can have a maximum length of 50.
        /// </summary>
        public static IRuleBuilderOptions<T, string> ValidName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .MaximumLength(50);
        }

        /// <summary>
        /// Validates a Description. It can have a maximum length of 250. <br/>
        /// This doesn't check if the value is null.
        /// </summary>
        public static IRuleBuilderOptions<T, string> ValidDescription<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .MaximumLength(250);
        }
    }
}