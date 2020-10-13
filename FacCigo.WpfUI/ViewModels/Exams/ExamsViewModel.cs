using CSVLibrary;
using FacCigo.Commands.Exams;
using FacCigo.Models;
using FacCigo.ViewModels.ETL;
using FacCigo.Views.ETL;
using FacCigo.Views.Exams;
using Microsoft.Extensions.DependencyInjection;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using Volo.Abp.DependencyInjection;

namespace FacCigo.ViewModels.Exams
{
    public class ExamsViewModel:BindableBase, IExamsViewModel,ITransientDependency
    {
        private readonly IExamAppService AppService;
        private ExamDto _selectedItem;
        private IServiceProvider _serviceProvider;
        private ObservableCollection<ExamDto> _items;
        private IEventAggregator EventAggregator;
        public ExamsViewModel(IExamAppService appService, IServiceProvider serviceProvider,IEventAggregator eventAggregator)
        {
            AppService = appService;
            _serviceProvider = serviceProvider;
            EventAggregator = eventAggregator;
            SelectedCommand = new DelegateCommand<object[]>(OnItemSelected);
            CreateCommand = new DelegateCommand(Create);
            UpdateCommand = new DelegateCommand(Update);
            ImportCommand = new DelegateCommand(Import);
            DeleteCommand = new DelegateCommand(Delete);
            Items = new ObservableCollection<ExamDto>();
            Items.AddRange( AppService.GetListAsync(new ExamGetListInput()).Result.Items);
            EventAggregator.GetEvent<ExamUpdatedEvent>().Subscribe(OnExamUpdated);
            EventAggregator.GetEvent<ExamDeletedEvent>().Subscribe(OnExamDeleted);
            EventAggregator.GetEvent<ExamAddedEvent>().Subscribe(OnExamAdded);


        }

        
        public ExamDto SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }
        public ObservableCollection<ExamDto> Items { get { return _items; } private set { SetProperty(ref _items, value); } }
        public DelegateCommand<object[]> SelectedCommand { get; private set; }
        public DelegateCommand CreateCommand { get; private set; }
        public DelegateCommand UpdateCommand { get; private set; }
        public DelegateCommand ImportCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }
        private void OnItemSelected(object[] selectedItems)
        {
            if (selectedItems != null && selectedItems.Count() > 0)
            {
                SelectedItem = (ExamDto)selectedItems.FirstOrDefault();
            }
        }
        private void Create()
        {
            var dialog = _serviceProvider.GetRequiredService<ExamInputDialog>();
            dialog.ShowDialog();
        }
        private void Update ()
        {
            if (SelectedItem == null) return;
            var dialog = _serviceProvider.GetRequiredService<ExamInputDialog>();
            dialog.setModel(SelectedItem);
            dialog.ShowDialog();
        }
        private void Delete()
        {
            if (SelectedItem == null) return;
            Guid id = SelectedItem.Id;
            AppService.DeleteAsync(id);
            EventAggregator.GetEvent<ExamDeletedEvent>().Publish(id);
        }
        private void Import()
        {
            CsvReaderDialog dialog = _serviceProvider.GetRequiredService<CsvReaderDialog>();
            CsvReaderViewModel<CsvReader<ExamModel>, ExamModel> viewModel 
                = new CsvReaderViewModel<CsvReader<ExamModel>, ExamModel>(EventAggregator,_serviceProvider);
            dialog.ShowDialog(viewModel);
        }
        private void OnExamAdded(ExamDto dto)
        {
            SelectedItem = Items.GetOrAdd(c => c.Id == dto.Id, () => dto);
        }
        private void OnExamUpdated(ExamDto dto)
        {
            SelectedItem = Items.Where(c => c.Id == dto.Id).Select(c => { c = dto; return c; }).FirstOrDefault();
        }
        private void OnExamDeleted(Guid id)
        {
            Items.RemoveAll(c => c.Id == id);
        }
    }
}
