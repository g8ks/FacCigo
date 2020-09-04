using EFCore.BulkExtensions;
using FacCigo.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace FacCigo.Exams
{
    public class ExamRepository: EfCoreRepository<FacCigoDbContext,Exam,Guid>,IExamRepository
    {
        public ExamRepository(IDbContextProvider<FacCigoDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public async Task<IList<Exam>> AddRange(IList<Exam> exams) {

             await DbContext.BulkInsertAsync(exams);
             var items = exams.Select(a => a.Id );
             return GetQueryable()
                   .Where(c=>items.Contains(c.Id))
                   .Select(c => c).ToList();
                
        }
    }
}
