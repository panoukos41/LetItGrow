using FluentValidation;
using LetItGrow.Microservice.Data.Groups.Requests;

namespace LetItGrow.Microservice.Data.Groups.Validators
{
    /// <summary>
    /// Validates a <see cref="FindGroup"/> object.
    /// </summary>
    public class FindGroupValidator : AbstractValidator<FindGroup>
    {
        public FindGroupValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();
        }
    }
}