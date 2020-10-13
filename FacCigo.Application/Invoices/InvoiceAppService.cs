using FacCigo.Exams;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;

namespace FacCigo.Invoices
{
    public class InvoiceAppService : CrudAppService<Invoice, InvoiceDto, Guid, InvoiceGetListInput,InvoiceInput>, IInvoiceAppService
    {
        private readonly InvoiceManager invoiceManager;
        private readonly IDataFilter<ISoftDelete> _softDeleteFilter;
        public InvoiceAppService(IRepository<Invoice,Guid> repo, InvoiceManager manager, IDataFilter<ISoftDelete> softDeleteFilter) : base(repo)
        {
            invoiceManager = manager;
            _softDeleteFilter = softDeleteFilter;
        }
        public override async Task<InvoiceDto> CreateAsync(InvoiceInput input)
        {
            Guid Id = GuidGenerator.Create();
            Invoice invoice = new Invoice(Id, input.ReferenceNo, input.PatientId, input.InvoiceDate);
            foreach (InvoiceLineInput line in input.InvoiceLines)
            {     
                invoice.AddInvoiceLine(line.ExamId);
            }
            invoice= await invoiceManager.CreateAsync(invoice);
            InvoiceDto dto = MapToGetOutputDto(invoice);
            return dto;
        }

        public  Task<string> NextReferenceNo(int year)
        {
             
            using (_softDeleteFilter.Disable())
            {
                string last = Repository.Where(c => c.InvoiceDate.Year == year)
                                  .OrderBy(c => c.ReferenceNo).LastOrDefault()?.ReferenceNo;
                int count = 1;
                string RefNo = string.Format("{0:00000}/CIGO/{1}", count, year);
                if (last != null) int.TryParse(last.Split("/")[0], out count);
                if ( Repository.Count() != 0)
                {
                
                        while (Repository.Any(c => c.ReferenceNo == RefNo))
                        {
                            count += 1;
                            RefNo = string.Format("{0:00000}/CIGO/{1}", count, year);
                        }
                
                }
                return Task.FromResult(RefNo);
            }
            
        }

        public override Task<InvoiceDto> UpdateAsync(Guid id, InvoiceInput input)
        {
            return base.UpdateAsync(id, input);
        }
        protected override IQueryable<Invoice> CreateFilteredQuery(InvoiceGetListInput input)
        {
            return base.CreateFilteredQuery(input)
                       .WhereIf(input.PatientId.HasValue,c=>c.PatientId==input.PatientId)
                       .WhereIf(input.InvoiceDate.HasValue,c=>c.InvoiceDate.Date.Equals(input.InvoiceDate.Value))
                       .Include(c=>c.Patient)
                       .Include(c=>c.Currency)
                       .Include(c=>c.ExchangeRate)
                       .Include(c => c.InvoiceLines).ThenInclude(c=>c.Exam);
        }

    }
}
