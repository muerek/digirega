using DigiRega.Server.Services;
using DigiRega.Server.Validators;
using DigiRega.Shared.Validation.StaffMember;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Utilities
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
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IClubService, ClubService>();
            services.AddScoped<IRaceService, RaceService>();
            services.AddScoped<IEntryService, EntryService>();
            services.AddScoped<IEmailingQueueService, EmailingQueueService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IStaffService, StaffService>();
        }
        
        /// <summary>
        /// Registers validators for FluentValidation to the service collection.
        /// </summary>
        /// <param name="services"></param>
        public static void AddFluentValidators(this IServiceCollection services)
        {
            services.AddScoped<IUsernameValidator, UsernameValidator>();
        }
    }
}
