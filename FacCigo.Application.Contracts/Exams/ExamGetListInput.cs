using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FacCigo
{
   public class ExamGetListInput: PagedAndSortedResultRequestDto
    {
        public string Category { get; set; }
        public string ReferenceNo { get; set; }
        public Guid? CategoryId { get; set; }
    }
}
