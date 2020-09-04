using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace FacCigo
{
    public class CurrencyNotSupportedForInvoiceException:BusinessException
    {
        public CurrencyNotSupportedForInvoiceException(string id)
            :base("F:003", $"The Currency {id} can not be used for invoice !")
        {

        }
    }
}
