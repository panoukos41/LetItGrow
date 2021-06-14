using AspNetCore.Identity.CouchDB.Models;
using LetItGrow.Identity.Role.Extensions;
using LetItGrow.Identity.User.Models;
using System.Linq;

namespace LetItGrow.Identity.User.Extensions
{
    public static class ModelExtensions
    {
        public static UserModel ToModel(this CouchDbUser other) =>
            new(other.Id, other.Rev, other.UserName, other.Roles.Select(x => x.ToModel()).ToHashSet());
    }
}