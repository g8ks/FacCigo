using FacCigo.Commands;
using FacCigo.ViewModels.ExchangeRates;
using Prism.Services.Dialogs;
using System.Windows;
using System.Windows.Input;
using Volo.Abp.DependencyInjection;

namespace FacCigo
{
    /// <summary>
    /// Interaction logic for ExchangeRateAdd.xaml
    /// </summary>
    public partial class ExchangeRateAdd : Window, ITransientDependency,ICloseable
    {
    
        public ExchangeRateAdd(IExchangeRateAddViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
           
        }

        public void Save(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
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
