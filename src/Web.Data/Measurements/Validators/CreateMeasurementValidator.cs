using FluentValidation;
using LetItGrow.Web.Data.Measurements.Requests;

namespace LetItGrow.Web.Data.Measurements.Validators
{
    public class CreateMeasurementValidator : AbstractValidator<CreateMeasurement>
    {
        public CreateMeasurementValidator()
        {
            RuleFor(x => x.NodeId)
                .ValidId();

            RuleFor(x => x.MeasuredAt)
                .NotEmpty();

            RuleFor(x => x.AirTemperatureC)
                .InclusiveBetween(-50.00, 80.00);

            RuleFor(x => x.AirHumidity)
                .InclusiveBetween(0.00, 100.00);

            RuleFor(x => x.SoilMoisture)
                .InclusiveBetween(0.00, 100.00);
        }
    }
}