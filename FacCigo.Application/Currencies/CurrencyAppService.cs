using FacCigo.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FacCigo
{
    public class CurrencyAppService : CrudAppService<Currency,CurrencyDto,string>, ICurrencyAppService
    {
       
        public CurrencyAppService(ICurrencyRepository repository):base(repository)
        {
            LocalizationResource = typeof(FacCigoResource);
            ObjectMapperContext = typeof(FacCigoApplicationModule);

        }
      
    }
}
