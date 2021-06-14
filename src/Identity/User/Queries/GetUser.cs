using LetItGrow.Identity.Common.Queries;
using LetItGrow.Identity.User.Models;

namespace LetItGrow.Identity.User.Queries
{
    public record GetUser : BaseGet<UserModel>
    {
    }
}