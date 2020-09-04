using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EntityFrameworkCore;

namespace FacCigo.EntityFrameworkCore
{
    public class FacCigoWinUIMigrationsDbContext : AbpDbContext<FacCigoWinUIMigrationsDbContext>
    {
        public FacCigoWinUIMigrationsDbContext(DbContextOptions<FacCigoWinUIMigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ConfigureFacCigo();
        }
    }
}
