using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace FacCigo.EntityFrameworkCore
{
    public static class FacCigoDbContextModelCreatingExtensions
    {
        public static void ConfigureFacCigo(
            this ModelBuilder builder,
            Action<FacCigoModelBuilderConfigurationOptions> optionsAction = null)
        {
            Check.NotNull(builder, nameof(builder));

            var options = new FacCigoModelBuilderConfigurationOptions(
                FacCigoDbProperties.DbTablePrefix,
                FacCigoDbProperties.DbSchema
            );

            optionsAction?.Invoke(options);

            /* Configure all entities here. Example:

            builder.Entity<Question>(b =>
            {
                //Configure table & schema name
                b.ToTable(options.TablePrefix + "Questions", options.Schema);
            
                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(QuestionConsts.MaxTitleLength);
                
                //Relations
                b.HasMany(question => question.Tags).WithOne().HasForeignKey(qt => qt.QuestionId);

                //Indexes
                b.HasIndex(q => q.CreationTime);
            });
            */
            builder.Entity<Exam>(b => {
                b.ToTable(options.TablePrefix + "Exams", options.Schema);
                b.ConfigureByConvention();
                b.Property(q => q.CurrencyId).IsRequired().HasMaxLength(CurrencyConsts.MaxLengthCurrency);
                b.Property(q => q.Name).IsRequired().HasMaxLength(ExamConsts.MaxLengthName);
                b.Property(q => q.CategoryId).IsRequired();
                b.Property(q => q.ReferenceNo).IsRequired().HasMaxLength(ExamConsts.MaxLengthReferenceNo);
                b.Property(q => q.Price).IsRequired().HasColumnType("decimal(18,2)");
                b.HasOne(c=>c.Currency).WithMany(b=>b.Exams).HasForeignKey(c=>c.CurrencyId).IsRequired();
                b.HasOne(c => c.Category).WithMany(b => b.Exams).HasForeignKey(c => c.CategoryId).IsRequired();
                b.HasIndex(q => q.CreationTime);
                b.HasIndex(q => q.Name).IsUnique();
                b.HasIndex(q => q.ReferenceNo).IsUnique();
                b.HasKey(q => q.Id);
            });
            builder.Entity<Patient>(b => {
                b.ToTable(options.TablePrefix + "Patients", options.Schema);
                b.ConfigureByConvention();
                b.Property(q => q.FirstName).IsRequired().HasMaxLength(PatientConsts.MaxNameLength);
                b.Property(q => q.LastName).IsRequired().HasMaxLength(PatientConsts.MaxNameLength);
                b.Property(q => q.MiddleName).IsRequired(false).HasMaxLength(PatientConsts.MaxNameLength);
                b.Property(q => q.BirthDate).IsRequired().HasColumnType("date");
                b.Property(q => q.Address).HasMaxLength(PatientConsts.MaxAddressLength);
                b.Property(q => q.PhoneNumber).HasMaxLength(PatientConsts.MaxPhoneNumberLength);
                b.HasIndex(q =>new { q.FirstName,q.LastName,q.MiddleName }).IsUnique();
                b.HasKey(q => q.Id);
                b.HasMany(c => c.Invoices).WithOne();

            });
            builder.Entity<Currency>(b =>
            {
                b.ToTable(options.TablePrefix + "Currencies", options.Schema);
                b.ConfigureByConvention();
                b.Property(q => q.Name).IsRequired().HasMaxLength(CurrencyConsts.MaxLengthName);
                b.Property(q => q.Id).IsRequired().HasMaxLength(CurrencyConsts.MaxLengthCurrency);
                b.Property(q => q.IsPivot).HasDefaultValue(false);
                b.HasKey(q => q.Id);
            });
            builder.Entity<Invoice>(b =>
            {
                b.ToTable(options.TablePrefix + "Invoices", options.Schema);
                b.ConfigureByConvention();
                b.Property(q => q.ReferenceNo).IsRequired().HasMaxLength(InvoiceConsts.MaxLengthReferenceNo);
                b.Property(q => q.PatientId).IsRequired();
                b.Property(q => q.InvoiceDate).IsRequired();
                b.Property(q => q.TotalAmount).IsRequired().HasColumnType("decimal(18,2)");
                b.Property(q => q.Status).IsRequired().HasConversion<int>();
                b.HasOne(q => q.Patient).WithMany(t => t.Invoices).HasForeignKey(q => q.PatientId).IsRequired();
                b.HasOne(q => q.Currency).WithMany(t=>t.Invoices).HasForeignKey(q => q.CurrencyId).IsRequired();
                b.HasCheckConstraint(
                    "CK_Status_InvoiceStatus", "[Status] IN ( "+ InvoiceConsts.InvoiceStatusElements+" )");
                b.HasIndex(q => q.ReferenceNo).IsUnique();
                b.HasIndex(q => q.CreationTime);
                b.HasKey(q => q.Id);
            });
            builder.Entity<InvoiceLine>(b => {
                b.ToTable(options.TablePrefix + "InvoiceLines", options.Schema);
                b.ConfigureByConvention();
                b.Property(q => q.Amount).IsRequired().HasColumnType("decimal(18,2)");
                b.HasOne(q => q.Currency).WithMany(q => q.InvoiceLines).HasForeignKey(q => q.CurrencyId);
                b.HasOne(q => q.Invoice).WithMany(q => q.InvoiceLines).HasForeignKey(q => q.InvoiceId);
                b.HasOne(q => q.Exam).WithMany(t => t.InvoiceLines).HasForeignKey(pt => pt.ExamId);
                b.HasKey(q => new { q.InvoiceId,q.ExamId });
            });
            builder.Entity<ExchangeRate>(b =>
            {
                b.ToTable(options.TablePrefix + "ExchangeRates", options.Schema);
                b.ConfigureByConvention();
                b.Property(q => q.ApplicableOn).IsRequired();
                b.Property(q => q.RefCurrencyId).IsRequired().HasMaxLength(CurrencyConsts.MaxLengthCurrency);
                b.Property(q => q.CurrencyId).IsRequired().HasMaxLength(CurrencyConsts.MaxLengthCurrency);
                b.Property(q => q.Rate).IsRequired().HasColumnType("decimal(18,2)");
                b.Property(q => q.IsActive).IsRequired().HasColumnType("INTEGER").HasDefaultValue(1);
                b.HasOne(c=>c.RefCurrency).WithMany(t=>t.RefExchangeRates).HasForeignKey("RefCurrencyId");
                b.HasOne(c=>c.Currency).WithMany(t=>t.ExchangeRates).HasForeignKey("CurrencyId");
                b.HasIndex(q => q.CreationTime);
                b.HasKey(q => q.Id);

            });
            builder.Entity<Payment>(b =>
            {
                b.ToTable(options.TablePrefix + "Payments", options.Schema);
                b.ConfigureByConvention();
                b.Property(q => q.PaymentDate).IsRequired();
                b.Property(q => q.PaidCurrencyId).IsRequired().HasMaxLength(CurrencyConsts.MaxLengthCurrency);
                b.Property(q => q.AmountPaid).IsRequired().HasColumnType("decimal(18,2)");
                b.Property(q => q.ExchangeRateApplied).IsRequired(false);
                b.Property(q => q.InvoiceId).IsRequired();
                b.HasOne(c=>c.Invoice).WithMany(t=>t.Payments).HasForeignKey("InvoiceId").IsRequired();
                b.HasOne(c=>c.PaidCurrency).WithMany(t=>t.Payments).HasForeignKey("PaidCurrencyId").IsRequired();
                b.HasIndex(q => q.CreationTime);
                b.HasKey(q => q.Id);

            });

            builder.Entity<Category>(b =>
            {
                b.ToTable(options.TablePrefix + "Categories", options.Schema);
                b.ConfigureByConvention();
                b.Property(q => q.Name).IsRequired().HasMaxLength(CategoryConsts.MaxLengthName);
                b.Property(q => q.CategoryId).IsRequired(false);
                b.Property(q => q.ShortName).IsRequired().HasMaxLength(CategoryConsts.MaxLengthShortName);
                b.HasOne(c => c.ParentCategory).WithMany(t => t.SubCategories).HasForeignKey("CategoryId");
                b.HasKey(q => q.Id);
                b.HasIndex(c => c.ShortName).IsUnique();
                b.HasIndex(c => c.Name).IsUnique();

            });
           
        }
    }
}