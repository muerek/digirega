using DigiRega.Client.Auth;
using DigiRega.Client.Services;
using DigiRega.Client.Validation;
using DigiRega.Shared.Validation.StaffMember;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Client.Utilities
{
    /// <summary>
    /// Holds extension methods to register services with a <see cref="IServiceCollection"/> for DI.
    /// </summary>
    public static class ServicesExtensions
    {
        /// <summary>
        /// Registers data services with the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services"></param>
        public static void AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IEntryService, EntryService>();
            services.AddScoped<IRaceService, RaceService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IStaffService, StaffService>();
        }

        /// <summary>
        /// Registers custom authentication services with the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services"></param>
        public static void AddAppAuth(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            // These services may need to be scoped since they hold state.
            services.AddScoped<ISessionManager, SessionManager>();
            services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
            services.AddScoped<ITokenStorage, LocalTokenStorage>();
        }
        
        /// <summary>
        /// Registers client-side validator implementations with the <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services"></param>
        public static void AddFluentValidators(this IServiceCollection services)
        {
            services.AddTransient<IUsernameValidator, UsernameValidator>();
        }
    }
}
