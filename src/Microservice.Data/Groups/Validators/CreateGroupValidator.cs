using FluentValidation;
using LetItGrow.Microservice.Data.Groups.Requests;

namespace LetItGrow.Microservice.Data.Groups.Validators
{
    /// <summary>
    /// Validates a <see cref="CreateGroup"/> object.
    /// </summary>
    public class CreateGroupValidator : AbstractValidator<CreateGroup>
    {
        public CreateGroupValidator()
        {
            RuleFor(x => x.Name)
                .ValidName();

            RuleFor(x => x.Description!)
                .ValidDescription()
                    .When(x => x.Description is not null);

            RuleFor(x => x.Type)
                .ValidGroupType();
        }
    }
}