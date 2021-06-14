using FluentValidation;
using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Node.Models;
using LetItGrow.Microservice.Node.Requests;

namespace LetItGrow.Microservice.Node.Validators
{
    /// <summary>
    /// Validates a <see cref="SearchNodes"/> object.
    /// </summary>
    public class SearchNodesValidator : BaseSearchValidator<SearchNodes, NodeModel>
    {
        public SearchNodesValidator()
        {
            RuleFor(x => x.GroupId!)
                .ValidId()
                    .When(x => x.GroupId is not null);
        }
    }
}