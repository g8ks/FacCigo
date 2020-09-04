using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FacCigo
{
    public class ExamDto : AuditedEntityDto<Guid>
    {
        public string ReferenceNo { get;set; }
        public string Name { set; get; }
        public string Category { set; get; }
        public Guid CategoryId { set; get; }
        public decimal Price { set; get; }
        public string CurrencyId {set; get; }
    }
}
