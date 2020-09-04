using Prism.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace FacCigo.Models
{
    public class InvoiceLineModel : ValidatableBindableBase,ITransientDependency
    {
        private ExchangeRateDto _exchange;
        private ExamDto _exam;
        private decimal _amount;
        public ExamDto Exam {get {return _exam;} 
                             set{ SetProperty(ref _exam, value); calculateA();

            }
        }
        public ExchangeRateDto ExchangeRate
        {
            get { return _exchange; }
            set { SetProperty(ref _exchange, value); }
        }
        public decimal Amount { get { return _amount; }
            set { SetProperty(ref _amount, value); calculateA(); }
          }

        public InvoiceLineInput InvoiceLine()
        {
            return new InvoiceLineInput() { ExamId = Exam.Id };
        }
        protected void calculateA()
        {
            Amount = ExchangeRate.Rate * Exam.Price;
        }
    }
}
