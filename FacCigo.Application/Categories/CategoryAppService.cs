using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FacCigo.Categories
{
    public class CategoryAppService:CrudAppService<Category,CategoryDto,Guid>,ICategoryAppService
    {
        public CategoryAppService(IRepository<Category,Guid> repo) : base(repo)
        {

        }
    }
}
