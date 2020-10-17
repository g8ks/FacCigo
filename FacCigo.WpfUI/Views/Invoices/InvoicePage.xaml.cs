using FacCigo.ViewModels.Invoices;
using System;
using System.Drawing.Printing;
using System.Printing;
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
                PageMediaSize pageSize = null;
                //bool bA4 = true;
                //if (bA4)pageSize = new PageMediaSize(PageMediaSizeName.NorthAmericaLetter);
                //else pageSize = new PageMediaSize(PageMediaSizeName.ISOA4);

                // Create a PrintDialog
                PrintDialog printDlg = new PrintDialog();
               
             
                //printDlg.PrintTicket.PageMediaSize = pageSize;
                if (printDlg.ShowDialog() == true)
                {
                    
                  
                    //PrintTicket pt = printDlg.PrintTicket;
                    //Double printableWidth = pt.PageMediaSize.Width.Value;
                   // Double printableHeight = pt.PageMediaSize.Height.Value;
                    //var pageSize2 = new Size(printDlg.PrintableAreaWidth, printDlg.PrintableAreaHeight);
                   // Doc.PageWidth = pageSize2.Width;
                    IDocumentPaginatorSource idpSource = Doc;
                    printDlg.PrintDocument(idpSource.DocumentPaginator, "Impression Facture");
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message+"\n"+ex.StackTrace + "\n" +ex.Source+"\n");
            }
           
        }
    }
}
