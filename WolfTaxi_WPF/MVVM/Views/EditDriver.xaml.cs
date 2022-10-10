using WolfTaxi_WPF.MVVM.ViewModels;
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
using WolfTaxi_WPF.Services;
using WolfTaxi_WPF.Enums;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WolfTaxi_WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for EditDriver.xaml
    /// </summary>
    public partial class EditDriver : Window
    {
        public EditDriver()
        {
            InitializeComponent();
            App.Container.GetInstance<EditDriverVM>().Page = this;
            DataContext = App.Container.GetInstance<EditDriverVM>();
        }

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

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            SoundService.Notification();
            this.Close();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                App.Container.GetInstance<EditDriverVM>().SaveCommand.Execute(this);
            }
            catch (Exception exc)
            {
                SoundService.Error();
                CMessageBox.Show(exc.Message, CMessageTitle.Error, CMessageButton.Ok, CMessageButton.None);
                return;
            }
            this.Close();
        }

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
                ((EditDriverVM)DataContext).Driver.Password.UpdateValue(HidePassword.Password);
        }

        private void ShowPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RegxService.CheckControl(ref ShowPassword, 8, Color.FromRgb(237, 236, 239), "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$"))
                ((EditDriverVM)DataContext).Driver.Password.UpdateValue(ShowPassword.Text);
        }

        private void Username_TextChanged(object sender, TextChangedEventArgs e)
                     => RegxService.CheckControl(ref Username, 4, Color.FromRgb(237, 236, 239), "(-?([A-Z].\\s)?([A-Z][a-z]+)\\s?)+([A-Z]'([A-Z][a-z]+))?");


        private void Email_TextChanged(object sender, TextChangedEventArgs e)
                    => RegxService.CheckControl(ref Email, 3, Color.FromRgb(179, 179, 179), "\\b[\\w\\.-]+@[\\w\\.-]+\\.\\w{2,4}\\b");


        private void Phone_TextChanged(object sender, TextChangedEventArgs e)
                    => RegxService.CheckControl(ref Phone, 10, Color.FromRgb(179, 179, 179), "^((\\050|055|099|077|070|010|051)|0)\\s?(4(099|055|050|077|070|010)\\d{1}|\\d{1})\\/?(((\\s?|\\.?)\\d{2}){3}|((\\s?|\\.)\\d{3}){2})\\b");

    }
}
