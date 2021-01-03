using FluentValidation;
using LetItGrow.Microservice.Data.Nodes.Requests;
using LetItGrow.Microservice.Data.Nodes.Validators.Internal;

namespace LetItGrow.Microservice.Data.Nodes.Validators
{
    /// <summary>
    /// Validates a <see cref="CreateMeasurementNode"/> object.<br/>
    /// Inherits from <see cref="CreateNodeBaseValidator{T}"/>
    /// </summary>
    public class CreateMeasurementNodeValidator : CreateNodeBaseValidator<CreateMeasurementNode>
    {
        public CreateMeasurementNodeValidator()
        {
            RuleFor(x => x.Settings)
                .SetValidator(new MeasurementSettingsModelValidator()!)
                    .When(x => x.Settings is not null);
        }
    }
}