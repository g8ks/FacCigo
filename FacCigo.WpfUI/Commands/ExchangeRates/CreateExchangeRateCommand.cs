using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Volo.Abp.DependencyInjection;

namespace FacCigo
{
    public class CreateExchangeRateCommand : BindableBase, ICommand, ITransientDependency
    {
        public event EventHandler CanExecuteChanged;
        private readonly IExchangeRateAppService appService;
        private readonly IEventAggregator eventAggregator;
        public CreateExchangeRateCommand(IExchangeRateAppService service,IEventAggregator aggregator)
        {
            appService = service;
            eventAggregator = aggregator;
        }
        public bool CanExecute(object parameter)
        {
            decimal rate = Convert.ToDecimal(parameter);
            if (rate > 0) return true;
            return false;
        }

        public void Execute(object parameter)
        {
            
            decimal rate = Convert.ToDecimal(parameter);
            ExchangeRateDto rateDto = appService.CreateAsync(rate).Result;
            eventAggregator.GetEvent<ExchangeRateAddedEvent>().Publish(rateDto);


        }
       
    }
}
