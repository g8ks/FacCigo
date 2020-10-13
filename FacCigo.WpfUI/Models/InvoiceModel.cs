using Prism.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace FacCigo.Models
{
    public class InvoiceModel:ValidatableBindableBase
    {
        private Guid _id;
        private PatientDto _patient;
        private decimal _totalAmount = 0.00m;
        private string _referenceNo;
        private DateTime _invoiceDate;
        private ObservableCollection<InvoiceLineModel> _invoicelines;
        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set { SetProperty(ref _totalAmount, value); }
        }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vous devez selectionner le patient")]
        public PatientDto Patient
        {
            get { return _patient; }
            set
            {
                SetProperty(ref _patient, value);

            }
        }
        public string ReferenceNo
        {
            get { return _referenceNo; }
            set { SetProperty(ref _referenceNo, value); }
        }
        public Guid Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        public DateTime InvoiceDate
        {
            get { return _invoiceDate; }
            set { SetProperty(ref _invoiceDate, value); }
        }
        public ObservableCollection<InvoiceLineModel> InvoiceLines
        {
            get { return _invoicelines; }
            set { SetProperty(ref _invoicelines, value);
              
            }
        }

        public InvoiceInput GetInput()
        {
            return  new InvoiceInput() { ReferenceNo = ReferenceNo,
                                         InvoiceLines = InvoiceLineInputs,
                                          PatientId = Patient.Id,
                                          InvoiceDate = InvoiceDate, Status = InvoiceStatus.PENDING };
        }
        protected IList<InvoiceLineInput> InvoiceLineInputs
        {
            get { return InvoiceLines.Select(c => c.InvoiceLine()).ToList(); }
        }
        public InvoiceModel()
        {
            InvoiceDate = DateTime.Now;
            InvoiceLines = new ObservableCollection<InvoiceLineModel>();
            InvoiceLines.CollectionChanged += InvoiceLinesChanged;
        }
        public static InvoiceModel GetModel(InvoiceDto dto)
        {
            ObservableCollection<InvoiceLineModel> models = new ObservableCollection<InvoiceLineModel>();
            models.AddRange(InvoiceLineModel.InvoiceLines(dto.InvoiceLines));
            return new InvoiceModel() {ReferenceNo=dto.ReferenceNo,
            InvoiceDate=dto.InvoiceDate,InvoiceLines=models};
        }
        public void InvoiceLinesChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            TotalAmount = InvoiceLines.Sum(c => c.Amount);
        }
    }
}
