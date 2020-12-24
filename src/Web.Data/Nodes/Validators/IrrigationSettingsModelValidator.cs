using FluentValidation;
using LetItGrow.Web.Data.Nodes.Models;

namespace LetItGrow.Web.Data.Nodes.Validators
{
    /// <summary>
    /// Validates a <see cref="IrrigationSettings"/> object.
    /// </summary>
    public class IrrigationSettingsModelValidator : AbstractValidator<IrrigationSettings>
    {
        public IrrigationSettingsModelValidator()
        {
            RuleFor(x => x.PollInterval).Transform(x => (int)x)
                .InclusiveBetween(60, 21600);
        }
    }
}