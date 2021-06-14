using FluentValidation;
using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Node.Models;

namespace LetItGrow.Microservice.Node.Validators
{
    /// <summary>
    /// Validates a <see cref="IrrigationSettings"/> object.
    /// </summary>
    public class IrrigationSettingsValidator : BaseValidator<IrrigationSettings>
    {
        public IrrigationSettingsValidator()
        {
            RuleFor(x => x.PollInterval)
                .InclusiveBetween(60, 21600);
        }
    }
}