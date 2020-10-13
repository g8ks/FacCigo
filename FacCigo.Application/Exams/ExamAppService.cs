using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FacCigo.Exams
{
    public class ExamAppService:CrudAppService<Exam,ExamDto,Guid, ExamGetListInput, ExamInput>,IExamAppService
    {
        private readonly IExamRepository repo;
        public ExamAppService(IExamRepository repo) : base(repo)
        {
            this.repo = repo;
        }

      

        public override Task<ExamDto> CreateAsync(ExamInput input)
        {
            
            return base.CreateAsync(input);
        }

        public Task<string> NextReferenceNo(Guid CategoryId)
        {
            string RefNo = string.Format("{0:0000}", new Random().Next(0, 999));
            while (Repository.Any(c => c.ReferenceNo.Contains(RefNo) && c.CategoryId == CategoryId))
            {
                RefNo = string.Format("{0:0000}", new Random().Next(0, 999));
            }
            return Task.FromResult(RefNo);
        }

        public override Task<PagedResultDto<ExamDto>> GetListAsync(ExamGetListInput input)
        {
            return base.GetListAsync(input);
        }

        protected override IQueryable<Exam> CreateFilteredQuery(ExamGetListInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.Category!=null, t => t.Category.Name == input.Category)
                .WhereIf(input.ReferenceNo!=null,t=>t.ReferenceNo==input.ReferenceNo)
                .WhereIf(input.CategoryId.HasValue,t=>t.CategoryId==input.CategoryId.Value)
                .Include(c=>c.Category);
        }

        public async Task<IList<ExamDto>> AddBulk(List<ExamInput> inputs)
        {
            IList<Exam> exams = new List<Exam>();
            for(int i =0; i<inputs.Count; i++)
            {
                exams.Add(
                      new Exam(
                               GuidGenerator.Create(),
                               inputs[i].Name,
                               inputs[i].ReferenceNo,
                               inputs[i].Price,
                               inputs[i].CurrencyId
                             ));
            }

             exams= await repo.AddRange(exams);
             return ObjectMapper.Map<IList<Exam>,IList<ExamDto>>(exams);
            //inputs.
            //examDb.AddRange();
        }
    }
}
