﻿using FacCigo.ViewModels.Patients;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Volo.Abp.DependencyInjection;

namespace FacCigo
{
    /// <summary>
    /// Interaction logic for PatientPage.xaml
    /// </summary>
    public partial class PatientPage : Page,ITransientDependency
    {
        public PatientPage(IPatientsViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
