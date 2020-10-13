using FacCigo.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Settings;

namespace FacCigo
{
    public class InvoiceManager:DomainService
    {
        private readonly IRepository<Invoice, Guid> Repository;
        private readonly IRepository<ExchangeRate, Guid> ExchangeRateRepository;
        private readonly IRepository<Exam, Guid> ExamRepository;
        private readonly ISettingProvider SettingProvider;
        private readonly IRepository<Currency, string> CurrencyRepository;
        public InvoiceManager(IRepository<Invoice,Guid> repo, 
              IRepository<ExchangeRate,Guid> exchangeRates, IRepository<Exam,Guid> exams,IRepository<Currency,string> currencies,SettingProvider settingProvider)
        {
            Repository = repo;
            ExchangeRateRepository = exchangeRates;
            ExamRepository = exams;
            CurrencyRepository = currencies;
            SettingProvider = settingProvider;
        }
        //Task<Invoice>
        public async Task<Invoice>CreateAsync(Invoice invoice)
        {
            string invoiceCurrency = await SettingProvider.GetOrNullAsync(FacCigoSettings.InvoiceCurrency);
            if (invoiceCurrency == null) throw new InvoiceCurrencyNotSettledException(invoiceCurrency);
            Currency currency = await CurrencyRepository.GetAsync(invoiceCurrency);
            if (currency == null) throw new InvoiceCurrencyNotSettledException(invoiceCurrency);
            invoice.ChangeCurrency(currency.Id);
            ExchangeRate exchange = await ExchangeRateRepository.FirstOrDefaultAsync(c => c.CurrencyId == currency.Id && c.IsActive);
            if (exchange == null) throw new ExchangeRateNotExisting();
            invoice.ChangeExchange(exchange.Id);
            foreach(InvoiceLine line in invoice.InvoiceLines)
            {
                Exam exam = await ExamRepository.GetAsync(line.ExamId);
                if (exam == null) throw new ExamNotExisting(line.ExamId);
                line.ChangeAmount(exam.Price);
                line.ChangeCurrency(exam.CurrencyId);

            }
            return await Repository.InsertAsync(invoice, autoSave: true);
        }
    }
}
