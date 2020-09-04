using Volo.Abp.Modularity;

namespace FacCigo
{
    [DependsOn(
        typeof(FacCigoDomainSharedModule)
        )]
    public class FacCigoDomainModule : AbpModule
    {

    }
}
