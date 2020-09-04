using CSVLibrary;
using FacCigo.Commands;
using FacCigo.Models;
using FacCigo.ViewModels.ETL;
using FacCigo.ViewModels.Exams;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Volo.Abp.DependencyInjection;

namespace FacCigo.Views.ETL
{
    /// <summary>
    /// Interaction logic for CsvReaderDialog.xaml
    /// </summary>
    public partial class CsvReaderDialog : Window,ICloseable,ITransientDependency
    {
      
        public CsvReaderDialog()
        {
           
            InitializeComponent();
        }
        private void WindowDraggableArea_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            if (WindowStateHelper.IsMaximized)
            {
                WindowStateHelper.SetWindowSizeToNormal(this, true);
               // ShowMaximumWindowButton();

                DragMove();
            }
            else
            {
                DragMove();
            }

            WindowStateHelper.UpdateLastKnownLocation(Top, Left);
        }
         public void ShowDialog<T,U>(ICsvReaderViewModel<T, U> viewModel)
            where U : CsvableBase, new()
            where T : CsvReader<U>, new()
        {
            if (viewModel == null) throw new NullReferenceException("ViewModel is null");
            DataContext = viewModel;
            ShowDialog();
         }
    }
}
