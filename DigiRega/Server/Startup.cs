using AutoMapper;
using DigiRega.Server.Data;
using DigiRega.Server.Services;
using DigiRega.Server.Services.Exceptions;
using DigiRega.Server.Utilities;
using DigiRega.Shared.Validation.StaffMember;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using System.Text;

namespace DigiRega.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Read options from configuration.
            services.Configure<EmailingOptions>(options => Configuration.GetSection("Emailing").Bind(options));

            // Add data context.
            var connection = string.Format("Server={0};Port={1};Database={2};User Id={3};Password={4}",
                Configuration["Database:Server"] ?? "digirega.db",
                Configuration["Database:Port"] ?? "5432",
                Configuration["Database:DbName"] ?? "digirega",
                Configuration["Database:User"],
                Configuration["Database:Password"]);
            services.AddDbContext<DigiRegaDbContext>(cfg => cfg.UseNpgsql(connection));

            services.AddControllersWithViews(options =>
            {
                // Add handling for custom well-known service exceptions.
                options.Filters.Add<ServiceExceptionFilter>();
            })
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddStaffMemberValidator>());
            services.AddRazorPages();

            // Add AutoMapper for model-DTO mapping
            services.AddAutoMapper(typeof(Startup));

            // Add app services.
            services.AddAppServices();

            // Add server-side validators.
            services.AddFluentValidators();

            // The TokenHelper accesses the configuration and therefore has to be a service.
            services.AddSingleton<TokenHelper>();

            // Add background workers as hosted services.
            services.AddHostedService<BackgroundEmailSender>();

            // This helps to get information on the current user.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Add authentication with JWT tokens.
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSigningKey"])),
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.FromSeconds(30)
                    };
                });

            // Register Swagger generator.
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            // Enable middleware to serve swagger.json.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui.
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "DigiRega API");
            });

            app.UseRouting();

            // Add authentication and authorization, has to be here after routing and before endpoints.
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
