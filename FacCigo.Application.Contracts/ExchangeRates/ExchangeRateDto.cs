using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FacCigo
{
    public class ExchangeRateDto: AuditedEntityDto<Guid>
    {
        [Required]
        public  DateTime ApplicableOn { set; get; }
        [Required]
        [StringLength(CurrencyConsts.MaxLengthCurrency)]
        public  string RefCurrencyId { set; get; }
        [Required]
        [StringLength(CurrencyConsts.MaxLengthCurrency)]
        public  string CurrencyId { set; get; }
        [Required]
        public  decimal Rate { set; get; }

        public bool IsActive { set; get; }
    }
}
