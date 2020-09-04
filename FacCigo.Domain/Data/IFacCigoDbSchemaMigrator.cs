using System.Threading.Tasks;

namespace FacCigo.Data
{
    public interface IFacCigoDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
