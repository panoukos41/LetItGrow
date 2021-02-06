using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NodaTime;
using NodaTime.Serialization.SystemTextJson;
using System;
using System.IO;
using System.Threading.Tasks;

namespace LetItGrow.UI.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");

            ConfigureServices(builder.Services, builder.Configuration);

            // Start the app.
            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<HubConnection>(sp => new HubConnectionBuilder()
                .WithUrl(new Uri(Path.Combine(configuration.GetService("api"), "api/v1/dashboard")), options =>
                {
                    options.AccessTokenProvider = async () =>
                    {
                        // s is the ServiceProvider
                        var provider = sp.CreateScope().ServiceProvider.GetRequiredService<IAccessTokenProvider>();
                        var result = await provider.RequestAccessToken();
                        AccessToken token;

                        while (!result.TryGetToken(out token))
                        {
                            await Task.Delay(2000);
                            result = await provider.RequestAccessToken();
                        }

                        return token.Value;
                    };
                })
                .AddJsonProtocol(o => o.PayloadSerializerOptions.ConfigureForNodaTime(DateTimeZoneProviders.Tzdb))
                .WithAutomaticReconnect()
                .Build());

            services.AddOidcAuthentication(options =>
            {
                var openid = configuration.GetSection("openid");

                options.ProviderOptions.ClientId = openid["clientid"]; //"blazor";
                options.ProviderOptions.Authority = openid["authority"]; //"https://localhost:6001/";
                options.ProviderOptions.ResponseType = "code";

                // Note: response_mode=fragment is the best option for a SPA. Unfortunately, the Blazor WASM
                // authentication stack is impacted by a bug that prevents it from correctly extracting
                // authorization error responses (e.g error=access_denied responses) from the URL fragment.
                // For more information about this bug, visit https://github.com/dotnet/aspnetcore/issues/28344.
                options.ProviderOptions.ResponseMode = "query";
                options.ProviderOptions.PostLogoutRedirectUri = "https://localhost:5001";
            });

            // Add UI required services.
            services.AddUI(configuration);
        }
    }
}