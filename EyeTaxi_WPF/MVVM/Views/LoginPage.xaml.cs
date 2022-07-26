using EyeTaxi_WPF.Enums;
using EyeTaxi_WPF.Interfaces;
using EyeTaxi_WPF.MVVM.ViewModels;
using EyeTaxi_WPF.Services;
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

namespace EyeTaxi_WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, IResetable
    {
        public LoginPage()
        {
            InitializeComponent(); App.Container.GetInstance<LoginPageVM>();
            DataContext = App.Container.GetInstance<LoginPageVM>();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
            => RegxService.CheckControl(ref Username, 3, Color.FromRgb(237, 236, 239), "^([A-Za-z0-9]){4,20}$");

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RegxService.CheckControl(ref Password, 8, Color.FromRgb(237, 236, 239), "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
            App.Container.GetInstance<LoginPageVM>().Password = Password.Password;
        }

        public void Reset() => App.Container.GetInstance<LoginPageVM>().Reset();

        private void ResetPassword(object sender, RoutedEventArgs e)
        {
            ForgotPassword forgotPassword = new();
            forgotPassword.Reset();
            forgotPassword.ShowDialog();
            if (forgotPassword.DialogResult == DialogResult.Success)
            {
                new MessageBoxCustom("Succes Reset Password", MessageType.Success, MessageButtons.Ok).ShowDialog();
                Username.Text = forgotPassword.Username.Text;
            }
        }
    }
}
