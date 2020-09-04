using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FacCigo
{
    public static class InvoiceConsts
    {
        public  const  int MaxLengthReferenceNo = 15;
        public  static string InvoiceStatusElements = string.Join(",", Enum.GetValues(typeof(InvoiceStatus)).Cast<InvoiceStatus>().ToList().Select(e => (int)e));
    }
}
