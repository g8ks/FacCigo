using FacCigo.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FacCigo.ViewModels.Invoices
{
     public interface IInvoiceInputViewModel
    {
        void UpdateModel(InvoiceModel dto);
        void AddExam(ExamDto dto);
    }
}
