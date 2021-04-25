using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigiRega.Server.Utilities
{
    /// <summary>
    /// Holds extension methods for <see cref="IHost"/> to be used during app startup.
    /// </summary>
    public static class HostExtensions
    {
        /// <summary>
        /// Applies EF database migrations.
        /// </summary>
        /// <param name="host"></param>
        /// <returns></returns>
        public static IHost ApplyDatabaseMigrations<TDbContext>(this IHost host) where TDbContext : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                try
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();
                    dbContext.Database.Migrate();
                }
                catch (Exception)
                {
                    // TODO: Add logging here.
                    throw;
                }
            }

            return host;
        }
    }
}
