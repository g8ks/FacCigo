using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FacCigo.Data;
using Volo.Abp.DependencyInjection;

namespace FacCigo.EntityFrameworkCore
{
    public class EntityFrameworkCoreFacCigoDbSchemaMigrator
        : IFacCigoDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreFacCigoDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the FacCigoMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<FacCigoMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}