using FluentValidation;
using LetItGrow.Microservice.Data.Nodes.Requests;

namespace LetItGrow.Microservice.Data.Nodes.Validators
{
    /// <summary>
    /// Validates a <see cref="GetNode"/> object.
    /// </summary>
    public class GetNodeValidator : AbstractValidator<GetNode>
    {
        public GetNodeValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();
        }
    }
}