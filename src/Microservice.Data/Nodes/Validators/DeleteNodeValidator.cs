using FluentValidation;
using LetItGrow.Microservice.Data.Nodes.Requests;

namespace LetItGrow.Microservice.Data.Nodes.Validators
{
    /// <summary>
    /// Validates a <see cref="DeleteNode"/> object.
    /// </summary>
    public class DeleteNodeValidator : AbstractValidator<DeleteNode>
    {
        public DeleteNodeValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();

            RuleFor(x => x.ConcurrencyStamp)
                .ValidConcurrencyStamp();
        }
    }
}