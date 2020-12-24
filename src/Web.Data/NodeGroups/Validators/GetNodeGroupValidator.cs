using FluentValidation;
using LetItGrow.Web.Data.NodeGroups.Requests;

namespace LetItGrow.Web.Data.NodeGroups.Validators
{
    /// <summary>
    /// Validates a <see cref="GetNodeGroup"/> object.
    /// </summary>
    public class GetNodeGroupValidator : AbstractValidator<GetNodeGroup>
    {
        public GetNodeGroupValidator()
        {
            RuleFor(x => x.Id)
                .ValidId();
        }
    }
}