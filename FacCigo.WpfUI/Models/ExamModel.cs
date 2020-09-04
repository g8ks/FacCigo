using CSVLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace FacCigo.Models
{
     public class ExamModel: CsvableBase, ICsvable,ITransientDependency
     {
        public string ReferenceNo { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Guid   CategoryId { get; set; }
        public string CurrencyId { get; set; }

        public ExamInput ToInput()
        {
            return new ExamInput()
            {
                Name = Name,
                Price = Price,
                CurrencyId = CurrencyId,
                CategoryId = CategoryId,
                ReferenceNo=ReferenceNo

            };
        }
     }
}
