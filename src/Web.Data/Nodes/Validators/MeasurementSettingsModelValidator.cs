using FluentValidation;
using LetItGrow.Web.Data.Nodes.Models;

namespace LetItGrow.Web.Data.Nodes.Validators
{
    /// <summary>
    /// Validates a <see cref="MeasurementSettings"/> object.
    /// </summary>
    public class MeasurementSettingsModelValidator : AbstractValidator<MeasurementSettings>
    {
        public MeasurementSettingsModelValidator()
        {
            RuleFor(x => x.PollInterval).Transform(x => (int)x)
                .InclusiveBetween(60, 3600);
        }
    }
}