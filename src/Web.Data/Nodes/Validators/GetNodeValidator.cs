using FluentValidation;
using LetItGrow.Web.Data.Nodes.Requests;

namespace LetItGrow.Web.Data.Nodes.Validators
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