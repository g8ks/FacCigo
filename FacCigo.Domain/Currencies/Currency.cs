using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace FacCigo
{
    public class Currency : Entity<string>
    {
        public virtual string Name { set; get; }
        public virtual bool IsPivot { set; get; } = false;
        public Collection<Exam> Exams { set; get;}
        public Collection<InvoiceLine> InvoiceLines { get; set; }
        public Collection<Invoice> Invoices { get; set; }
        public Collection<ExchangeRate> RefExchangeRates { set; get; }
        public Collection<ExchangeRate> ExchangeRates { set; get; }
        public Collection<Payment> Payments { set; get; }
        public Currency()
        {

        }
        public Currency(string id)
        {
            this.Id = id;
        }
    }
}
