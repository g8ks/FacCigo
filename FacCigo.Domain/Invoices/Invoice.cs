using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace FacCigo
{
    public class Invoice : FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string ReferenceNo { protected set; get; }
        [NotNull]
        public virtual Guid PatientId { protected set; get; }
        public Patient Patient { set; get; }

        public virtual DateTime InvoiceDate { get; protected set; }
        [NotMapped]
        public virtual decimal TotalAmount { get; protected set; }
  
        public virtual InvoiceStatus Status { get; protected set; } = InvoiceStatus.PENDING;

        public virtual List<InvoiceLine> InvoiceLines { get; protected set; }

        public virtual Collection<Payment> Payments { set; get; }

        public virtual string CurrencyId { get; protected set; }
        public Currency Currency { get; set; }
        public virtual Guid ExchangeRateId { get; protected set; }
        public virtual ExchangeRate ExchangeRate { get; set; }
        protected Invoice() { }
        public Invoice(Guid id, string referenceNo, Guid patientId, DateTime date)
        {
            Id = id;
            ReferenceNo = referenceNo;
            PatientId = patientId;
            InvoiceDate = date;
            InvoiceLines = new List<InvoiceLine>();
        }

        internal void ChangeCurrency(string id)
        {
            CurrencyId = id;
        }
        internal void ChangeExchange(Guid exchnage)
        {
            ExchangeRateId = exchnage;
        }
        public void AddInvoiceLine(Guid examId)
        {
         
            var ext = InvoiceLines.FirstOrDefault(c => c.InvoiceId == Id && c.ExamId == examId);
            if (ext == null)
            {
                InvoiceLines.Add(new InvoiceLine(Id, examId));
            }
                    
        }
       
        internal void ChangeStatus(InvoiceStatus status)
        {
            Status = status;
        }
    }
    public class InvoiceLine : Entity
    {
        public virtual Guid InvoiceId { get; protected set; }
        public Invoice Invoice { get; set; }
        public virtual Guid ExamId { get; protected set; }
        public Exam Exam { get; set; }
        public virtual decimal Amount { get; protected set; }
        public virtual string CurrencyId { get; protected set; }
        public Currency Currency { get; set; }
        public InvoiceLine(Guid invoiceId,Guid examId) {
            InvoiceId = invoiceId;
            ExamId = examId;
        }
        internal InvoiceLine(Guid invoiceId,Guid examId,decimal baseAmount,string currency)
        {
            InvoiceId = invoiceId;
            ExamId = examId;
            Amount = baseAmount;
            CurrencyId = currency;       
        }
      
        public override object[] GetKeys()
        {
            return new object[] { InvoiceId, ExamId };
        }

        internal void ChangeAmount(decimal baseAmount)
        {
            Amount = baseAmount;
        }
        internal void ChangeCurrency(string currency)
        {
            CurrencyId = currency;
        }
    }
  
}
