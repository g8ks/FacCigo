using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace FacCigo.ViewModels.Invoices
{
    public  class InvoicesViewModel :BindableBase,ITransientDependency,IInvoicesViewModel
    {
    }
}
