using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace FacCigo
{
    public class Exam: AuditedAggregateRoot<Guid>
    {
        public virtual string ReferenceNo { get; protected set; }
        public virtual string Name { set; get; }
        public virtual Guid CategoryId { set; get; }
        public  Category Category { set; get; }
        public virtual decimal Price { set; get; }
        public virtual string CurrencyId { set; get; }
        public virtual Currency Currency { set; get; }
        public Collection<InvoiceLine> InvoiceLines { set; get; }
        protected Exam() { }
        public Exam(Guid id,string name, string referenceNo, decimal price,string currency,Guid categoryId)
        {
            Check.NotNull(referenceNo, nameof(referenceNo));
            Id = id;
            Name = name;
            ReferenceNo = referenceNo;
            Price = price;
            CurrencyId = currency;
            CategoryId = categoryId;
        }
        public Exam(Guid id, string name, string referenceNo, decimal price, string currency)
        {
            Check.NotNull(referenceNo, nameof(referenceNo));
            Id = id;
            Name = name;
            ReferenceNo = referenceNo;
            Price = price;
            CurrencyId = currency;
           
        }


    }
}
