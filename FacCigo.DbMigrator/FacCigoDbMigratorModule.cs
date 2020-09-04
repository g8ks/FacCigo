using FacCigo.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace FacCigo.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(FacCigoEntityFrameworkCoreDbMigrationsModule),
        typeof(FacCigoApplicationContractsModule)
        )]
    public class FacCigoDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
