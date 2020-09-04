using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text;

namespace FacCigo
{
    public class ExamInput
    {
        [Required]
        [StringLength(ExamConsts.MaxLengthReferenceNo)]
        public string ReferenceNo { get; set; }
        [Required]
        [StringLength(ExamConsts.MaxLengthName)]
        public string Name { set; get; }
        public Guid CategoryId { set; get; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { set; get; }
        [Required]
        public string CurrencyId { set; get; }
      
    }

}
