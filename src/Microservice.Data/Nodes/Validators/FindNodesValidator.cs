using FluentValidation;
using LetItGrow.Microservice.Data.Nodes.Requests;

namespace LetItGrow.Microservice.Data.Nodes.Validators
{
    /// <summary>
    /// Validates a <see cref="FindNodes"/> object.
    /// </summary>
    public class FindNodesValidator : AbstractValidator<FindNodes>
    {
        public FindNodesValidator()
        {
        }
    }
}