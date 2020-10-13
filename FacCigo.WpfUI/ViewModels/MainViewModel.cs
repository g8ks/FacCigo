using FacCigo.Settings;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace FacCigo.ViewModels
{
    public class MainViewModel:BindableBase,IMainViewModel,ITransientDependency
    {
        private readonly IExchangeRateAppService ExchangeService;
        private readonly ISettingProvider SettingProvider;
        private readonly IEventAggregator EventAggregator;
        private ExchangeRateDto _exchange;
        private string _rate;
       
        public MainViewModel(IExchangeRateAppService exchangeAppService,ISettingProvider setting, IEventAggregator ea)
        {
            ExchangeService = exchangeAppService;
            SettingProvider = setting;
            EventAggregator = ea;
            EventAggregator.GetEvent<ExchangeRateAddedEvent>().Subscribe(ExchangeChanged);
            Exchange = ExchangeService.CurrentExchangeRate().Result;
        }
        public ExchangeRateDto Exchange { get =>_exchange; 
                                         set { 
                SetProperty(ref _exchange, value);
                Rate=string.Format(new CultureInfo("fr-CD", false), "1 {0} = {1:F} {2}", PivotCurrency, Exchange?.Rate, InvoiceCurrency);
            } }
        public string  Rate { get => _rate;
                             set { SetProperty(ref _rate, value); } }

        public string  PivotCurrency { get { return SettingProvider.GetOrNullAsync(FacCigoSettings.PivotCurrency).Result; } }
        public string  InvoiceCurrency { get { return SettingProvider.GetOrNullAsync(FacCigoSettings.InvoiceCurrency).Result; } }
        private void   ExchangeChanged(ExchangeRateDto rateDto)
        {
            Exchange = rateDto;
        }
       
       
    }
}
