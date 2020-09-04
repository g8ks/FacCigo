using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace FacCigo
{
    public class AmountNotMatchingException : BusinessException
    {
        public AmountNotMatchingException() : base("F:0005")
        {
        }
    }
}
