﻿using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.Interfaces;
using WolfTaxi_WPF.MVVM.ViewModels;
using WolfTaxi_WPF.Services;
using System;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WolfTaxi_WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Page,IResetable
    {
        public SignUp()
        {
            InitializeComponent();
            DataContext = App.Container.GetInstance<SignUpVM>();
        }

        public DialogResult DialogResult { get; set; } = DialogResult.Cancel;

        private void Username_TextChanged(object sender, TextChangedEventArgs e)
                    => RegxService.CheckControl(ref Username, 3, Color.FromRgb(237, 236, 239), "^([A-Za-z0-9]){4,20}$");


        private void Email_TextChanged(object sender, TextChangedEventArgs e)
                    => RegxService.CheckControl(ref Email, 3, Color.FromRgb(237, 236, 239), "\\b[\\w\\.-]+@[\\w\\.-]+\\.\\w{2,4}\\b");


        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
                    => RegxService.CheckControl(ref Phone, 10, Color.FromRgb(237, 236, 239), "^((\\050|055|099|077|070|010|051)|0)\\s?(4(099|055|050|077|070|010)\\d{1}|\\d{1})\\/?(((\\s?|\\.?)\\d{2}){3}|((\\s?|\\.)\\d{3}){2})\\b");

        private void PasswordEye_Click(object sender, RoutedEventArgs e)
        {
            HidePassword.Visibility = PasswordEye.IsChecked == true ? Visibility.Hidden : Visibility.Visible;
            ShowPassword.Visibility = PasswordEye.IsChecked == false ? Visibility.Hidden : Visibility.Visible;
            if (PasswordEye.IsChecked == true) ShowPassword.Text = HidePassword.Password;
            else HidePassword.Password = ShowPassword.Text;
        }

        private void HidePassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (RegxService.CheckControl(ref HidePassword, 8, Color.FromRgb(237, 236, 239), "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$"))
                ((SignUpVM)DataContext).Password = HidePassword.Password;
        }

        private void ShowPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RegxService.CheckControl(ref ShowPassword, 8, Color.FromRgb(237, 236, 239), "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$"))
                ((SignUpVM)DataContext).Password = ShowPassword.Text;
        }

        public void Reset() => App.Container.GetInstance<SignUpVM>().Reset();
    }
}
