using FluentValidation;
using LetItGrow.Microservice.Data.NodeGroups.Requests;

namespace LetItGrow.Microservice.Data.NodeGroups.Validators
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