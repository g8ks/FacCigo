using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace FacCigo
{
    public class InvoiceCurrencyNotSettledException:BusinessException
    {
        public InvoiceCurrencyNotSettledException(string id) : base("F:055", $"The Invoice Currency {id} was nout found !")
        {

        }
    }
}
