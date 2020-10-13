using FacCigo.ViewModels.Invoices;
using System;
using System.Drawing.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Volo.Abp.DependencyInjection;


namespace FacCigo
{
    /// <summary>
    /// Interaction logic for InvoicePage.xaml
    /// </summary>
    public partial class InvoicePage : Page, ITransientDependency
    {
        public InvoicePage(IInvoicesViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
        public void Print()
        {

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create a PrintDialog
                PrintDialog printDlg = new PrintDialog();
                IDocumentPaginatorSource idpSource = Doc;
                if (printDlg.ShowDialog() == true)
                {

                    printDlg.PrintDocument(idpSource.DocumentPaginator, "Impression Facture");
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message+"\n"+ex.StackTrace + "\n" +ex.Source+"\n");
            }
           
        }
    }
}
