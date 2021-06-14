using CouchDB.Driver.Exceptions;
using Flurl;
using LetItGrow.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenIddict.Core;
using OpenIddict.CouchDB.Models;
using Polly;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using static OpenIddict.Abstractions.OpenIddictConstants;

namespace LetItGrow.Identity.Workers
{
    public class OpenIddictWorker : BackgroundService
    {
        private readonly IConfiguration configuration;
        private readonly OpenIddictApplicationManager<CouchDbOpenIddictApplication> manager;
        private readonly IPrimaryKeyService primaryKey;

        private static readonly Lazy<AsyncRetryPolicy> retryPolicy = new(() =>
            Policy
            .Handle<CouchException>()
            .WaitAndRetryForeverAsync(i => TimeSpan.FromSeconds(30)));

        private static Task Execute(Func<Task> func) => retryPolicy.Value.ExecuteAsync(func);

        private static Task<T> Execute<T>(Func<Task<T>> func) => retryPolicy.Value.ExecuteAsync(func);

        public OpenIddictWorker(IConfiguration configuration, OpenIddictApplicationManager<CouchDbOpenIddictApplication> manager, IPrimaryKeyService primaryKey)
        {
            this.configuration = configuration;
            this.manager = manager;
            this.primaryKey = primaryKey;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            if (await Execute(() => manager.FindByClientIdAsync("blazor", stoppingToken).AsTask()) is not null)
                return;

            var api = configuration.GetService("blazor");
            var identity = "https://localhost:5021";

            await new[]
            {
                Execute(() => manager.CreateAsync(
                    cancellationToken: stoppingToken,
                    application: new()
                    {
                        Id = primaryKey.Create(),
                        ClientId = "blazor",
                        ConsentType = ConsentTypes.Explicit,
                        DisplayName = "Blazor client application",
                        Type = ClientTypes.Public,
                        RedirectUris = ImmutableList(new()
                        {
                            new Url(api).AppendPathSegments("authentication", "login-callback").ToString()
                        }),
                        PostLogoutRedirectUris = ImmutableList(new()
                        {
                            new Url(api).AppendPathSegments("authentication", "logout-callback").ToString()
                        }),
                        Permissions = ImmutableList(new()
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
                        }),
                        Requirements = ImmutableList(new()
                        {
                            Requirements.Features.ProofKeyForCodeExchange
                        })
                    }).AsTask()),

                Execute(() => manager.CreateAsync(
                    cancellationToken: stoppingToken,
                    application: new()
                    {
                        Id = primaryKey.Create(),
                        ClientId = "identity",
                        ConsentType = ConsentTypes.Explicit,
                        DisplayName = "Identity client application",
                        Type = ClientTypes.Public,
                        RedirectUris = ImmutableList(new()
                        {
                            new Url(identity).AppendPathSegments("authentication", "login-callback").ToString(),
                        }),
                        PostLogoutRedirectUris = ImmutableList(new()
                        {
                            new Url(identity).AppendPathSegments("authentication", "logout-callback").ToString()
                        }),
                        Permissions = ImmutableList(new()
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
                        }),
                        Requirements = ImmutableList(new()
                        {
                            Requirements.Features.ProofKeyForCodeExchange
                        })
                    }).AsTask())
            };

            static ImmutableList<string> ImmutableList(HashSet<string> set) => set.ToImmutableList();
        }
    }
}