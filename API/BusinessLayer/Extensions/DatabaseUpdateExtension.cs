using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CREL_BE.Entities;
using Microsoft.EntityFrameworkCore;

namespace CREL_BE.Extensions
{
    public static class DatabaseUpdateExtension
    {
        public static async Task<WebApplication> UpdateDatabaseAsync(this WebApplication app)
        {
            var services = app.Services.CreateScope().ServiceProvider;
            try
            {
                await services.GetRequiredService<CRELDBContext>().Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred during migration");
            }
            return app;
        }
    }

}
