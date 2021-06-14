using FluentValidation;
using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Measurement.Requests;
using MediatR;

namespace LetItGrow.Microservice.Measurement.Validators
{
    public class CreateMeasurementValidator : BaseCreateValidator<CreateMeasurement, Unit>
    {
        public CreateMeasurementValidator()
        {
            RuleFor(x => x.NodeId)
                .ValidId();

            RuleFor(x => x.MeasuredAt)
                .NotEmpty();

            RuleFor(x => x.AirTemperatureC)
                .InclusiveBetween(-20.00, 50.00);

            RuleFor(x => x.AirHumidity)
                .InclusiveBetween(0.00, 100.00);

            RuleFor(x => x.SoilMoisture)
                .InclusiveBetween(0.00, 100.00);
        }
    }
}