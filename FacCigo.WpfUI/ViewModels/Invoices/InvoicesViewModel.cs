using FacCigo.Commands.Invoices;
using FacCigo.Models;
using FacCigo.Views.Invoices;
using Microsoft.Extensions.DependencyInjection;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using Volo.Abp.DependencyInjection;

namespace FacCigo.ViewModels.Invoices
{
    public  class InvoicesViewModel :BindableBase,ITransientDependency,IInvoicesViewModel
    {
        private InvoiceDto _invoice;
        private IServiceProvider _serviceProvider;
        private ObservableCollection<InvoiceDto> _items;
        private IEventAggregator EventAggregator;
        private IInvoiceAppService AppService;
        private FlowDocument _document;
        public InvoicesViewModel(IInvoiceAppService appService, IServiceProvider serviceProvider, IEventAggregator eventAggregator)
        {
            AppService = appService;
            _serviceProvider = serviceProvider;
            EventAggregator = eventAggregator;
            SelectedCommand = new DelegateCommand<object[]>(OnItemSelected);
            CreateCommand = new DelegateCommand(Create);
            UpdateCommand = new DelegateCommand(Update);
            ImportCommand = new DelegateCommand(Print);
            DeleteCommand = new DelegateCommand(Delete);
            Items = new ObservableCollection<InvoiceDto>();
            Items.AddRange(AppService.GetListAsync(new InvoiceGetListInput()).Result.Items);
            EventAggregator.GetEvent<InvoiceUpdatedEvent>().Subscribe(OnInvoiceUpdated);
            EventAggregator.GetEvent<InvoiceDeletedEvent>().Subscribe(OnInvoiceDeleted);
            EventAggregator.GetEvent<InvoiceAddedEvent>().Subscribe(OnInvoiceAdded);
            // Document = new InvoiceDocument ();
        }
        public FlowDocument Document { get => _document; set { SetProperty(ref _document, value); } }
        public InvoiceDto SelectedItem { get { return _invoice; }
            set { SetProperty(ref _invoice, value); CreateFlowDocument(); } }
        public ObservableCollection<InvoiceDto> Items { get { return _items; } private set { SetProperty(ref _items, value); } }
        public DelegateCommand<object[]> SelectedCommand { get; private set; }
        public DelegateCommand CreateCommand { get; private set; }
        public DelegateCommand UpdateCommand { get; private set; }
        public DelegateCommand ImportCommand { get; private set; }
        public DelegateCommand DeleteCommand { get; private set; }
        private void OnItemSelected(object[] selectedItems)
        {
            if (selectedItems != null && selectedItems.Count() > 0)
            {
                SelectedItem = (InvoiceDto)selectedItems.FirstOrDefault();
            }
        }
        private void Create()
        {
            var dialog = _serviceProvider.GetRequiredService<InvoiceInputDialog>();
            dialog.ShowDialog();
        }
        private void Update()
        {
            if (SelectedItem == null) return;
            var dialog = _serviceProvider.GetRequiredService<InvoiceInputDialog>();
            dialog.setModel(InvoiceModel.GetModel(SelectedItem));
            dialog.ShowDialog();
        }
        private void Delete()
        {
            if (SelectedItem == null) return;
            Guid id = SelectedItem.Id;
            AppService.DeleteAsync(id);
            EventAggregator.GetEvent<InvoiceDeletedEvent>().Publish(id);
        }
        private void Print()
        {
           
        }
        private void OnInvoiceAdded(InvoiceDto dto)
        {
            SelectedItem = Items.GetOrAdd(c => c.Id == dto.Id, () => dto);
        }
        private void OnInvoiceUpdated(InvoiceDto dto)
        {
            SelectedItem = Items.Where(c => c.Id == dto.Id).Select(c => { c = dto; return c; }).FirstOrDefault();
        }
        private void OnInvoiceDeleted(Guid id)
        {
            Items.RemoveAll(c => c.Id == id);
        }

        private void CreateFlowDocument()
        {
            //Document.DataContext = SelectedItem;
        }
    }
}
