using CSVLibrary;
using FacCigo.Commands;
using FacCigo.Commands.ETL;
using FacCigo.Models;
using FacCigo.Views.ETL;
using MaterialDesignColors.Recommended;
using Microsoft.Extensions.DependencyInjection;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ObjectExtending.Modularity;

namespace FacCigo.ViewModels.ETL
{
    public  class CsvReaderViewModel<T,U>: BindableBase,ICsvReaderViewModel<T,U>, ITransientDependency
            where U : CsvableBase,new()
            where T : CsvReader<U>, new()
    {
        private T _reader;
        private readonly IEventAggregator EventAggregator;
        private  readonly IServiceProvider ServiceProvider;
        private ETLOption _option;
        private IList<U> _models;
        public CsvReaderViewModel()
        {

        }
        public CsvReaderViewModel(IEventAggregator eventAggregator,IServiceProvider serviceProvider)
        {
            EventAggregator = eventAggregator;
            ServiceProvider = serviceProvider;
            EventAggregator.GetEvent<ETLOptionsAddedEvent>().Subscribe(OnOptionAdd);
            OkCommand = new DelegateCommand<ICloseable>(Ok);
            ImportCommand = new DelegateCommand(Import);
        }
        public DelegateCommand<ICloseable> OkCommand { get; private set; }
        public DelegateCommand ImportCommand { get; private set; }
        public IList<U> Models { get => _models; set => SetProperty(ref _models, value); }
        private void Ok(ICloseable window)
        {
            if (Models == null) return;
            EventAggregator.GetEvent<ETLFileLoadedEvent>().Publish((IList<object>)Models);
            window.Close();
        }
        private  void  OnOptionAdd(ETLOption option)
        {
            _option = option;
            Models= Reader.Read(_option.FilePath, _option.HasHeader, _option.Delimiter)?.ToList();
        }
        private void Import()
        {
            ETLDialog dialog = ServiceProvider.GetRequiredService<ETLDialog>();
            dialog.ShowDialog();
        }

         public  T Reader 
        {
            get { return _reader; }
            set { SetProperty(ref _reader, value); }
        }
    }
}
