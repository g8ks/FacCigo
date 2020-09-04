using FacCigo.Commands;
using FacCigo.ViewModels.ExchangeRates;
using Microsoft.Data.Sqlite;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace FacCigo
{
    public class ExchangeRateAddViewModel : BindableBase, IExchangeRateAddViewModel, ITransientDependency
    {
        private readonly IExchangeRateAppService AppService;
        private readonly IEventAggregator EventAggregator;
        private string _errorText;
        private decimal Default { get; set; }
        private ExchangeRateDto Exchange;
        public ExchangeRateAddViewModel(IExchangeRateAppService service, IEventAggregator eventAggregator)
        {
            AppService = service;
            EventAggregator = eventAggregator;
            CreateCommand = new DelegateCommand<ICloseable>(Create);
            Exchange = AppService.CurrentExchangeRate().Result;
        }
        private decimal _rate = 0.00m;
        public decimal Rate
        {
            get { return _rate; }
            set { SetProperty(ref _rate, value); }
        }
        public DelegateCommand<ICloseable> CreateCommand { get; private set; }
        public string ErrorText { get => _errorText; 
                                 set { SetProperty(ref _errorText, value); } }

        public async void Create(ICloseable window)
        {
            try { 
                if(Exchange==null|| Exchange.Rate != Rate)
                {
                    ExchangeRateDto rateDto = await AppService.CreateAsync(Rate);
                    EventAggregator.GetEvent<ExchangeRateAddedEvent>().Publish(rateDto);
                }
                else
                {
                    EventAggregator.GetEvent<ExchangeRateAddedEvent>().Publish(Exchange);
                }
                window.Close();
            } catch (SqliteException sqlEx)
            {
                ErrorText = sqlEx.Message;
            } catch (BusinessException busEx)
            {
                ErrorText = busEx.Message;
            }



        }
        
    }
}
