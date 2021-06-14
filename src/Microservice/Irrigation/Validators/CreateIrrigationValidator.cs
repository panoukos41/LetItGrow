using FluentValidation;
using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;
using MediatR;

namespace LetItGrow.Microservice.Irrigation.Validators
{
    public class CreateIrrigationValidator : BaseCreateValidator<CreateIrrigation, Unit>
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