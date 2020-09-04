using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FacCigo
{
    public class CurrencyDto: EntityDto<string>
    {
        [Required]
        [StringLength(CurrencyConsts.MaxLengthName)]
        public string Name { set; get; }
        [Required]
        public bool IsPivot { set; get; } = false;
    }
}
