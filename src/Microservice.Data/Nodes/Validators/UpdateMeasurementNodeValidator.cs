using FluentValidation;
using LetItGrow.Microservice.Data.Nodes.Requests;
using LetItGrow.Microservice.Data.Nodes.Validators.Internal;

namespace LetItGrow.Microservice.Data.Nodes.Validators
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