using FluentValidation;
using LetItGrow.Microservice.Data.NodeAuths.Requests;

namespace LetItGrow.Microservice.Data.NodeAuths.Validators
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