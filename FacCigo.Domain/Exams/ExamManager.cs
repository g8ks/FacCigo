using FacCigo.Settings;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using Volo.Abp.Settings;

namespace FacCigo
{
    public class ExamManager: DomainService
    {
        private readonly IExamRepository Repository;
        private readonly ICurrencyRepository CurrencyRepository;
        private readonly ISettingProvider SettingProvider;
        public ExamManager(IExamRepository repo,ICurrencyRepository currencies,ISettingProvider setting) {

            Repository = repo;
            CurrencyRepository = currencies;
            SettingProvider = setting;
        }
        public async Task<Exam> CreateAsync(string Name,string referenceNo,string currency,decimal price,Guid category)
        {
            if (! await CheckPivot(currency))throw new CurrencyNotPivotException(currency);
            return await Repository.InsertAsync(new Exam(GuidGenerator.Create(),Name, referenceNo, price, currency, category));
        }
        public async Task<Exam> UpdateAsync(Guid id, string Name,string referenceNo, string currency, decimal price, Guid category)
        {
            if (!await CheckPivot(currency)) throw new CurrencyNotPivotException(currency);
            Exam exam =await Repository.GetAsync(id);
            if (exam == null) throw new ExamNotExisting(id);
            exam = new Exam(id, Name, referenceNo, price, currency, category);
            return await Repository.UpdateAsync(exam, autoSave: true);
        }
        private async Task<bool> CheckPivot(string currency)
        {
            string pivotCurrency = await SettingProvider.GetOrNullAsync(FacCigoSettings.PivotCurrency);
            Currency cp = await CurrencyRepository.GetAsync(pivotCurrency);
            if (cp == null) throw new CurrencyNotPivotException(pivotCurrency);
            return cp.Id == currency;
        }
    }
}
