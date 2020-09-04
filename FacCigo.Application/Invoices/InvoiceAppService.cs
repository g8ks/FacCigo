using FacCigo.Exams;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace FacCigo.Invoices
{
    public class InvoiceAppService : CrudAppService<Invoice, InvoiceDto, Guid, InvoiceGetListInput,InvoiceInput>, IInvoiceAppService
    {
        private readonly InvoiceManager invoiceManager;
        public InvoiceAppService(IInvoiceRepository repo, InvoiceManager manager) : base(repo)
        {
            invoiceManager = manager;
        }
        public override async Task<InvoiceDto> CreateAsync(InvoiceInput input)
        {
            Guid Id = GuidGenerator.Create();
            string RefNo = input?.ReferenceNo+ Id.ToString().Left(4);
            Invoice invoice = new Invoice(Id, RefNo, input.PatientId, input.InvoiceDate);
            foreach (InvoiceLineInput line in input.InvoiceLines)
            {     
                invoice.AddInvoiceLine(line.ExamId);
            }
            invoice= await invoiceManager.CreateAsync(invoice);
            InvoiceDto dto = MapToGetOutputDto(invoice);
            return dto;
        }
        public override Task<InvoiceDto> UpdateAsync(Guid id, InvoiceInput input)
        {
            return base.UpdateAsync(id, input);
        }
        protected override IQueryable<Invoice> CreateFilteredQuery(InvoiceGetListInput input)
        {
            return base.CreateFilteredQuery(input)
                       .Include(c=>c.Patient)
                       .Include(c=>c.InvoiceLines)
                       .Include(c=>c.Currency)
                       .Include(c=>c.ExchangeRate);
        }

    }
}
