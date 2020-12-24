using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace LetItGrow.Identity
{
    public class Worker : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public Worker(IServiceProvider serviceProvider)
            => _serviceProvider = serviceProvider;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var api = scope.ServiceProvider.GetRequiredService<IConfiguration>().GetService("blazor");
            var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

            if (await manager.FindByClientIdAsync("blazor", cancellationToken) is not null)
                return;

            await manager.CreateAsync(
                cancellationToken: cancellationToken,
                descriptor: new OpenIddictApplicationDescriptor
                {
                    ClientId = "blazor",
                    ConsentType = ConsentTypes.Explicit,
                    DisplayName = "Blazor client application",
                    Type = ClientTypes.Public,
                    RedirectUris =
                    {
                        //new Uri("https://localhost:5001/authentication/login-callback")
                        new Uri(api,"/authentication/login-callback")
                    },
                    PostLogoutRedirectUris =
                    {
                        //new Uri("https://localhost:5001/authentication/logout-callback")
                        new Uri(api,"/authentication/logout-callback")
                    },
                    Permissions =
                    {
                        Permissions.Endpoints.Authorization,
                        Permissions.Endpoints.Logout,
                        Permissions.Endpoints.Token,
                        Permissions.GrantTypes.AuthorizationCode,
                        Permissions.GrantTypes.RefreshToken,
                        Permissions.ResponseTypes.Code,
                        Permissions.Scopes.Email,
                        Permissions.Scopes.Profile,
                        Permissions.Scopes.Roles
                    },
                    Requirements =
                    {
                        Requirements.Features.ProofKeyForCodeExchange
                    }
                });
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}