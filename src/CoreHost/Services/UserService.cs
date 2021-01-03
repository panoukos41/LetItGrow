using LetItGrow.Services;
using Microsoft.AspNetCore.Http;

namespace LetItGrow.CoreHost.Services
{
    /// <summary>
    /// todo: summary
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string? GetId() => httpContextAccessor
            .HttpContext
                ?.User
                ?.Identity
                ?.Name;
    }
}