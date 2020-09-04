using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Guids;

namespace FacCigo
{
    public class CurrencyDataSeedContributor : IDataSeedContributor, ITransientDependency
    {
        private readonly IRepository<Currency, string> _currencyRepository;
      

        public CurrencyDataSeedContributor(IRepository<Currency, string> currencyRepository )
        {
            _currencyRepository = currencyRepository;
           
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (await _currencyRepository.GetCountAsync() > 0)
            {
                return;
            }
            await _currencyRepository.InsertAsync(new Currency("FC") { Name="Franc Congolais"}, autoSave: true);
            await _currencyRepository.InsertAsync(new Currency("USD") { Name = "Dollar Americain",IsPivot=true }, autoSave: true);
        }
    }
}
