using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FacCigo
{
    public class InvoiceInput
    {


        [Required]
        [StringLength(InvoiceConsts.MaxLengthReferenceNo)]
        public virtual string ReferenceNo {set; get; }
        [Required]
        public virtual Guid PatientId {set; get; }
        [Required]
        public virtual DateTime InvoiceDate { get; set; }
        public virtual InvoiceStatus Status { get; set; } = InvoiceStatus.PENDING;
        public virtual List<InvoiceLineInput> InvoiceLines { get; set; }
    
    }
    public class InvoiceLineInput
    {
        [Required]
        public Guid ExamId { get; set; }
    }
}
