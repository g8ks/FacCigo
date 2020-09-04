using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace FacCigo
{
    public interface IPaymentRepository:IRepository<Payment,Guid>
    {
    }
}
