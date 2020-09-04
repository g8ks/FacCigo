using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FacCigo.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class FacCigoMigrationsDbContextFactory : IDesignTimeDbContextFactory<FacCigoMigrationsDbContext>
    {
        public FacCigoMigrationsDbContext CreateDbContext(string[] args)
        {
            FacCigoEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();
            var conn = configuration.GetConnectionString("Default").Replace("~", Directory.GetCurrentDirectory());
            var builder = new DbContextOptionsBuilder<FacCigoMigrationsDbContext>()
                .UseSqlite(conn);
            
            return new FacCigoMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
