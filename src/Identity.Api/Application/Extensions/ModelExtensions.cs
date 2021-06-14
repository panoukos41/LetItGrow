using LetItGrow.Identity.Application.Models;
using Newtonsoft.Json.Linq;
using OpenIddict.CouchDB.Models;
using System.Linq;
using System.Text.Json;

namespace LetItGrow.Identity.Application.Extensions
{
    public static class ModelExtensions
    {
        public static ApplicationModel ToModel(this CouchDbOpenIddictApplication other) =>
            new(other.Id, other.Rev)
            {
                ClientId = other.ClientId,
                ClientSecret = other.ClientSecret,
                ConsentType = other.ConsentType,
                DisplayName = other.DisplayName,
                DisplayNames = other.DisplayNames,
                Permissions = other.Permissions,
                PostLogoutRedirectUris = other.PostLogoutRedirectUris,
                Properties = GetJsonDocument(other.Properties),
                RedirectUris = other.RedirectUris.ToHashSet(),
                Requirements = other.Requirements.ToHashSet(),
                Type = other.Type
            };

        private static JsonDocument? GetJsonDocument(JObject? json)
        {
            if (json is null) return null;

            return JsonDocument
                .Parse(json.ToString(Newtonsoft.Json.Formatting.None));
        }
    }
}