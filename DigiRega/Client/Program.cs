using Blazored.LocalStorage;
using Blazored.Modal;
using DigiRega.Client.Auth;
using DigiRega.Client.Services;
using DigiRega.Client.Utilities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DigiRega.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            // Keep HTTP scoped so the authentication header is kept.
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress + "api/") });

            // Add custom services.
            builder.Services.AddAppServices();

            // Add authorization.
            builder.Services.AddApiAuthorization();

            // Add custom services for authentication.
            builder.Services.AddAppAuth();

            // Add client-side validators.
            builder.Services.AddFluentValidators();

            // Add access to local storage, this is a dependency of the token storage.
            builder.Services.AddBlazoredLocalStorage();

            // Used to show dialogs in in-page modals.
            builder.Services.AddBlazoredModal();

            var host = builder.Build();

            // Try to pick up remnants of previous sessions or clean up.
            // This is neccessary to keep client and server in sync.
            // Otherwise, the client may assume authentication based on outdated information.
            var sessionManager = host.Services.GetRequiredService<ISessionManager>();
            await sessionManager.TryRestartAsync();

            await host.RunAsync();
        }
    }
}
