using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace FacCigo
{
    public interface ICurrencyAppService :  IApplicationService,ICrudAppService<CurrencyDto,string>
    {
    }
}
