using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace FacCigo
{
    public class Patient: FullAuditedAggregateRoot<Guid>
    {
        [NotNull]
        public virtual string FirstName { set; get;}
        [NotNull]
        public virtual string LastName { set; get; }
      
        public virtual string MiddleName { set; get; }

        public virtual DateTime? BirthDate { set; get; }

        public virtual string Address { set; get; }

        public virtual string PhoneNumber { set; get; }

        public IList<Invoice> Invoices { set; get; }
        [NotMapped]
        public string Name { get { return FirstName + ' ' + LastName; } }
    }
}
