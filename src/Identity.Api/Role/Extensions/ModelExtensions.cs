using AspNetCore.Identity.CouchDB.Models;
using LetItGrow.Identity.Role.Models;

namespace LetItGrow.Identity.Role.Extensions
{
    public static class ModelExtensions
    {
        public static RoleModel ToModel(this CouchDbRole other) =>
            new(other.Id, other.Rev, other.Name);
    }
}