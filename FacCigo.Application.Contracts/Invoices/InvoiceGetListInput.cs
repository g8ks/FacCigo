using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FacCigo.Invoices
{
    public class InvoiceGetListInput : PagedAndSortedResultRequestDto
    {
        public Guid PatientId { get; set; }
        public string PatientName { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string ReferenceNo { get; set; }
    }
}
