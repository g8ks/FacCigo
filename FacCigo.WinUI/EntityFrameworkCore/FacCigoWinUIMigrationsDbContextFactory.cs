using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FacCigo.EntityFrameworkCore
{
    public class FacCigoWinUIMigrationsDbContextFactory : IDesignTimeDbContextFactory<FacCigoWinUIMigrationsDbContext>
    {
        public FacCigoWinUIMigrationsDbContext CreateDbContext(string[] args)
        {
            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<FacCigoWinUIMigrationsDbContext>()
                .UseSqlite(configuration.GetConnectionString("FacCigo"));

            return new FacCigoWinUIMigrationsDbContext(builder.Options);
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
