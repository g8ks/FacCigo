using CSVLibrary;
using FacCigo.Commands.ETL;
using FacCigo.Models;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace FacCigo.ViewModels.ETL
{
    public class ETLViewModel: ValidatableBindableBase, ITransientDependency,IETLViewModel
    {

        private bool _header;
        private string _filepath;
        private char _delimiter;
        private string _errorText;
        private IEventAggregator EventAggregator;
        
        public ETLViewModel(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            Delimiter = ',';
            DoneCommand = new DelegateCommand(Done);
            OpenFileCommand= new DelegateCommand(SelectFile);
        }
        public bool HasHeader { get { return _header; }
            set { SetProperty(ref _header, value); }
        }

        
        public String FilePath
        {
            get { return _filepath; }
            set
            {
                SetProperty(ref _filepath, value);
            }
        }
        public char Delimiter
        {
            get { return _delimiter; }
            set
            {
                SetProperty(ref _delimiter, value);
            }
        }
        public string ErrorText
        {
            get { return _errorText; }
            set { SetProperty(ref _errorText, value); }
        }
        public DelegateCommand OpenFileCommand;
        public DelegateCommand DoneCommand;

        private void SelectFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|Csv files (*.csv)|*.csv";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileNames[0];
            }
        }
        public void Done()
        {
            if (HasErrors) return;
            ETLOption option = new ETLOption(FilePath, Delimiter, HasErrors);
            EventAggregator.GetEvent<ETLOptionsAddedEvent>().Publish(option);
        }

    }
}
