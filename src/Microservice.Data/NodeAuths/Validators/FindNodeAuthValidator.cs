using FluentValidation;
using LetItGrow.Microservice.Data.NodeAuths.Requests;

namespace LetItGrow.Microservice.Data.NodeAuths.Validators
{
    public class FindNodeAuthValidator : AbstractValidator<FindNodeAuth>
    {
        public FindNodeAuthValidator()
        {
            RuleFor(x => x.NodeId)
                .ValidId();
        }
    }
}