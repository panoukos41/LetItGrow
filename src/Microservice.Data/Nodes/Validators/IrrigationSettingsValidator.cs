using FluentValidation;
using LetItGrow.Microservice.Data.Nodes.Models;

namespace LetItGrow.Microservice.Data.Nodes.Validators
{
    /// <summary>
    /// Validates a <see cref="IrrigationSettings"/> object.
    /// </summary>
    public class IrrigationSettingsValidator : AbstractValidator<IrrigationSettings>
    {
        public IrrigationSettingsValidator()
        {
            RuleFor(x => x.PollInterval)
                .InclusiveBetween(60, 21600);
        }
    }
}