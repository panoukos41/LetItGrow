using FluentValidation;
using LetItGrow.Web.Data.Nodes.Requests;
using LetItGrow.Web.Data.Nodes.Validators.Internal;

namespace LetItGrow.Web.Data.Nodes.Validators
{
    /// <summary>
    /// Validates a <see cref="CreateIrrigationNode"/> object.<br/>
    /// Inherits from <see cref="CreateNodeBaseValidator{T}"/>
    /// </summary>
    public class CreateIrrigationNodeValidator : CreateNodeBaseValidator<CreateIrrigationNode>
    {
        public CreateIrrigationNodeValidator()
        {
            RuleFor(x => x.Settings)
                .SetValidator(new IrrigationSettingsModelValidator()!)
                    .When(x => x.Settings is not null);
        }
    }
}