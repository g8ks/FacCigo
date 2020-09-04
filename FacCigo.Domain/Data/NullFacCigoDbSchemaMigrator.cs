using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace FacCigo.Data
{
    /* This is used if database provider does't define
     * IFacCigoDbSchemaMigrator implementation.
     */
    public class NullFacCigoDbSchemaMigrator : IFacCigoDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}