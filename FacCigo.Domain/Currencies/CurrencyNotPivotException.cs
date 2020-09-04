using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace FacCigo
{
    public class CurrencyNotPivotException : BusinessException
    {
        public CurrencyNotPivotException(string id) : base("F:022", $"The Invoice {id} does not exist !")
        {

        }
    }
}
