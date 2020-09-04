using FacCigo.Commands;
using FacCigo.Commands.Patients;
using Microsoft.Data.Sqlite;
using Prism.Commands;
using Prism.Events;
using Prism.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace FacCigo.ViewModels.Patients
{
    public class PatientInputViewModel:ValidatableBindableBase,ITransientDependency,IPatientInputViewModel
    {
        private Guid _id;
        private string _firstname;
        private string _lastname;
        private string _middlename;
        private DateTime _birthDate;
        private string _phoneNumber;
        private string _address;
        private string _errorText;
        private readonly IEventAggregator EventAggregator;
        private readonly IPatientAppService AppService;
        public bool NewExam { get; private set; } = true;
        private PatientDto updated;
        public PatientInputViewModel(IPatientAppService appService,  IEventAggregator ea)
        {
            
            EventAggregator = ea;
            AppService = appService;
            CreateCommand = new DelegateCommand<ICloseable>(Create);
            
        }
        public DelegateCommand<ICloseable> CreateCommand { get; private set; }
        [StringLength(ExamConsts.MaxLengthName, ErrorMessage = "La Taille ne correspond pas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vous devez fournir le prenom")]
        public string FirstName
        {
            get { return _firstname; }
            set { SetProperty(ref _firstname, value); }
        }
        public Guid Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        [StringLength(ExamConsts.MaxLengthName, ErrorMessage = "La Taille ne correspond pas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vous devez fournir le Nom")]
        public string LastName
        {
            get { return _lastname; }
            set { SetProperty(ref _lastname, value); }
        }
        [StringLength(PatientConsts.MaxNameLength, ErrorMessage = "La Taille ne correspond pas")]
        public string MiddleName
        {
            get { return _middlename; }
            set
            {
                SetProperty(ref _middlename, value);
              
            }
        }
        public DateTime BirthDate
        {
            get { return _birthDate; }
            set { SetProperty(ref _birthDate, value); }
        }
        [StringLength(PatientConsts.MaxAddressLength, ErrorMessage = "La Taille ne correspond pas")]
        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { SetProperty(ref _phoneNumber, value); }
        }
        public string ErrorText
        {
            get { return _errorText; }
            set { SetProperty(ref _errorText, value); }
        }
        public async void Create(ICloseable window)
        {
            try
            {
                if (HasErrors) return;
                PatientInput input = new PatientInput() { FirstName = FirstName, LastName = LastName,
                                                          MiddleName = MiddleName, BirthDate = BirthDate,PhoneNumber=PhoneNumber,Address=Address };
                PatientDto examDto = await (NewExam ? AppService.CreateAsync(input) : AppService.UpdateAsync(Id, input));
                if (NewExam) EventAggregator.GetEvent<PatientAddedEvent>().Publish(examDto);
                else EventAggregator.GetEvent<PatientUpdatedEvent>().Publish(examDto);
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
        public bool IsValid { get { return !HasErrors; } }
     

        public void UpdateModel(PatientDto dto)
        {
            updated = dto;
            NewExam = false;
            Id = dto.Id;
            FirstName = dto.FirstName;
            LastName = dto.LastName;
            MiddleName = dto.MiddleName;
            BirthDate = dto.BirthDate.Value;
            Address = dto.Address;


        }
    }
}
