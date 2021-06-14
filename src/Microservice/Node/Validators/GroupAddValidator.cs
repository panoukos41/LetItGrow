using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Node.Requests;

namespace LetItGrow.Microservice.Node.Validators
{
    /// <summary>
    /// Validates a <see cref="GroupAdd"/> object.
    /// </summary>
    public class GroupAddValidator : BaseUpdateValidator<GroupAdd>
    {
        public GroupAddValidator()
        {
            RuleFor(x => x.GroupId)
                .ValidId();
        }
    }
}