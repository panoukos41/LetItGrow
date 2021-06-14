using LetItGrow.Identity.Common.Models;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LetItGrow.Identity.Application.Models
{
    public record ApplicationModel : BaseModel
    {
        [JsonConstructor]
        public ApplicationModel(string id, string rev)
        {
            Id = id;
            Rev = rev;
        }

        /// <summary>
        /// Gets or sets the client identifier associated with the current application.
        /// </summary>
        [JsonPropertyName("client_id")]
        public virtual string? ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client secret associated with the current application.
        /// Note: depending on the application manager used to create this instance,
        /// this property may be hashed or encrypted for security reasons.
        /// </summary>
        [JsonPropertyName("client_secret")]
        public virtual string? ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets the consent type associated with the current application.
        /// </summary>
        [JsonPropertyName("consent_type")]
        public virtual string? ConsentType { get; set; }

        /// <summary>
        /// Gets or sets the display name associated with the current application.
        /// </summary>
        [JsonPropertyName("display_name")]
        public virtual string? DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the localized display names associated with the current application.
        /// </summary>
        [JsonPropertyName("display_names")]
        public virtual IReadOnlyDictionary<CultureInfo, string> DisplayNames { get; set; }
            = ImmutableDictionary.Create<CultureInfo, string>();

        /// <summary>
        /// Gets or sets the permissions associated with the current application.
        /// </summary>
        [JsonPropertyName("permissions")]
        public virtual IReadOnlyList<string> Permissions { get; set; } = ImmutableList.Create<string>();

        /// <summary>
        /// Gets or sets the logout callback URLs associated with the current application.
        /// </summary>
        [JsonPropertyName("post_logout_redirect_uris")]
        public virtual IReadOnlyList<string> PostLogoutRedirectUris { get; set; } = ImmutableList.Create<string>();

        /// <summary>
        /// Gets or sets the additional properties associated with the current application.
        /// </summary>
        [JsonPropertyName("properties")]
        public virtual JsonDocument? Properties { get; set; }

        /// <summary>
        /// Gets or sets the callback URLs associated with the current application.
        /// </summary>
        [JsonPropertyName("redirect_uris")]
        public virtual HashSet<string> RedirectUris { get; init; } = new();

        /// <summary>
        /// Gets or sets the requirements associated with the current application.
        /// </summary>
        [JsonPropertyName("requirements")]
        public virtual HashSet<string> Requirements { get; init; } = new();

        /// <summary>
        /// Gets or sets the application type
        /// associated with the current application.
        /// </summary>
        [JsonPropertyName("type")]
        public virtual string? Type { get; set; }
    }
}