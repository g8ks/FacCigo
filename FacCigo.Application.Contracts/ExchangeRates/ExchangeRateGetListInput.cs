using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FacCigo
{
    public class ExchangeRateGetListInput:PagedAndSortedResultRequestDto
    {
        public bool? IsActive { get; set; }
    }
}
