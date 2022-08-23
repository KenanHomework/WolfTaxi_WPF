﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using CloudinaryDotNet;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CloudinaryDotNet.Actions;
using WolfTaxi_WPF.MVVM.ViewModels;
using WolfTaxi_WPF.Enums;
using MaterialDesignThemes.Wpf;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WolfTaxi_WPF.Services;
using System.IO;

namespace WolfTaxi_WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for AddDriver.xaml
    /// </summary>
    public partial class AddDriver : Window
    {

        public AddDriver()
        {
            InitializeComponent();
            App.Container.GetInstance<AddDriverVM>().Reset();
            App.Container.GetInstance<AddDriverVM>().Window = this;
            DataContext = App.Container.GetInstance<AddDriverVM>();
            TaxiTypeComboBox.ItemsSource = Enum.GetValues(typeof(TaxiTypes));
        }

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Controls Checks

        private void Username_TextChanged(object sender, TextChangedEventArgs e)
                        => RegxService.CheckControl(ref Username, 3, Color.FromRgb(179, 179, 179), "^([A-Za-z0-9]){4,20}$");


        private void Email_TextChanged(object sender, TextChangedEventArgs e)
                    => RegxService.CheckControl(ref Email, 3, Color.FromRgb(179, 179, 179), "\\b[\\w\\.-]+@[\\w\\.-]+\\.\\w{2,4}\\b");


        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
                    => RegxService.CheckControl(ref Phone, 10, Color.FromRgb(179, 179, 179), "(\\+?( |-|\\.)?\\d{3}( |-|\\.)?)?(\\(?\\d{3}\\)?|\\d{2})( |-|\\.)?\\d{2}\\d{2}");

        private void Password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RegxService.CheckControl(ref Password, 8, Color.FromRgb(179, 179, 179), "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
            App.Container.GetInstance<SignUpVM>().Password = Password.Password;
        }

        private void Locatoion_TextChanged(object sender, TextChangedEventArgs e)
                         => RegxService.CheckControl(ref Location, 3, Color.FromRgb(179, 179, 179));

        private void Model_TextChanged(object sender, TextChangedEventArgs e)
                           => RegxService.CheckControl(ref Model, 3, Color.FromRgb(179, 179, 179));

        private void Year_TextChanged(object sender, TextChangedEventArgs e)
                           => RegxService.CheckCarYear(ref Year, Color.FromRgb(179, 179, 179));

        private void Number_TextChanged(object sender, TextChangedEventArgs e)
                         => RegxService.CheckControl(ref Number, 7, Color.FromRgb(179, 179, 179));

        #endregion

        private void ResizeButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                switch (btn.Content.ToString())
                {
                    case "_":
                        this.WindowState = WindowState.Minimized;
                        break;
                    case "X":
                        App.DataFacade.Save();
                        this.Close();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void TaxiTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            App.Container.GetInstance<AddDriverVM>().Driver.Taxi.Type = (TaxiTypes)TaxiTypeComboBox.SelectedItem;
            TaxiIcon.ImageSource = BitmapService.GetBitmapImageFromUrl(App.Container.GetInstance<AddDriverVM>().Driver.Taxi.IconSource.ToString());
        }

    }
}