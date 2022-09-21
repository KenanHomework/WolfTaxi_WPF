using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WolfTaxi_WPF.MVVM.ViewModels;
using WolfTaxi_WPF.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace WolfTaxi_WPF.MVVM.Views.Pages
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        public ProfilePage()
        {
            InitializeComponent();
            App.Container.GetInstance<ProfilePageVM>().Page = this;
            DataContext = App.Container.GetInstance<ProfilePageVM>();
            InfoColor = new SolidColorBrush(Color.FromRgb(96, 104, 108));
            ChangeHitColors();
        }

        public SolidColorBrush InfoColor { get; set; }


        #region ClickMethods

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
                ((ProfilePageVM)DataContext).User.Password.UpdateValue(HidePassword.Password);
        }

        private void ShowPassword_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (RegxService.CheckControl(ref ShowPassword, 8, Color.FromRgb(237, 236, 239), "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$"))
                ((ProfilePageVM)DataContext).User.Password.UpdateValue(ShowPassword.Text);
        }

        private void PhoneTBX_TextChanged(object sender, TextChangedEventArgs e)
            => RegxService.CheckControl(ref PhoneTBX, 10, Color.FromRgb(237, 236, 239), "(\\+?( |-|\\.)?\\d{3}( |-|\\.)?)?(\\(?\\d{3}\\)?|\\d{2})( |-|\\.)?\\d{2}\\d{2}");

        private void EmailTBX_TextChanged(object sender, TextChangedEventArgs e)
            => RegxService.CheckControl(ref EmailTBX, 3, Color.FromRgb(237, 236, 239), "\\b[\\w\\.-]+@[\\w\\.-]+\\.\\w{2,4}\\b");


        private void UsernameTBX_TextChanged(object sender, TextChangedEventArgs e)
            => RegxService.CheckControl(ref UsernameTBX, 3, Color.FromRgb(237, 236, 239), "^([A-Za-z0-9]){4,20}$");

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion


        #region Methods

        public void EditPropertys(bool IsEditing)
        {
            InfoColor = new SolidColorBrush(IsEditing ? Color.FromRgb(179, 179, 179) : Color.FromRgb(96, 104, 108));
            ChangeHitColors();

            EmailTBX.IsEnabled = IsEditing;
            UsernameTBX.IsEnabled = IsEditing;
            PhoneTBX.IsEnabled = IsEditing;
            ShowPass.IsEnabled = IsEditing;
        }

        public void ChangeHitColors()
        {
            UsernameChvrn.Foreground = InfoColor;
            UsernameLbl.Foreground = InfoColor;

            EmailChvrn.Foreground = InfoColor;
            EmailLbl.Foreground = InfoColor;

            PhoneChvrn.Foreground = InfoColor;
            PhoneLbl.Foreground = InfoColor;

            PasswordChvrn.Foreground = InfoColor;
            PasswordLbl.Foreground = InfoColor;
        }

        public void PrepareEdit(bool edit)
        {
            Edit.Visibility = edit ? Visibility.Hidden : Visibility.Visible;
            Cancel.Visibility = edit ? Visibility.Visible : Visibility.Collapsed;
            Save.Visibility = edit ? Visibility.Visible : Visibility.Collapsed;
            BrowsePP.IsEnabled = edit;
            EditPropertys(edit);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            PrepareEdit(true);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            App.Container.GetInstance<ProfilePageVM>().Save.Execute(sender);
            PrepareEdit(false);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            App.Container.GetInstance<ProfilePageVM>().ShowPassword(false);
            PrepareEdit(false);
        }

        #endregion
    }
}
