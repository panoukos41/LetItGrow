using FluentValidation;
using LetItGrow.Microservice.Data.Irrigations.Models;
using LetItGrow.Microservice.Data.Irrigations.Requests;

namespace LetItGrow.Microservice.Data.Irrigations.Validators
{
    public class CreateIrrigationValidator : AbstractValidator<CreateIrrigation>
    {
        public CreateIrrigationValidator()
        {
            RuleFor(x => x.NodeId)
                .ValidId();

            RuleFor(x => x.IssuedAt)
                .NotEmpty();

            RuleFor(x => x.Type)
                .NotEqual(IrrigationType.Invalid)
                .IsInEnum();
        }
    }
}