using FacCigo.Localization;
using Volo.Abp.Application.Services;

namespace FacCigo
{
    public abstract class FacCigoAppService : ApplicationService
    {
        protected FacCigoAppService()
        {
            LocalizationResource = typeof(FacCigoResource);
            ObjectMapperContext = typeof(FacCigoApplicationModule);
        }
    }
}
