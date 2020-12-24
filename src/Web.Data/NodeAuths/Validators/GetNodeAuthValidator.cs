using FluentValidation;
using LetItGrow.Web.Data.NodeAuths.Requests;

namespace LetItGrow.Web.Data.NodeAuths.Validators
{
    public class GetNodeAuthValidator : AbstractValidator<GetNodeAuth>
    {
        public GetNodeAuthValidator()
        {
            RuleFor(x => x.NodeId)
                .ValidId();
        }
    }
}