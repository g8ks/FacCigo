using FacCigo.Commands;
using FacCigo.ViewModels.Exams;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Volo.Abp.DependencyInjection;

namespace FacCigo.Views.Exams
{
    /// <summary>
    /// Interaction logic for ExamAddOrEditUserControl.xaml
    /// </summary>
    public partial class ExamInputDialog : Window,ICloseable,ITransientDependency
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);
        public ExamInputDialog(IExamInputViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
        public void setModel(ExamDto dto)
        {
            ((IExamInputViewModel)DataContext).UpdateModel(dto);
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
