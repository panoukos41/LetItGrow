using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Requests;

namespace LetItGrow.Microservice.Node.Validators
{
    /// <summary>
    /// Validates a <see cref="FindNode"/> object.
    /// </summary>
    public class FindNodeValidator : BaseFindValidator<FindNode, NodeModel>
    {
    }
}