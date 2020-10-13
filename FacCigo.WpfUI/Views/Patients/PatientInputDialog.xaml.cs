using FacCigo.Commands;
using FacCigo.ViewModels.Patients;
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

namespace FacCigo.Views.Patients
{
    /// <summary>
    /// Interaction logic for PatientInputDialog.xaml
    /// </summary>
    public partial class PatientInputDialog : Window,ICloseable,ITransientDependency
    {
        public PatientInputDialog(IPatientInputViewModel  viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
        public void setModel(PatientDto dto)
        {
            ((IPatientInputViewModel)DataContext).UpdateModel(dto);
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
