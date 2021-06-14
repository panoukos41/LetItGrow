using FluentValidation;
using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Measurement.Models;
using LetItGrow.Microservice.Measurement.Requests;

namespace LetItGrow.Microservice.Measurement.Validators
{
    public class SearchMeasurementsValidator : BaseSearchValidator<SearchMeasurements, MeasurementModel>
    {
        public SearchMeasurementsValidator()
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