using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace FacCigo
{
    public interface IPatientAppService: ICrudAppService<PatientDto, Guid, PatientGetListInput, PatientInput>,IApplicationService
    {
        //Task<PatientDto> GetAsync
    }
}
