using FluentValidation;
using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Node.Models;

namespace LetItGrow.Microservice.Node.Validators
{
    /// <summary>
    /// Validates a <see cref="MeasurementSettings"/> object.
    /// </summary>
    public class MeasurementSettingsValidator : BaseValidator<MeasurementSettings>
    {
        public MeasurementSettingsValidator()
        {
            RuleFor(x => x.PollInterval)
                .InclusiveBetween(60, 3600);
        }
    }
}