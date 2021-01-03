using FluentValidation;
using LetItGrow.Microservice.Data.Nodes.Requests;

namespace LetItGrow.Microservice.Data.Nodes.Validators
{
    /// <summary>
    /// Validates a <see cref="GetNodes"/> object.
    /// </summary>
    public class GetNodesValidator : AbstractValidator<GetNodes>
    {
        public GetNodesValidator()
        {
            RuleFor(x => x.Type)
                .Must(x => x is "irrigation" or "measurement")
                    .When(x => x.Type is not null);
        }
    }
}