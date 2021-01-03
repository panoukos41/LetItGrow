using FluentValidation;
using LetItGrow.Microservice.Data.Nodes.Requests;
using LetItGrow.Microservice.Data.Nodes.Validators.Internal;

namespace LetItGrow.Microservice.Data.Nodes.Validators
{
    /// <summary>
    /// Validates a <see cref="UpdateIrrigationNode"/> object.<br/>
    /// Inherits from <see cref="UpdateNodeBaseValidator{T}"/>
    /// </summary>
    public class UpdateIrrigationNodeValidator : UpdateNodeBaseValidator<UpdateIrrigationNode>
    {
        public UpdateIrrigationNodeValidator()
        {
            RuleFor(x => x.Settings)
                .SetValidator(new IrrigationSettingsModelValidator()!)
                    .When(x => x.Settings is not null);
        }
    }
}