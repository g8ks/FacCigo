using Prism.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace FacCigo.Models
{
    public class InvoiceLineModel : ValidatableBindableBase,ITransientDependency
    {
      
        private ExamDto _exam;
        private decimal _amount;
        public ExamDto Exam {get {return _exam;} 
                             set{ SetProperty(ref _exam, value);
                

            }
        }
        
        public decimal Amount { get { return _amount; }
            set { SetProperty(ref _amount, value);         
            
            }
          }

        public InvoiceLineInput InvoiceLine()
        {
            return new InvoiceLineInput() { ExamId = Exam.Id };
        }

        public static IList<InvoiceLineModel> InvoiceLines(IList<InvoiceLineDto> dtos)
        {
            IList<InvoiceLineModel> invoiceLines = new List<InvoiceLineModel>();
            foreach (InvoiceLineDto dto in dtos)
            {
                invoiceLines.Add(new InvoiceLineModel() { Exam = dto.Exam, Amount = dto.ConvertedAmount });
             }
            return invoiceLines;
        }
    }
}
