using AspNetCore.Identity.CouchDB.Models;
using CouchDB.Driver.Exceptions;
using LetItGrow.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Polly;
using Polly.Retry;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace LetItGrow.Identity.Workers
{
    public class IdentityWorker : BackgroundService
    {
        private readonly UserManager<CouchDbUser> userManager;
        private readonly RoleManager<CouchDbRole> roleManager;
        private readonly IPrimaryKeyService primaryKey;

        private static readonly Lazy<AsyncRetryPolicy> retryPolicy = new(() =>
            Policy
            .Handle<CouchException>()
            .WaitAndRetryForeverAsync(i => TimeSpan.FromSeconds(30)));

        public IdentityWorker(UserManager<CouchDbUser> userManager, RoleManager<CouchDbRole> roleManager, IPrimaryKeyService primaryKey)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.primaryKey = primaryKey;
        }

        private static Task<T> Execute<T>(Func<Task<T>> func) => retryPolicy.Value.ExecuteAsync(func);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            const string username = "admin";

            if (await Execute(() => userManager.FindByNameAsync(username)) is not null)
                return;

            var user = new CouchDbUser(username) { Id = primaryKey.Create() };

            await Execute(() => userManager.CreateAsync(user, "Admin!1"));

            var admin = "Admin";
            await Execute(async () => await new[]
            {
                roleManager.CreateAsync(new(admin) { Id = primaryKey.Create() }),
                roleManager.CreateAsync(new("User") { Id = primaryKey.Create() })
            });

            await Execute(() => userManager.AddToRoleAsync(user, admin));
        }
    }
}