using WolfTaxi_WPF.Commands;
using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.MVVM.Models.DerivedClasses;
using WolfTaxi_WPF.MVVM.Views;
using WolfTaxi_WPF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WolfTaxi_WPF.MVVM.ViewModels
{
    public class ForgotPasswordVM
    {

        #region Members

        public ForgotPassword Window { get; set; } = null;

        private int SecurityCode { get; set; }

        private string usernam;

        public string Username
        {
            get { return usernam; }
            set { usernam = value; OnPropertyChanged(); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        private string infoText;

        public string InfoText
        {
            get { return infoText; }
            set { infoText = value; OnPropertyChanged(); }
        }

        private string infoImg;

        public string InfoImg
        {
            get { return infoImg; }
            set { infoImg = value; OnPropertyChanged(); }
        }



        public DateTime Time { get; set; }

        public DialogResult DialogResult { get; set; } = DialogResult.Cancel;

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Commands

        public RelayCommand SendCodeCommand { get; set; }

        #endregion

        #region Methods

        public void Reset()
        {
            Username = String.Empty;
            Email = String.Empty;
        }

        public bool CanSend(object param)
             => !string.IsNullOrWhiteSpace(Email) &&
                !string.IsNullOrWhiteSpace(Username) &&
                Regex.IsMatch(Username, "^([A-Za-z0-9]){4,20}$") &&
                Regex.IsMatch(Email, "\\b[\\w\\.-]+@[\\w\\.-]+\\.\\w{2,4}\\b");

        public void SendCode(object param)
        {
            if (!CheckEmailAndUsername(param))
                return;
            SendMail();
            EnterSecurityCode enter = new();
            enter.Reset();
            enter.Code = SecurityCode;
            Window.Visibility = System.Windows.Visibility.Hidden;
            enter.ShowDialog();
            if (enter.DialogResult == DialogResult.Success)
            {
                EnterNewPassword password = new();
                password.Reset();
                password.ShowDialog();
                if (password.DialogResult == DialogResult.Success)
                {
                    DialogResult = DialogResult.Success;
                    User user = JSONService.Read<User>($"{App.UserSubFilePath}/{Username}.json");
                    user.Password.UpdateValue(password.Password.Text);
                    UserService.Write(user);
                    Window.DialogResult = DialogResult.Success;
                    SoundService.Succes();
                    Window.Close();
                }
            }
            else if (enter.DialogResult == DialogResult.Cancel)
                Window.Visibility = System.Windows.Visibility.Visible;


        }

        public void SendMail()
        {
            Time = DateTime.Now;
            SecurityCode = new Random().Next(1000, 9999);
            EmailService.Send(Email, "Reset Password Security Code", $"Your Reset Code: {SecurityCode}", "WolfTaxi");
        }

        public bool CheckEmailAndUsername(object param)
        {
            if (!File.Exists($"{App.UserSubFilePath}/{Username}.json"))
            {
                CMessageBox.Show("User not found !", CMessageBox.CMessageTitle.Warning, CMessageBox.CMessageButton.Ok, CMessageBox.CMessageButton.None);

                return false;
            }
            User data;
            try { data = JSONService.Read<User>($"{App.UserSubFilePath}/{Username}.json"); }
            catch (Exception)
            {
                CMessageBox.Show("User Empty !", CMessageBox.CMessageTitle.Warning, CMessageBox.CMessageButton.Ok, CMessageBox.CMessageButton.None);
                ; return false;
            }

            if (data.Email == Email)
            {
                CMessageBox.Show("Security code sended. If don't see mail plese view SPAM.\nPlese write securuty code under 2 minute.", CMessageBox.CMessageTitle.Confirm, CMessageBox.CMessageButton.Ok, CMessageBox.CMessageButton.None);

                return true;
            }
            else
                CMessageBox.Show($"Incorrect Email For {Username}", CMessageBox.CMessageTitle.Warning, CMessageBox.CMessageButton.No, CMessageBox.CMessageButton.None);

            return false;
        }

        #endregion

        public ForgotPasswordVM()
        {
            SendCodeCommand = new(SendCode, CanSend);
            InfoText = "Reset Password";
            InfoImg = "https://cdn1.iconfinder.com/data/icons/web-programming-misc/80/change_password-reset_password-new_password-Forgot_password-1024.png";
        }
    }
}
