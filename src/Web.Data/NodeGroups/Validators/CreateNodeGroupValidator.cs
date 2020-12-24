using FluentValidation;
using LetItGrow.Web.Data.NodeGroups.Requests;

namespace LetItGrow.Web.Data.NodeGroups.Validators
{
    /// <summary>
    /// Validates a <see cref="CreateNodeGroup"/> object.
    /// </summary>
    public class CreateNodeGroupValidator : AbstractValidator<CreateNodeGroup>
    {
        public CreateNodeGroupValidator()
        {
            RuleFor(x => x.Name)
                .ValidName();

            RuleFor(x => x.Description!)
                .ValidDescription()
                    .When(x => x.Description is not null);
        }
    }
}