using FluentValidation;
using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Group.Requests;

namespace LetItGrow.Microservice.Group.Validators
{
    /// <summary>
    /// Validates a <see cref="CreateGroup"/> object.
    /// </summary>
    public class CreateGroupValidator : BaseCreateValidator<CreateGroup, GroupModel>
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