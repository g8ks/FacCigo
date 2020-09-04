using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace FacCigo
{
    public class Payment: AuditedEntity<Guid>
    {
        public virtual Guid InvoiceId { get; protected set; }
        public Invoice Invoice { get; set; }
        public virtual decimal AmountPaid { get; protected set; }
        public virtual string  PaidCurrencyId { get; protected set; }
        public Currency PaidCurrency { get; set; }
        public virtual Guid? ExchangeRateApplied { get; protected set; }
        public virtual DateTime PaymentDate { get; set; }
        public Payment() { }
        public Payment(Guid id, Guid invoiceId,DateTime paymentDate)
        {
            Id = id;
            InvoiceId = invoiceId;
            PaymentDate = paymentDate;
        }
        public Payment(Guid id, Guid invoiceId,decimal amountPaid,string paidCurrencyId,Guid exchangeRate,DateTime payementDate)
        {
            Id = id;
            InvoiceId = invoiceId;
            AmountPaid = amountPaid;
            PaidCurrencyId = paidCurrencyId;
            ExchangeRateApplied = exchangeRate;
            PaymentDate = payementDate;
        }
    }
}
