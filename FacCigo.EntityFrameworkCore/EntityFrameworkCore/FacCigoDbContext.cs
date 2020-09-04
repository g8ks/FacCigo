using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace FacCigo.EntityFrameworkCore
{
    [ConnectionStringName(FacCigoDbProperties.ConnectionStringName)]
    public class FacCigoDbContext : AbpDbContext<FacCigoDbContext>, IFacCigoDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Patient>Patients { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Category> Categories { get; set; }
        
        public FacCigoDbContext(DbContextOptions<FacCigoDbContext> options) 
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ConfigureFacCigo();
        }
      
    }
}