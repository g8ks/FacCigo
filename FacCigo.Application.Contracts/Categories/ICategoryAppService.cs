using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace FacCigo.Categories
{
    public interface ICategoryAppService:IApplicationService,ICrudAppService< CategoryDto, Guid>
    {
    }
}
