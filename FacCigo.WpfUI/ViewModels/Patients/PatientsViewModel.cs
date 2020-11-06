using FacCigo.Commands.Patients;
using FacCigo.Views.Patients;
using Microsoft.Extensions.DependencyInjection;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Navigation;
using Volo.Abp.DependencyInjection;

namespace FacCigo.ViewModels.Patients
{
    public class PatientsViewModel:BindableBase,ITransientDependency,IPatientsViewModel
    {
        private readonly IPatientAppService AppService;
        private PatientDto _selectedItem;
        private IServiceProvider _serviceProvider;
        private ObservableCollection<PatientDto> _items;
        private IEventAggregator EventAggregator;
        public PatientsViewModel(IPatientAppService appService, IServiceProvider serviceProvider, IEventAggregator eventAggregator)
        {
            AppService = appService;
            _serviceProvider = serviceProvider;
            EventAggregator = eventAggregator;
            SelectedCommand = new DelegateCommand<object[]>(OnItemSelected);
            CreateCommand = new DelegateCommand(Create);
            UpdateCommand = new DelegateCommand(Update);
            DeleteCommand = new DelegateCommand(Delete);
            Items = new ObservableCollection<PatientDto>();
            Items.AddRange(AppService.GetListAsync().Result);
            EventAggregator.GetEvent<PatientUpdatedEvent>().Subscribe(OnPatientUpdated);
            EventAggregator.GetEvent<PatientDeletedEvent>().Subscribe(OnPatientDeleted);
            EventAggregator.GetEvent<PatientAddedEvent>().Subscribe(OnPatientAdded);


        }


        public PatientDto SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
        public ObservableCollection<PatientDto> Items { get { return _items; } private set { SetProperty(ref _items, value); } }
        public DelegateCommand<object[]> SelectedCommand { get; private set; }
        public DelegateCommand CreateCommand { get; private set; }
        public DelegateCommand UpdateCommand { get; private set; }
        public DelegateCommand ImportCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }
        private void OnItemSelected(object[] selectedItems)
        {
            if (selectedItems != null && selectedItems.Count() > 0)
            {
                SelectedItem = (PatientDto)selectedItems.FirstOrDefault();
                
            }
        }
        private void Create()
        {
            var dialog = _serviceProvider.GetRequiredService<PatientInputDialog>();
            dialog.ShowDialog();
        }
        private void Update()
        {
            if (SelectedItem == null) return;
            var dialog = _serviceProvider.GetRequiredService<PatientInputDialog>();
            dialog.setModel(SelectedItem);
            dialog.ShowDialog();
        }
        private void Delete()
        {
            if (SelectedItem == null) return;
            Guid id = SelectedItem.Id;
            AppService.DeleteAsync(id);
            EventAggregator.GetEvent<PatientDeletedEvent>().Publish(id);
        }
       
        private void OnPatientAdded(PatientDto dto)
        {
           SelectedItem=Items.GetOrAdd(c=>c.Id==dto.Id,()=>dto);
        }
        private void OnPatientUpdated(PatientDto dto)
        {
           SelectedItem= Items.Where(c => c.Id == dto.Id).Select(c => { c = dto; return c; }).FirstOrDefault() ;
        }
        private void OnPatientDeleted(Guid id)
        {
            Items.RemoveAll(c => c.Id == id);
        }
    }
}
