using FluentValidation;
using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Measurement.Models;
using LetItGrow.Microservice.Measurement.Requests;

namespace LetItGrow.Microservice.Measurement.Validators
{
    public class SearchManyMeasurementsValidator : BaseSearchValidator<SearchManyMeasurements, MeasurementModel>
    {
        public SearchManyMeasurementsValidator()
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