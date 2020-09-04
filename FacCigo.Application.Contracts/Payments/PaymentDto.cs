using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FacCigo
{
    public class PaymentDto:AuditedEntityDto<Guid>
    {
        public Guid InvoiceId { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaidCurrencyId { get; set; }
        public virtual Guid ExchangeRateApplied { get; set; }
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }
    }
}
