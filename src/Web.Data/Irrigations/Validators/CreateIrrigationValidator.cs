using FluentValidation;
using LetItGrow.Web.Data.Irrigations.Models;
using LetItGrow.Web.Data.Irrigations.Requests;

namespace LetItGrow.Web.Data.Irrigations.Validators
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