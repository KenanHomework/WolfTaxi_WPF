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
using System.Windows.Shapes;
using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.MVVM.Models.BaseClasses;
using WolfTaxi_WPF.MVVM.Models.DerivedClasses;
using WolfTaxi_WPF.MVVM.ViewModels;
using WolfTaxi_WPF.Services;

namespace WolfTaxi_WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for EnterUserPassword.xaml
    /// </summary>
    public partial class EnterUserPassword : Window
    {
        public EnterUserPassword()
        {
            InitializeComponent();
            User = App.Container.GetInstance<ProfilePageVM>().User;
            DataContext = this;
        }

        public string ToolTipStr { get; set; } = "Password Requirements :\n* Use at least 8 characters \n* Use upper and lower case characters \n* Use 1 or more numbers \n* Recommend use special characters";

        public Human User { get; set; }

        public string Password { get; set; }

        public bool Succes { get; set; } = false;

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

        private void ShowPassword_TextChanged(object sender, TextChangedEventArgs e)
            => RegxService.CheckControl(ref ShowPassword, 8, Color.FromRgb(237, 236, 239), "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");

        private void PasswordEye_Click(object sender, RoutedEventArgs e)
        {
            HidePassword.Visibility = PasswordEye.IsChecked == true ? Visibility.Hidden : Visibility.Visible;
            ShowPassword.Visibility = PasswordEye.IsChecked == false ? Visibility.Hidden : Visibility.Visible;
            if (PasswordEye.IsChecked == true) ShowPassword.Text = HidePassword.Password;
            else HidePassword.Password = ShowPassword.Text;
        }

        private void HidePassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            RegxService.CheckControl(ref HidePassword, 8, Color.FromRgb(237, 236, 239), "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");
            ((EnterUserPassword)DataContext).Password = HidePassword.Password;
        }

        private void Submite_Click(object sender, RoutedEventArgs e)
        {
            if (!User.Password.Compare(Password))
            {
                CMessageBox.Show("Incorrect User Password !", CMessageTitle.Warning, CMessageButton.Ok, CMessageButton.None);
                Succes = false;
                return;
            }
            Succes = true;
            this.Close();
        }
    }
}
