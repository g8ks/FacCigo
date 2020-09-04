using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace FacCigo
{
    public class ExchangeRateNotApplicable : BusinessException
    {
        public ExchangeRateNotApplicable(Guid id) : base("F:010", $"The ExchangeRate {id} is not Applicable !")
        {

        }
    }
}
