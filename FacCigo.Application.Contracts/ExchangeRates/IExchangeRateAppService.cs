using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace FacCigo
{
    public interface IExchangeRateAppService :ICrudAppService<ExchangeRateDto, Guid, ExchangeRateGetListInput, decimal>
                                    , IApplicationService
    {
        public Task<ExchangeRateDto> CurrentExchangeRate();
    }
}
