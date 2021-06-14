using FluentValidation;
using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;

namespace LetItGrow.Microservice.Irrigation.Validators
{
    public class SearchIrrigationsValidator : BaseSearchValidator<SearchIrrigations, IrrigationModel>
    {
        public SearchIrrigationsValidator()
        {
            RuleFor(x => x.NodeId)
                .ValidId();

            RuleFor(x => x.StartDate)
                .GreaterThan(x => x.EndDate)
                .NotEmpty();

            RuleFor(x => x.EndDate)
                .LessThan(x => x.StartDate)
                .NotEmpty();
        }
    }
}