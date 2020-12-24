using FluentValidation;
using LetItGrow.Web.Data.NodeGroups.Requests;

namespace LetItGrow.Web.Data.NodeGroups.Validators
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