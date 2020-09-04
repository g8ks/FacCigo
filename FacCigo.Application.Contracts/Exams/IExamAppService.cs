using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace FacCigo
{
    public interface IExamAppService:ICrudAppService< ExamDto, Guid, ExamGetListInput, ExamInput> ,IApplicationService
    {
        Task<string> NextReferenceNo(Guid CategoryId);
        Task<IList<ExamDto>> AddBulk(List<ExamInput> inputs);
    }
}
