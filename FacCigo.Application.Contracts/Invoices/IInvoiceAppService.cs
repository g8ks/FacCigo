using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace FacCigo
{
    public interface IInvoiceAppService : ICrudAppService<InvoiceDto, Guid, InvoiceGetListInput, InvoiceInput>,IApplicationService
    {
        Task<string> NextReferenceNo(int year);
    }
}
