using FacCigo.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FacCigo
{
    public class ExchangeRateAppService: CrudAppService<ExchangeRate, ExchangeRateDto, Guid, ExchangeRateGetListInput, decimal>, IExchangeRateAppService
    {
        
        private readonly ExchangeRateManager ExchangeRateManager;
        public ExchangeRateAppService(IRepository<ExchangeRate,Guid> repo,ExchangeRateManager manager) : base(repo)
        {
           
            LocalizationResource = typeof(FacCigoResource);
            ObjectMapperContext = typeof(FacCigoApplicationModule);
            ExchangeRateManager = manager;
        }
        public override async Task<ExchangeRateDto> CreateAsync(decimal Rate)
        {
            ExchangeRate exchange = await ExchangeRateManager.CreateAsync(Rate);
            return MapToGetOutputDto(exchange);
        }

        public async Task<ExchangeRateDto> CurrentExchangeRate()
        {
            ExchangeRate exchange = await ExchangeRateManager.GetCurrentExchangeRate();
            return MapToGetOutputDto(exchange);
        }

        public override async Task<ExchangeRateDto> UpdateAsync(Guid id, decimal input)
        {
            ExchangeRate exchange = await ExchangeRateManager.CreateAsync(input);
            return MapToGetOutputDto(exchange);
        }
        protected override IQueryable<ExchangeRate> CreateFilteredQuery(ExchangeRateGetListInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.IsActive.HasValue, t => t.IsActive== input.IsActive.Value);
        }
    }
}
