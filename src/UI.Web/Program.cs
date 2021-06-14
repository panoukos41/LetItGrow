using CoreWasm;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LetItGrow.UI.Web
{
    public class Program
    {
        public static Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            var services = builder.Services;
            var configuration = builder.Configuration;

            services.AddCoreWeb();
            services.AddUI(configuration);

            services
                .AddSingleton<HubConnection>(sp => new HubConnectionBuilder()
                .WithUrl(new Uri(Path.Combine(configuration.GetService("api"), "v1/dashboard")), options =>
                {
                    options.AccessTokenProvider = async () =>
                    {
                        var provider = sp.CreateScope().ServiceProvider.GetRequiredService<IAccessTokenProvider>();
                        var result = await provider.RequestAccessToken();
                        AccessToken token;

                        while (!result.TryGetToken(out token))
                        {
                            Console.WriteLine("Getting token");
                            await Task.Delay(2000);
                            result = await provider.RequestAccessToken();
                        }
                        return token.Value;
                    };
                })
                .WithAutomaticReconnect()
                .Build());

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