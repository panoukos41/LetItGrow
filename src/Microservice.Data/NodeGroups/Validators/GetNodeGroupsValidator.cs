using FluentValidation;
using LetItGrow.Microservice.Data.NodeGroups.Requests;

namespace LetItGrow.Microservice.Data.NodeGroups.Validators
{
    /// <summary>
    /// Validates a <see cref="GetNodeGroups"/> object.
    /// </summary>
    public class GetNodeGroupsValidator : AbstractValidator<GetNodeGroups>
    {
        public GetNodeGroupsValidator()
        {
        }
    }
}