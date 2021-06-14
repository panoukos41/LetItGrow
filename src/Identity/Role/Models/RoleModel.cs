using LetItGrow.Identity.Common.Models;
using System.Text.Json.Serialization;

namespace LetItGrow.Identity.Role.Models
{
    public record RoleModel : BaseModel
    {
        [JsonConstructor]
        public RoleModel(string id, string rev, string name)
        {
            Id = id;
            Rev = rev;
            Name = name;
        }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}