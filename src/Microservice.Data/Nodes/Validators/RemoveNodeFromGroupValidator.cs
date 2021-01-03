using FluentValidation;
using LetItGrow.Microservice.Data.Nodes.Requests;

namespace LetItGrow.Microservice.Data.Nodes.Validators
{
    /// <summary>
    /// Validates a <see cref="RemoveNodeFromGroup"/> object.
    /// </summary>
    public class RemoveNodeFromGroupValidator : AbstractValidator<RemoveNodeFromGroup>
    {
        public RemoveNodeFromGroupValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();

            RuleFor(x => x.ConcurrencyStamp)
                .ValidConcurrencyStamp();
        }
    }
}