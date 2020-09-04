using Prism.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace FacCigo.ViewModels.Invoices
{
    public class InvoiceInputViewModel:ValidatableBindableBase,IInvoiceInputViewModel,ITransientDependency
    {
        public InvoiceInputViewModel()
        {

        }
    }
}
