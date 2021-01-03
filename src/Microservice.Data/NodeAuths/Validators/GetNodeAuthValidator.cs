using FluentValidation;
using LetItGrow.Microservice.Data.NodeAuths.Requests;

namespace LetItGrow.Microservice.Data.NodeAuths.Validators
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