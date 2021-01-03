using FluentValidation;
using LetItGrow.Microservice.Data.NodeGroups.Requests;

namespace LetItGrow.Microservice.Data.NodeGroups.Validators
{
    /// <summary>
    /// Validates a <see cref="DeleteNodeGroup"/> object.
    /// </summary>
    public class DeleteNodeGroupValidator : AbstractValidator<DeleteNodeGroup>
    {
        public DeleteNodeGroupValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();

            RuleFor(x => x.ConcurrencyStamp)
                .ValidConcurrencyStamp();
        }
    }
}