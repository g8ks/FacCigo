using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace FacCigo
{
    [DependsOn(
        typeof(FacCigoDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class FacCigoApplicationContractsModule : AbpModule
    {

    }
}
