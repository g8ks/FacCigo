using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace FacCigo
{
    public class PatientAppService:CrudAppService<Patient,PatientDto,Guid,PatientGetListInput,PatientInput>,IPatientAppService
    {
        public PatientAppService(IRepository<Patient,Guid> repo) : base(repo)
        {

        }
        public Task<IList<PatientDto>> GetListAsync()
        {
            var list = Repository.AsQueryable()
                     //  .Include(c => c.Invoices)
                       .ToList();
            return Task.FromResult(ObjectMapper.Map<IList<Patient>, IList<PatientDto>>(list));
        }
        protected override IQueryable<Patient> CreateFilteredQuery(PatientGetListInput input)
        {
            return base.CreateFilteredQuery(input)
                .WhereIf(input.FirstName != null, t => t.FirstName == input.FirstName)
                .WhereIf(input.LastName != null, t => t.LastName == input.LastName)
                .WhereIf(input.MiddleName != null, t => t.MiddleName == input.MiddleName)
                .WhereIf(input.PhoneNumber != null, t => t.PhoneNumber == input.PhoneNumber)
                .WhereIf(!string.IsNullOrEmpty(input.Search),t=>(t.FirstName.Contains(input.Search)
                           ||t.LastName.Contains(input.LastName))||t.PhoneNumber.Contains(input.Search))
                .Include(c=>c.Invoices);
        }
    }
}
