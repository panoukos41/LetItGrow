using FluentValidation;
using LetItGrow.Web.Data.NodeAuths.Requests;

namespace LetItGrow.Web.Data.NodeAuths.Validators
{
    public class InvalidateNodeAtuhValidator : AbstractValidator<GetNodeAuth>
    {
        public InvalidateNodeAtuhValidator()
        {
            RuleFor(x => x.NodeId)
                .ValidId();
        }
    }
}