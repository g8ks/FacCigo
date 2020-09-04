using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace FacCigo.EntityFrameworkCore
{
    [DependsOn(
        typeof(FacCigoEntityFrameworkCoreModule)
        )]
    public class FacCigoEntityFrameworkCoreDbMigrationsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddAbpDbContext<FacCigoMigrationsDbContext>();
        }
    }
}
