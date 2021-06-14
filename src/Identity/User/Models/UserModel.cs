using LetItGrow.Identity.Common.Models;
using LetItGrow.Identity.Role.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace LetItGrow.Identity.User.Models
{
    public record UserModel : BaseModel
    {
        [JsonConstructor]
        public UserModel(string id, string rev, string userName, HashSet<RoleModel> roles)
        {
            Id = id;
            Rev = rev;
            UserName = userName;
            Roles = roles;
        }

        public UserModel(string id, string rev, string userName, ICollection<RoleModel> roles)
            : this(id, rev, userName, roles.ToHashSet())
        {
        }

        [JsonPropertyName("username")]
        public string UserName { get; }

        [JsonPropertyName("roles")]
        public HashSet<RoleModel> Roles { get; }
    }
}