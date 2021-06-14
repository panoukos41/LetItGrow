using LetItGrow.Services;
using Microsoft.AspNetCore.Http;

namespace LetItGrow.CoreHost.Services
{
    /// <summary>
    /// Implementation of <see cref="IUserService"/>
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc/>
        public string? GetId() =>
            httpContextAccessor.HttpContext
                ?.User
                ?.Identity
                ?.Name;
    }
}