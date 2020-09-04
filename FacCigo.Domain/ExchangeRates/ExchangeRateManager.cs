using AutoMapper;
using FacCigo.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Settings;
using Volo.Abp.Uow;

namespace FacCigo
{
    public class ExchangeRateManager:DomainService
    {
        private readonly ISettingProvider SettingProvider;
        private readonly IRepository<ExchangeRate, Guid> Repository;
        private readonly IRepository<Currency, string>  CurrencyRepository;
        public ExchangeRateManager(IRepository<ExchangeRate,Guid> repo,IRepository<Currency,string> currencies,ISettingProvider setting)
        {
            SettingProvider = setting;
            Repository = repo;
            CurrencyRepository = currencies;
        }

        public async Task<ExchangeRate> CreateAsync(decimal rate)
        {
            string invoiceCurrency = await SettingProvider.GetOrNullAsync(FacCigoSettings.InvoiceCurrency);
            string pivotCurrency = await SettingProvider.GetOrNullAsync(FacCigoSettings.PivotCurrency);
            if (invoiceCurrency == null) throw new InvoiceCurrencyNotSettledException(invoiceCurrency);
            if (pivotCurrency == null) throw new CurrencyNotPivotException(pivotCurrency);
            Currency ci = await CurrencyRepository.GetAsync(invoiceCurrency);
            if (ci == null) throw new InvoiceCurrencyNotSettledException(invoiceCurrency);
            Currency cp = await CurrencyRepository.GetAsync(pivotCurrency);
            if (cp == null) throw new CurrencyNotPivotException(pivotCurrency);
            DateTime date = DateTime.Now;
            return await Repository.InsertAsync(new ExchangeRate(GuidGenerator.Create(), cp.Id, ci.Id, date, rate)
                          { CreationTime = date,IsActive=true},autoSave:true);   
        }
        public async Task<ExchangeRate> GetCurrentExchangeRate()
        {
            string invoiceCurrency = await SettingProvider.GetOrNullAsync(FacCigoSettings.InvoiceCurrency);
            string pivotCurrency = await SettingProvider.GetOrNullAsync(FacCigoSettings.PivotCurrency);
            if (invoiceCurrency == null) throw new InvoiceCurrencyNotSettledException(invoiceCurrency);
            if (pivotCurrency == null) throw new CurrencyNotPivotException(pivotCurrency);
            Currency ci = await CurrencyRepository.GetAsync(invoiceCurrency);
            if (ci == null) throw new InvoiceCurrencyNotSettledException(invoiceCurrency);
            Currency cp = await CurrencyRepository.GetAsync(pivotCurrency);
            if (cp == null) throw new CurrencyNotPivotException(pivotCurrency);
            var result = await Repository.FirstOrDefaultAsync(c => c.IsActive && (c.RefCurrencyId == cp.Id && c.CurrencyId == ci.Id));
            return result;
        }
    }
}
