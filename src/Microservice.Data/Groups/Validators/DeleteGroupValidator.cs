using FluentValidation;
using LetItGrow.Microservice.Data.Groups.Requests;

namespace LetItGrow.Microservice.Data.Groups.Validators
{
    /// <summary>
    /// Validates a <see cref="DeleteGroup"/> object.
    /// </summary>
    public class DeleteGroupValidator : AbstractValidator<DeleteGroup>
    {
        public DeleteGroupValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();

            RuleFor(x => x.ConcurrencyStamp)
                .ValidConcurrencyStamp();
        }
    }
}