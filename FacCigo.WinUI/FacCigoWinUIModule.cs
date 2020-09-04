using FacCigo.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;

namespace FacCigo
{
    [DependsOn(
          typeof(FacCigoApplicationModule),
         typeof(FacCigoEntityFrameworkCoreModule), 
        typeof(AbpAutofacModule),
        typeof(AbpEntityFrameworkCoreSqliteModule))]
    public class FacCigoWinUIModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlite();
            });
        }
    }
}
