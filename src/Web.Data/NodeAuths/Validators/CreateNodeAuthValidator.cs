using FluentValidation;
using LetItGrow.Web.Data.NodeAuths.Requests;

namespace LetItGrow.Web.Data.NodeAuths.Validators
{
    public class CreateNodeAuthValidator : AbstractValidator<CreateNodeAuth>
    {
        public CreateNodeAuthValidator()
        {
            RuleFor(x => x.NodeId)
                .ValidId();
        }
    }
}