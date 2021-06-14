using FluentValidation;
using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Requests;
using System.Text.Json;

namespace LetItGrow.Microservice.Node.Validators
{
    /// <summary>
    /// Base validator for the creation of nodes and validates:<br/>
    /// <br/>
    /// Name<br/>
    /// Description
    /// </summary>
    public class CreateNodeValidator : BaseCreateValidator<CreateNode, NodeModel>
    {
        public CreateNodeValidator()
        {
            RuleFor(x => x.Name)
                .ValidName();

            RuleFor(x => x.Description!)
                .ValidDescription()
                    .When(x => x.Description is not null);

            RuleFor(x => x.Type)
                .ValidNodeType();

            Transform(x => x.Settings, x => x.To<IrrigationSettings>())
                .SetValidator(new IrrigationSettingsValidator()!)
                .When(x => x is { Type: NodeType.Irrigation });

            Transform(x => x.Settings, x => x.To<MeasurementSettings>())
                .SetValidator(new MeasurementSettingsValidator()!)
                .When(x => x is { Type: NodeType.Measurement });
        }
    }
}