using FluentValidation;
using LetItGrow.Web.Data.Nodes.Requests.Internal;

namespace LetItGrow.Web.Data.Nodes.Validators.Internal
{
    /// <summary>
    /// Base validator for the creation of nodes and validates:<br/>
    /// <br/>
    /// Name<br/>
    /// Description
    /// </summary>
    public abstract class CreateNodeBaseValidator<TCreateNode> : AbstractValidator<TCreateNode>
        where TCreateNode : CreateNodeBase
    {
        public CreateNodeBaseValidator()
        {
            RuleFor(x => x.Name)
                .ValidName();

            RuleFor(x => x.Description!)
                .ValidDescription()
                    .When(x => x.Description is not null);
        }
    }
}