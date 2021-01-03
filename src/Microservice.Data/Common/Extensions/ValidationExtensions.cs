namespace FluentValidation
{
    public static class ValidationExtensions
    {
        /// <summary>
        /// Validates the length of an id. It must be 11 characters.
        /// </summary>
        public static IRuleBuilderOptions<T, string> ValidId<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Length(11);
        }

        /// <summary>
        /// Validates a concurrencytoken.
        /// </summary>
        public static IRuleBuilderOptions<T, uint> ValidConcurrencyStamp<T>(this IRuleBuilder<T, uint> ruleBuilder)
        {
            return ruleBuilder.NotEmpty();
        }

        /// <summary>
        /// Validates the length of a node auth token. It must be 45 characters.
        /// </summary>
        public static IRuleBuilderOptions<T, string> ValidToken<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Length(45);
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