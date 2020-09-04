using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace FacCigo.EntityFrameworkCore
{
    [ConnectionStringName(FacCigoDbProperties.ConnectionStringName)]
    public interface IFacCigoDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}