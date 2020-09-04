using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FacCigo.Payments
{
    public class PaymentGetListInput:PagedAndSortedResultRequestDto
    {
        public string Currency { get; set; }
        public string InvoiceRef { get; set;}
        public Guid  InvoiceId { get; set; }
        public DateTime? Date { get; set; }
    }
}
