using FacCigo.Commands;
using FacCigo.ViewModels.ETL;
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
    /// Interaction logic for ETLDialog.xaml
    /// </summary>
    public partial class ETLDialog: Window,ICloseable,ITransientDependency
    {
        public ETLDialog(IETLViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
        private void WindowDraggableArea_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            if (WindowStateHelper.IsMaximized)
            {
                WindowStateHelper.SetWindowSizeToNormal(this, true);
                //ShowMaximumWindowButton();

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
