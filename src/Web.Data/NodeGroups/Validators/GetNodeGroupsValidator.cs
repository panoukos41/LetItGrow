using FluentValidation;
using LetItGrow.Web.Data.NodeGroups.Requests;

namespace LetItGrow.Web.Data.NodeGroups.Validators
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