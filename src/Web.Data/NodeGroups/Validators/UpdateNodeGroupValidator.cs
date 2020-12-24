using FluentValidation;
using LetItGrow.Web.Data.NodeGroups.Requests;

namespace LetItGrow.Web.Data.NodeGroups.Validators
{
    /// <summary>
    /// Validates a <see cref="UpdateNodeGroup"/> object.
    /// </summary>
    public class UpdateNodeGroupValidator : AbstractValidator<UpdateNodeGroup>
    {
        public UpdateNodeGroupValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();

            RuleFor(x => x.ConcurrencyStamp)
                .ValidConcurrencyStamp();

            RuleFor(x => x.Name!)
                .ValidName()
                    .When(x => x.Name is not null);

            RuleFor(x => x.Description!)
                .ValidDescription()
                    .When(x => x.Description is not null);
        }
    }
}