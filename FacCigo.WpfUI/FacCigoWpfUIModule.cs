using CSVLibrary;
using FacCigo.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Sqlite;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;

namespace FacCigo
{
    [DependsOn(
          typeof(FacCigoApplicationModule),
         typeof(FacCigoEntityFrameworkCoreModule), 
        typeof(AbpAutofacModule),
        typeof(AbpEntityFrameworkCoreSqliteModule))]
    public class FacCigoWpfUIModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            //var conn = configuration.GetConnectionString("MyDB").Replace("~", Directory.GetCurrentDirectory());
            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlite();
            });
            context.Services.AddSingleton<EventAggregator>(new EventAggregator());
            context.Services.AddScoped<IEventAggregator>(sp => sp.GetRequiredService<EventAggregator>());
           

        }
    }
}
