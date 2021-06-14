using BlazorPro.BlazorSize;
using CoreWasm;
using Flurl.Http;
using LetItGrow.Identity.Application.Services;
using LetItGrow.Identity.Common.Json;
using LetItGrow.Identity.Role.Services;
using LetItGrow.Identity.User.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace LetItGrow.Identity
{
    internal class Program
    {
        public static Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddCoreWeb();
            services.AddSingleton<IFlurlClient>(sp => new FlurlClient(builder.HostEnvironment.BaseAddress));
            services.AddSingleton<ApplicationService>();
            services.AddSingleton<UserService>();
            services.AddSingleton<RoleService>();
            services.AddSingleton<IMediaQueryService, MediaQueryService>();

            FlurlHttp.Configure(config =>
            {
                config.JsonSerializer = new SystemJsonSerializer();
            });

            services.AddOidcAuthentication(options =>
            {
                options.UserOptions.RoleClaim = "role";

                // Note: response_mode=fragment is the best option for a SPA. Unfortunately, the Blazor WASM
                // authentication stack is impacted by a bug that prevents it from correctly extracting
                // authorization error responses (e.g error=access_denied responses) from the URL fragment.
                // For more information about this bug, visit https://github.com/dotnet/aspnetcore/issues/28344.
                configuration.Bind("OpenId", options.ProviderOptions);
            });

            return builder.Build().RunAsync();
        }
    }
}