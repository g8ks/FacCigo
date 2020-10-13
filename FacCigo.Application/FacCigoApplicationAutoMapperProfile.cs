using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.AutoMapper;

namespace FacCigo
{
    public class FacCigoApplicationAutoMapperProfile : Profile
    {
        public FacCigoApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
           
            CreateMap<Patient, PatientDto>();
            CreateMap<PatientInput, Patient>()
                .Ignore(c=>c.Id)
                .Ignore(c=>c.Invoices)
                .Ignore(c => c.ExtraProperties)
                .Ignore(c=>c.ConcurrencyStamp)
                .IgnoreFullAuditedObjectProperties();
            CreateMap<ExamInput, Exam>()
                .Ignore(c => c.Id)
                .Ignore(c => c.Currency)
                .Ignore(c => c.Category)
                .Ignore(c => c.InvoiceLines)
                .Ignore(c => c.ExtraProperties)
                .Ignore(c => c.ConcurrencyStamp)
                .IgnoreAuditedObjectProperties();
            CreateMap<Exam, ExamDto>()
                .ForMember(dest=>dest.Category,opt=>opt.MapFrom(x=>x.Category.Name));
            CreateMap<Currency,CurrencyDto>();
            CreateMap<CurrencyDto, Currency>()
                 .Ignore(c => c.InvoiceLines)
                 .Ignore(c => c.Invoices)
                 .Ignore(c => c.Exams)
                .Ignore(c => c.Payments)
                .Ignore(c => c.RefExchangeRates)
                .Ignore(c => c.ExchangeRates); 
            CreateMap<InvoiceInput, Invoice>()
                .Ignore(c => c.Id)
                .Ignore(c => c.InvoiceLines)
                .Ignore(c => c.ExtraProperties)
                .Ignore(c=>c.CurrencyId)
                .Ignore(c => c.Currency)
                .Ignore(c => c.ExchangeRateId)
                .Ignore(c => c.ExchangeRate)
                .Ignore(c => c.TotalAmount)
                .Ignore(c => c.Payments)
                .Ignore(c => c.Patient)
                .Ignore(c => c.ConcurrencyStamp)
                .IgnoreFullAuditedObjectProperties();

            CreateMap<Invoice,InvoiceDto>()
                         .ForMember(dest => dest.PatientName, opt => opt.MapFrom(x => x.Patient.Name))
                         .ForMember(dest => dest.Rate, opt => opt.MapFrom(x => x.ExchangeRate.Rate))
                         .ForMember(dest=>dest.TotalAmount,opt=>opt.MapFrom(x=>x.InvoiceLines.Sum(c=>c.Amount)*x.ExchangeRate.Rate));
            CreateMap<InvoiceLine, InvoiceLineDto>()
                .ForMember(dest=>dest.ConvertedAmount,opt=>opt.MapFrom(x=>x.Amount*x.Invoice.ExchangeRate.Rate));
           
            CreateMap<ExchangeRate, ExchangeRateDto>();
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>()
                .Ignore(c=>c.ParentCategory)
                .Ignore(c=>c.Exams)
                .Ignore(c=>c.SubCategories);
            //CreateMap<IList<InvoiceLine>, IList<InvoiceLineDto>>();
            //CreateMap<IList<ExchangeRate>, IList<ExchangeRateDto>>();
            //CreateMap<IList<Payment>, IList<PaymentDto>>();
            //CreateMap<IList<Category>, IList<CategoryDto>>();
            //CreateMap<IList<Invoice>, IList<InvoiceDto>>();
            //CreateMap<IList<Exam>, IList<ExamDto>>();
        }
    }
}