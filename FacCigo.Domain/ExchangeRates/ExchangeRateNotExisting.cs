using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace FacCigo
{
     public class ExchangeRateNotExisting:BusinessException
    {
        public ExchangeRateNotExisting(Guid id):base("F:007",$"The ExchangeRate {id} does not exist !")
            {
            }
        public ExchangeRateNotExisting() : base("F:007", $"The ExchangeRate does not exist !")
        {
        }
    }
}
