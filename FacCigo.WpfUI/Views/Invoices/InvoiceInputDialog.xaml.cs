using FacCigo.Commands;
using FacCigo.Models;
using FacCigo.ViewModels.Invoices;
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

namespace FacCigo.Views.Invoices
{
    /// <summary>
    /// Interaction logic for InvoiceInputDialog.xaml
    /// </summary>
    public partial class InvoiceInputDialog : Window,ICloseable,ITransientDependency
    {
        public InvoiceInputDialog(IInvoiceInputViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
            
        }
        public void setModel(InvoiceModel invoice)
        {
            ((IInvoiceInputViewModel)DataContext).UpdateModel(invoice);
        }
        private void WindowDraggableArea_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            if (WindowStateHelper.IsMaximized)
            {
                WindowStateHelper.SetWindowSizeToNormal(this, true);
                DragMove();
            }
            else
            {
                DragMove();
            }

            WindowStateHelper.UpdateLastKnownLocation(Top, Left);
        }
    }
}
