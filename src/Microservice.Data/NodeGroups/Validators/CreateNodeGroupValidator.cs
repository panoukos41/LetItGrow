using FluentValidation;
using LetItGrow.Microservice.Data.NodeGroups.Requests;

namespace LetItGrow.Microservice.Data.NodeGroups.Validators
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