using FluentValidation;
using LetItGrow.Web.Data.Nodes.Requests.Internal;

namespace LetItGrow.Web.Data.Nodes.Validators.Internal
{
    /// <summary>
    /// Base validator for the updates of nodes and validates:<br/>
    /// <br/>
    /// Id<br/>
    /// ConcurrencyStamp<br/>
    /// Name<br/>
    /// Description<br/>
    /// </summary>
    public abstract class UpdateNodeBaseValidator<TUpdateNode> : AbstractValidator<TUpdateNode>
        where TUpdateNode : UpdateNodeBase
    {
        public UpdateNodeBaseValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();

            RuleFor(x => x.ConcurrencyStamp)
                .ValidConcurrencyStamp();

            RuleFor(x => x.Name!)
                .ValidName()
                    .When(x => x.Name is not null);

            RuleFor(x => x.Description!)
                .ValidDescription()
                    .When(x => x.Description is not null);
        }
    }
}