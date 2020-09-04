using FacCigo.Samples;
using FacCigo.ViewModels;
using FluentUI;
using MaterialDesignThemes.Wpf;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Settings;

namespace FacCigo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISingletonDependency
    {
        bool MenuClosed = false;
        int index = 0;
        private HwndSource _hwndSource;
        private readonly IServiceProvider _serviceProvider;
        //public MainWindow()
        //{

        //    InitializeComponent();
        //}

        private void Window_OnSourceInitialized(object sender, EventArgs e)
        {
            // Call for resizing effects
            _hwndSource = (HwndSource)PresentationSource.FromVisual(this);
        }
        private void ResizeWindow(ResizeDirection direction)
        {
            SendMessage(_hwndSource.Handle, 0x112, (IntPtr)(61440 + direction), IntPtr.Zero);
        }
        private enum ResizeDirection
        {
            Left = 1,
            Right = 2,
            Top = 3,
            TopLeft = 4,
            TopRight = 5,
            Bottom = 6,
            BottomLeft = 7,
            BottomRight = 8,
        }

        private void Window_OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                Cursor = Cursors.Arrow;
        }

        private void WindowResize_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            var rectangle = (Rectangle)sender;
            if (rectangle == null) return;

            if (WindowStateHelper.IsMaximized) return;

            switch (rectangle.Name)
            {
                case "WindowResizeTop":
                    Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Top);
                    break;
                case "WindowResizeBottom":
                    Cursor = Cursors.SizeNS;
                    ResizeWindow(ResizeDirection.Bottom);
                    break;
                case "WindowResizeLeft":
                    Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Left);
                    break;
                case "WindowResizeRight":
                    Cursor = Cursors.SizeWE;
                    ResizeWindow(ResizeDirection.Right);
                    break;
                case "WindowResizeTopLeft":
                    Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.TopLeft);
                    break;
                case "WindowResizeTopRight":
                    Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.TopRight);
                    break;
                case "WindowResizeBottomLeft":
                    Cursor = Cursors.SizeNESW;
                    ResizeWindow(ResizeDirection.BottomLeft);
                    break;
                case "WindowResizeBottomRight":
                    Cursor = Cursors.SizeNWSE;
                    ResizeWindow(ResizeDirection.BottomRight);
                    break;
            }
        }
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 msg, IntPtr wParam, IntPtr lParam);

        private void WindowResize_OnMouseMove(object sender, MouseEventArgs e)
        {
            var rectangle = (Rectangle)sender;
            if (rectangle == null) return;

            if (WindowStateHelper.IsMaximized) return;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (rectangle.Name)
            {
                case "WindowResizeTop":
                    Cursor = Cursors.SizeNS;
                    break;
                case "WindowResizeTop1":
                    Cursor = Cursors.Hand;
                    break;
                case "WindowResizeBottom":
                    Cursor = Cursors.SizeNS;
                    break;
                case "WindowResizeLeft":
                    Cursor = Cursors.SizeWE;
                    break;
                case "WindowResizeRight":
                    Cursor = Cursors.SizeWE;
                    break;
                case "WindowResizeTopLeft":
                    Cursor = Cursors.SizeNWSE;
                    break;
                case "WindowResizeTopRight":
                    Cursor = Cursors.SizeNESW;
                    break;
                case "WindowResizeBottomLeft":
                    Cursor = Cursors.SizeNESW;
                    break;
                case "WindowResizeBottomRight":
                    Cursor = Cursors.SizeNWSE;
                    break;
            }
        }

        private void WindowDraggableArea_OnPreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            if (WindowStateHelper.IsMaximized)
            {
                WindowStateHelper.SetWindowSizeToNormal(this, true);
                ShowMaximumWindowButton();

                DragMove();
            }
            else
            {
                DragMove();
            }

            WindowStateHelper.UpdateLastKnownLocation(Top, Left);
        }


        private void Window_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowStateHelper.SetWindowMaximized(this);
                WindowStateHelper.BlockStateChange = true;

                var screen = ScreenFinder.FindAppropriateScreen(this);
                if (screen != null)
                {
                    Top = screen.WorkingArea.Top;
                    Left = screen.WorkingArea.Left;
                    Width = screen.WorkingArea.Width;
                    Height = screen.WorkingArea.Height;
                }

                ShowRestoreDownButton();
            }
            else
            {
                if (WindowStateHelper.BlockStateChange)
                {
                    WindowStateHelper.BlockStateChange = false;
                    return;
                }

                WindowStateHelper.UpdateLastKnownNormalSize(Width, Height);
                WindowStateHelper.UpdateLastKnownLocation(Top, Left);
            }
        }

        private void ShowRestoreDownButton()
        {
            maximize.Visibility = Visibility.Collapsed;
            restore.Visibility = Visibility.Visible;
        }

        private void ShowMaximumWindowButton()
        {
            restore.Visibility = Visibility.Collapsed;
            maximize.Visibility = Visibility.Visible;
        }
        public MainWindow(MainViewModel viewModel, IServiceProvider serviceProvider)
        {
            DataContext = viewModel;
            InitializeComponent();
            restore.Visibility = Visibility.Collapsed;
            _serviceProvider = serviceProvider;
            NavMenus.SelectedIndex = 0;
        }

        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MenuClosed)
            {
                Storyboard openMenu = (Storyboard)button.FindResource("OpenMenu");
                openMenu.Begin();
            }
            else
            {
                Storyboard closeMenu = (Storyboard)button.FindResource("CloseMenu");
                closeMenu.Begin();
            }
            MenuClosed = !MenuClosed;
        }
        public void Next_Click(object sender,RoutedEventArgs e) {
            if (index == NavMenus.Items.Count-1) return;
            index += 1;
            NavMenus.SelectedIndex = index;
        }
        public void Previous_Click(object sender, RoutedEventArgs e) {
            if (index == 0) return;
            index -= 1;
            NavMenus.SelectedIndex = index;
            
        }
        public  void BtnExchangeRate_Click(object sender,RoutedEventArgs e)
        {
            try {
                var dialog = _serviceProvider.GetRequiredService<ExchangeRateAdd>();
                dialog.ShowDialog();
            }catch (SqliteException Ex){
                MessageBox.Show(Ex.Message);
            }catch(BusinessException Ex1)
            {
                MessageBox.Show(Ex1.Message);
            }
            
        }
        public void Minimize_Click(object sender,RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        public void Maximize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
            ShowRestoreDownButton();
        }
        public void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        public void Restore_Click(object sender, RoutedEventArgs e)
        {
            ShowMaximumWindowButton();
            WindowStateHelper.SetWindowSizeToNormal(this);
        }
        public void Navigation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MoveCursorMenu(NavMenus.SelectedIndex);

            switch (NavMenus.SelectedIndex)
            {
                case 0:
                    var patientPage = _serviceProvider.GetRequiredService<PatientPage>();
                    mainContent.Navigate(patientPage);
                    break;
                case 1:
                    var examPage = _serviceProvider.GetRequiredService<ExamPage>();
                    mainContent.Navigate(examPage);
                    break;
                case 2:
                    var invoicePage = _serviceProvider.GetRequiredService<InvoicePage>();
                    mainContent.Navigate(invoicePage);
                    break;
                case 3:
                    var paymentPage = _serviceProvider.GetRequiredService<PaymentPage>();
                    mainContent.Navigate(paymentPage);
                    break;

            }
        }
        
        private void MoveCursorMenu(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0,  (60 * index), 0, 0);
        }
        public void Exchange_Click(object sender, EventArgs e)
        {
            try
            {
                var dialog = _serviceProvider.GetRequiredService<ExchangeRateAdd>();
                dialog.ShowDialog();
            }
            catch (SqliteException Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            catch (BusinessException Ex1)
            {
                MessageBox.Show(Ex1.Message);
            }
        }
    }
}


