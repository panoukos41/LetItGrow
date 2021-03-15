using FluentValidation;
using LetItGrow.Microservice.Data.Nodes.Requests;

namespace LetItGrow.Microservice.Data.Nodes.Validators
{
    /// <summary>
    /// Validates a <see cref="FindNode"/> object.
    /// </summary>
    public class FindNodeValidator : AbstractValidator<FindNode>
    {
        public FindNodeValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();
        }
    }
}