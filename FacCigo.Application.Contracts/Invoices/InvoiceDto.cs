using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FacCigo
{
    public class InvoiceDto:FullAuditedEntityDto<Guid>
    {
      
        public virtual string ReferenceNo { set; get; }
        public virtual Guid PatientId { set; get; }
        public virtual string PatientName { set; get; }
        public virtual DateTime InvoiceDate { get; set; }
        public virtual decimal TotalAmount { get; set; }
        public virtual InvoiceStatus Status { get; set; } 
        public virtual List<InvoiceLineDto> InvoiceLines { get; set; }
        public virtual string CurrencyId { get;set; }
        public virtual Guid ExchangeRateId { get;set; }
        public virtual decimal Rate { get; set; }
    }

    public class InvoiceLineDto
    {
        public Guid InvoiceID { get; set; }
        public Guid ExamId { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyId { get; set; }
        public decimal ConvertedAmount { get;set; }
    }
}
