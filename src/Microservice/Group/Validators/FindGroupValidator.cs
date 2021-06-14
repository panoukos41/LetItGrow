using LetItGrow.Microservice.Common.Validators;
using LetItGrow.Microservice.Group.Models;
using LetItGrow.Microservice.Group.Requests;

namespace LetItGrow.Microservice.Group.Validators
{
    /// <summary>
    /// Validates a <see cref="FindGroup"/> object.
    /// </summary>
    public class FindGroupValidator : BaseFindValidator<FindGroup, GroupModel>
    {
    }
}