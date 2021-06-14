using FluentValidation;
using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Group.Requests;

namespace LetItGrow.Microservice.Group.Validators
{
    /// <summary>
    /// Validates a <see cref="UpdateGroup"/> object.
    /// </summary>
    public class UpdateGroupValidator : BaseUpdateValidator<UpdateGroup>
    {
        public UpdateGroupValidator()
        {
            RuleFor(x => x.Name!)
                .ValidName()
                    .When(x => x.Name is not null);

            RuleFor(x => x.Description!)
                .ValidDescription()
                    .When(x => x.Description is not null);

            RuleFor(x => x.Type!)
                .ValidGroupType()
                    .When(x => x.Type is not null);
        }
    }
}