using FluentValidation;
using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Irrigation.Models;
using LetItGrow.Microservice.Irrigation.Requests;

namespace LetItGrow.Microservice.Irrigation.Validators
{
    public class SearchManyIrrigationsValidator : BaseSearchValidator<SearchManyIrrigations, IrrigationModel>
    {
        public SearchManyIrrigationsValidator()
        {
            RuleFor(x => x.NodeIds)
                .NotEmpty()
                .ForEach(x => x.ValidId());

            RuleFor(x => x.StartDate)
                .NotEmpty();

            RuleFor(x => x.EndDate)
                .NotEmpty();
        }
    }
}