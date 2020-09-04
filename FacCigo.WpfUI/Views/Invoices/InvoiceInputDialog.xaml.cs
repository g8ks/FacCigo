using FacCigo.Commands;
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
        public InvoiceInputDialog()
        {
            InitializeComponent();
        }
    }
}
