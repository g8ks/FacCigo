using FacCigo.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Settings;

namespace FacCigo
{
    public class InvoiceManager:DomainService
    {
        private readonly IInvoiceRepository Repository;
        private readonly IExchangeRateRepository ExchangeRateRepository;
        private readonly IExamRepository ExamRepository;
        private readonly ISettingProvider SettingProvider;
        private readonly ICurrencyRepository CurrencyRepository;
        public InvoiceManager(IInvoiceRepository repo, 
              IExchangeRateRepository exchangeRates, IExamRepository exams,ICurrencyRepository currencies,SettingProvider settingProvider)
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
