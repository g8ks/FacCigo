using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FacCigo
{
    public class PatientDto : FullAuditedEntityDto<Guid>
    {
        [Required]
        [StringLength(PatientConsts.MaxNameLength)]
        public virtual string FirstName { set; get; }
        [Required]
        [StringLength(PatientConsts.MaxNameLength)]
        public virtual string LastName { set; get; }
        [StringLength(PatientConsts.MaxNameLength)]
        public virtual string MiddleName { set; get; }

        [DataType(DataType.Date)]
        public virtual DateTime? BirthDate { set; get; }

        [StringLength(PatientConsts.MaxAddressLength)]
        public virtual string Address { set; get; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(PatientConsts.MaxPhoneNumberLength)]
        public virtual string PhoneNumber { set; get; }
        
        public virtual List<InvoiceDto> Invoices { set; get; }

        public string Name { set; get; }
    }
}
