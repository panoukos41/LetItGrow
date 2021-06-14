using FluentValidation;
using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Requests;
using System.Text.Json;

namespace LetItGrow.Microservice.Node.Validators
{
    /// <summary>
    /// Base validator for the updates of nodes and validates:<br/>
    /// <br/>
    /// Id<br/>
    /// ConcurrencyStamp<br/>
    /// Name<br/>
    /// Description<br/>
    /// </summary>
    public class UpdateNodeValidator : BaseUpdateValidator<UpdateNode>
    {
        public UpdateNodeValidator()
        {
            RuleFor(x => x.Type)
                .ValidNodeType();

            RuleFor(x => x.Name!)
                .ValidName()
                    .When(x => x.Name is not null);

            RuleFor(x => x.Description!)
                .ValidDescription()
                    .When(x => x.Description is not null);

            When(x => x.Settings is not null, () =>
            {
                Transform(x => x.Settings, x => x.To<IrrigationSettings>())
                    .SetValidator(new IrrigationSettingsValidator()!)
                    .When(x => x.Type == NodeType.Irrigation);

                Transform(x => x.Settings, x => x.To<MeasurementSettings>())
                    .SetValidator(new MeasurementSettingsValidator()!)
                    .When(x => x.Type == NodeType.Measurement);
            });
        }
    }
}