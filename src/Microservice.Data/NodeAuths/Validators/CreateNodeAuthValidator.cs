using FluentValidation;
using LetItGrow.Microservice.Data.NodeAuths.Requests;

namespace LetItGrow.Microservice.Data.NodeAuths.Validators
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