using FluentValidation;
using LetItGrow.Web.Data.Nodes.Requests;
using LetItGrow.Web.Data.Nodes.Validators.Internal;

namespace LetItGrow.Web.Data.Nodes.Validators
{
    /// <summary>
    /// Validates a <see cref="UpdateMeasurementNode"/> object.<br/>
    /// Inherits from <see cref="UpdateNodeBaseValidator{T}"/>
    /// </summary>
    public class UpdateMeasurementNodeValidator : UpdateNodeBaseValidator<UpdateMeasurementNode>
    {
        public UpdateMeasurementNodeValidator()
        {
            RuleFor(x => x.Settings)
                .SetValidator(new MeasurementSettingsModelValidator()!)
                    .When(x => x.Settings is not null);
        }
    }
}