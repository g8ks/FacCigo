using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace FacCigo
{
    public class ExchangeRate:FullAuditedAggregateRoot<Guid>
    {
        public virtual DateTime ApplicableOn { protected set; get; }
        public virtual string RefCurrencyId { protected  set; get; }
        public Currency RefCurrency { set; get; }
        public virtual string CurrencyId { protected set; get; }
        public Currency Currency { set; get; }
        public virtual decimal Rate { protected  set; get; }
        public bool IsActive { get;set; }
        protected ExchangeRate() { }
        public ExchangeRate(Guid id,string Ref,string to,DateTime date,decimal rate)
        {
            RefCurrencyId = Ref;
            CurrencyId = to;
            ApplicableOn = date;
            Rate = rate;
           
        }
    }
}
