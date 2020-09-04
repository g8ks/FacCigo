using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace FacCigo
{
    public interface IExamRepository:IRepository<Exam,Guid>
    {
        public Task<IList<Exam>> AddRange(IList<Exam> exams);
    }
}
