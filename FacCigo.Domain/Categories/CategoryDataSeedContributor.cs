using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace FacCigo.Categories
{
    public class CategoryDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Category, Guid> _categoryRepository;
        private readonly IGuidGenerator _guidGenerator;

        public CategoryDataSeedContributor(IRepository<Category, Guid> categoryRepository,IGuidGenerator guidGenerator)
        {
            _categoryRepository = categoryRepository;
            _guidGenerator = guidGenerator;

        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _categoryRepository.GetCountAsync() > 0)
            {
                return;
            }
            await _categoryRepository.InsertAsync(new Category(
                       _guidGenerator.Create(),"Echographie","ECHO"
                ), autoSave: true);
            await _categoryRepository.InsertAsync(new Category(
                      _guidGenerator.Create(), "Radio","RX"
               ), autoSave: true);
            await _categoryRepository.InsertAsync(new Category(
                     _guidGenerator.Create(), "Scanner","SCAN"
              ),autoSave:true);
        }
    }
}
