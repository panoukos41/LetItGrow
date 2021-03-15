using FluentValidation;
using LetItGrow.Microservice.Data.NodeAuths.Requests;

namespace LetItGrow.Microservice.Data.NodeAuths.Validators
{
    public class DeleteNodeAuthValidator : AbstractValidator<DeleteNodeAuth>
    {
        public DeleteNodeAuthValidator()
        {
            RuleFor(x => x.NodeId)
                .ValidId();
        }
    }
}