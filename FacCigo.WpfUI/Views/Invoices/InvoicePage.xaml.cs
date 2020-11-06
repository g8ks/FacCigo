using FacCigo.ViewModels.Invoices;
using System;
using System.Drawing.Printing;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListInvoices.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("GroupTime");
            view.GroupDescriptions.Add(groupDescription);
            Doc.PageWidth = PrintLayout.CUTSHEETS.Width;
            Doc.PageHeight = PrintLayout.CUTSHEETS.Height;
            Doc.PagePadding = PrintLayout.CUTSHEETS.Margin;
            Doc.ColumnWidth = PrintLayout.CUTSHEETS.ColumnWidth;
            
        }
        public void Print()
        {

        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PageMediaSize pageSize = null;  
                pageSize = new PageMediaSize(PrintLayout.CUTSHEETS.Width,PrintLayout.CUTSHEETS.Height);
                PrintDialog printDlg = new PrintDialog();
                printDlg.PrintTicket.PageMediaSize = pageSize;
                if (printDlg.ShowDialog() == true)
                {
                    PrintTicket pt = printDlg.PrintTicket;
                    Double printableWidth = pt.PageMediaSize.Width.Value;
                    Double printableHeight = pt.PageMediaSize.Height.Value;
                    var pageSize2 = new Size(printDlg.PrintableAreaWidth, printDlg.PrintableAreaHeight);
                    Doc.PageWidth = pageSize2.Width;
                    //Doc.PageHeight = pageSize2.Height;
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
