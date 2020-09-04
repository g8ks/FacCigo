using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace FacCigo
{
    public class CategoryDto: EntityDto<Guid>
    {
        [Required]
        [StringLength(CategoryConsts.MaxLengthName)]
        public string Name { get; set; }
        public Guid? CategoryId { set; get; }
        public List<ExamDto> Exams { get; set;}
        public List<CategoryDto>SubCategories { get; set; }
        public string ShortName { get; set; }
    }
}
