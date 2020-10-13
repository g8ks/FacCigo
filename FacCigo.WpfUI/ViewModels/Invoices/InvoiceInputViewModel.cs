using FacCigo.Commands;
using FacCigo.Commands.Invoices;
using FacCigo.Commands.Patients;
using FacCigo.Exams;
using FacCigo.Models;
using FacCigo.Settings;
using FacCigo.Views.Patients;
using MaterialDesignThemes.Wpf.Converters;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using Prism.Commands;
using Prism.Events;
using Prism.Validation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Navigation;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace FacCigo.ViewModels.Invoices
{
    public class InvoiceInputViewModel : ValidatableBindableBase, IInvoiceInputViewModel, ITransientDependency
    {
        
        private ObservableCollection<PatientDto> _patients;
        private ObservableCollection<ExamDto> _exams;
        private string _errorText;
        private string _currency;
        private ExchangeRateDto _exchangeRate;
        private readonly ISettingProvider SettingProvider;
        private IServiceProvider _serviceProvider;
        private readonly IEventAggregator EventAggregator;
        private readonly IPatientAppService PatientService;
        private readonly IExamAppService ExamService;
        private readonly IInvoiceAppService AppService;
        private readonly IExchangeRateAppService rateAppService;
        public bool NewInvoice { get; private set; } = true;
        private InvoiceModel _model;
        private InvoiceLineModel _selectedInvoiceLine;
        public InvoiceInputViewModel(IInvoiceAppService appService,
            IPatientAppService patientApp, IExamAppService examApp,
            IEventAggregator eventAggregator, ISettingProvider setting, IExchangeRateAppService exchangeRateApp, IServiceProvider serviceProvider)
        {
            SettingProvider = setting;
            EventAggregator = eventAggregator;
            _serviceProvider = serviceProvider;
            PatientService = patientApp;
            AppService = appService;
            rateAppService = exchangeRateApp;
            ExamService = examApp;
            CreateCommand = new DelegateCommand<ICloseable>(Create);
            AddPatientCommand = new DelegateCommand(CreatePatient);
            DeleteInvoiceLineCommand = new DelegateCommand(OnInvoiceLineDeleted);
            SelectedCommand = new DelegateCommand<object[]>(OnItemSelected);
            AddExamCommand = new DelegateCommand<ExamDto>(AddExam);
            Patients = new ObservableCollection<PatientDto>();
            Patients.AddRange(PatientService.GetListAsync(new PatientGetListInput() { }).Result.Items);
            Currency = SettingProvider.GetOrNullAsync(FacCigoSettings.InvoiceCurrency).Result;
            Model = new InvoiceModel();
            Model.ReferenceNo = AppService.NextReferenceNo(Model.InvoiceDate.Year).Result;
            Exams = new ObservableCollection<ExamDto>();
            Exams.AddRange(ExamService.GetListAsync(new ExamGetListInput()).Result.Items);
            EventAggregator.GetEvent<PatientAddedEvent>().Subscribe(PatientAdded); 
            ExchangeRate = rateAppService.CurrentExchangeRate().Result;
        }

        public void UpdateModel(InvoiceModel dto)
        {
            NewInvoice = false;
            Model = dto;
            throw new NotImplementedException();
        }
        public DelegateCommand<object[]> SelectedCommand { get; private set; }
        public DelegateCommand<ICloseable> CreateCommand { get; private set; }
        public DelegateCommand AddPatientCommand { get; private set; }
        public DelegateCommand<ExamDto> AddExamCommand { get; private set; }
        public DelegateCommand DeleteInvoiceLineCommand { get; private set; }
        public async void Create(ICloseable window)
        {
            try
            {
                if (Model.HasErrors) return;
              
                InvoiceDto invoiceDto = await (NewInvoice ? AppService.CreateAsync(Model.GetInput()) : 
                                                        AppService.UpdateAsync(Model.Id, Model.GetInput()));
                invoiceDto.PatientName = Model.Patient.Name;
                if (NewInvoice) EventAggregator.GetEvent<InvoiceAddedEvent>().Publish(invoiceDto);
                else EventAggregator.GetEvent<InvoiceUpdatedEvent>().Publish(invoiceDto);
                window.Close();
            }
            catch (SqliteException sqlEx)
            {
                ErrorText = sqlEx.Message;
            }
            catch (BusinessException busEx)
            {
                ErrorText = busEx.Message;
            }

        }   
        public InvoiceModel Model { get => _model;  set { SetProperty(ref _model, value); } }
        public string ErrorText
        {
            get { return _errorText; }
            set { SetProperty(ref _errorText, value); }
        }
        public ObservableCollection<PatientDto> Patients
        {
            get { return _patients; }
            set { SetProperty(ref _patients, value); }
        }
      
        public string Currency
        {
            get { return _currency; }
            set { SetProperty(ref _currency, value); }
        }
       
        public ObservableCollection<ExamDto> Exams
        {
            get { return _exams; }
            set { SetProperty(ref _exams, value); }
        }
        private void PatientAdded(PatientDto dto)
        {
          Model.Patient=  Patients.GetOrAdd(c => c.Id == dto.Id, () => dto);
          
        }
        public InvoiceLineModel SelectedInvoiceLine
        {
            get { return _selectedInvoiceLine; }
            set { SetProperty(ref _selectedInvoiceLine, value); }
        }
        private void CreatePatient()
        {
            var dialog = _serviceProvider.GetRequiredService<PatientInputDialog>();
            dialog.ShowDialog();
        }
        private void AddExam(ExamDto dto)
        {
         SelectedInvoiceLine= Model.InvoiceLines.GetOrAdd(c => c.Exam.Id == dto.Id,
                                     () => new InvoiceLineModel() { Exam = dto, Amount = ExchangeRate.Rate * dto.Price });
        }
        public ExchangeRateDto ExchangeRate { get { return _exchangeRate; }
            set { SetProperty(ref _exchangeRate, value); } 
        }
        private void OnItemSelected(object[] selectedItem)
        {
            if (selectedItem != null && selectedItem.Count() > 0 )
            {
                SelectedInvoiceLine= (InvoiceLineModel)selectedItem.FirstOrDefault();
            }
        }
        private void OnInvoiceLineDeleted()
        {
            Model.InvoiceLines.Remove(SelectedInvoiceLine);
        }
        private DataRowView select_request;
        public DataRowView Select_Request
        {
            get { return select_request; }
            set
            {
                select_request = value;
                SetProperty(ref select_request, value);
                SelectedInvoiceLine = value.Row.As<InvoiceLineModel>();
            }
        }
    }
}
