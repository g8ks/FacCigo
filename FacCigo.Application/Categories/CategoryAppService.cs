using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FacCigo.Categories
{
    public class CategoryAppService:CrudAppService<Category,CategoryDto,Guid>,ICategoryAppService
    {
        public CategoryAppService(IRepository<Category,Guid> repo) : base(repo)
        {

        }

        public Task<IList<CategoryDto>> GetListAsync()
        {
            var list = Repository.AsQueryable()
                      .Include(c=>c.Exams)
                      .ToList();
            return Task.FromResult(ObjectMapper.Map<IList<Category>, IList<CategoryDto>>(list));
        }
    }
}
