using FluentValidation;
using LetItGrow.Microservice.Data.Nodes.Models;

namespace LetItGrow.Microservice.Data.Nodes.Validators
{
    /// <summary>
    /// Validates a <see cref="MeasurementSettings"/> object.
    /// </summary>
    public class MeasurementSettingsValidator : AbstractValidator<MeasurementSettings>
    {
        public MeasurementSettingsValidator()
        {
            RuleFor(x => x.PollInterval)
                .InclusiveBetween(60, 3600);
        }
    }
}