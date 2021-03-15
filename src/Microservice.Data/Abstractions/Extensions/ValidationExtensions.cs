using LetItGrow.Microservice.Data.Groups.Models;
using LetItGrow.Microservice.Data.Irrigations.Models;
using LetItGrow.Microservice.Data.Nodes.Models;

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
        /// Validates a concurrencytoken by calling NotEmpty.
        /// </summary>
        public static IRuleBuilderOptions<T, TStamp> ValidConcurrencyStamp<T, TStamp>(this IRuleBuilder<T, TStamp> ruleBuilder)
        {
            return ruleBuilder.NotEmpty();
        }

        /// <summary>
        /// Validates a node type.
        /// </summary>
        public static IRuleBuilderOptions<T, NodeType> ValidNodeType<T>(this IRuleBuilder<T, NodeType> ruleBuilder)
        {
            return ruleBuilder
                .NotEqual(NodeType.Invalid)
                .IsInEnum();
        }

        /// <summary>
        /// Validates a node type.
        /// </summary>
        public static IRuleBuilderOptions<T, IrrigationType> ValidIrrigationType<T>(this IRuleBuilder<T, IrrigationType> ruleBuilder)
        {
            return ruleBuilder
                .NotEqual(IrrigationType.Invalid)
                .IsInEnum();
        }

        /// <summary>
        /// Validates a node type.
        /// </summary>
        public static IRuleBuilderOptions<T, GroupType> ValidGroupType<T>(this IRuleBuilder<T, GroupType> ruleBuilder)
        {
            return ruleBuilder
                .IsInEnum();
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