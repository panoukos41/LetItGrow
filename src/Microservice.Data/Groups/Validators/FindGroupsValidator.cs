using FluentValidation;
using LetItGrow.Microservice.Data.Groups.Requests;

namespace LetItGrow.Microservice.Data.Groups.Validators
{
    /// <summary>
    /// Validates a <see cref="FindGroups"/> object.
    /// </summary>
    public class FindGroupsValidator : AbstractValidator<FindGroups>
    {
        public FindGroupsValidator()
        {
        }
    }
}