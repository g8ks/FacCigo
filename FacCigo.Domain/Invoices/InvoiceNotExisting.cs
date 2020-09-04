using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp;

namespace FacCigo
{
    public class InvoiceNotExisting : BusinessException
    {
        public InvoiceNotExisting(Guid id) : base("F:003",$"The Invoice {id} does not exist !")
        {

        }
    }
}
