using WolfTaxi_WPF.Commands;
using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using WolfTaxi_WPF.MVVM.Views;

namespace WolfTaxi_WPF.MVVM.ViewModels
{
    public class LoginPageVM : IEnterPage, IResetable
    {

        #region Members

        private string usernam;

        public string Username
        {
            get { return usernam; }
            set { usernam = value; OnPropertyChanged(); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(); }
        }

        private bool remeber = true;

        public bool Remember
        {
            get { return remeber; }
            set { remeber = value; OnPropertyChanged(); }
        }

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Commands

        public RelayCommand LoginClick { get; set; }

        public RelayCommand LoginLocal { get; set; }

        public RelayCommand ForgotPasswordClick { get; set; }

        public RelayCommand ResetPasswordClick { get; set; }

        public RelayCommand SignUpClick { get; set; }

        public RelayCommand AdminClick { get; set; }

        #endregion

        #region Methods

        bool LoginCanRun(object param) => AllInfoCorrect();

        public void Login(object param)
        {
            if (AllInfoCorrect())
            {
                LoginClick.Execute(param);
            }
        }

        public bool AllInfoCorrect() =>
            (!string.IsNullOrWhiteSpace(Password) &&
            !string.IsNullOrWhiteSpace(Username) &&
            Regex.IsMatch(Username, "^([A-Za-z0-9]){4,20}$") &&
            Regex.IsMatch(Password, "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$"));


        //     !string.IsNullOrWhiteSpace(Password) &&
        //!string.IsNullOrWhiteSpace(Username) &&
        //Regex.IsMatch(Username, "^([A-Za-z0-9]){4,20}$") &&
        //Regex.IsMatch(Password, "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");

        public void Reset()
        {
            Username = String.Empty;
            Password = String.Empty;
        }

        private void ResetPassword(object param)
        {
            ForgotPassword forgotPassword = new(Username);
            forgotPassword.Reset();
            forgotPassword.ShowDialog();
            if (forgotPassword.DialogResult == DialogResult.Success)
            {
                CMessageBox.Show("Succes Reset Password !", CMessageTitle.Info, CMessageButton.Ok, CMessageButton.None);

                Username = ((ForgotPasswordVM)forgotPassword.DataContext).Username;
            }
        }

        public bool CanResetPassword(object param) => (!string.IsNullOrWhiteSpace(Username) && Regex.IsMatch(Username, "^([A-Za-z0-9]){4,20}$"));

        #endregion

        public LoginPageVM()
        {
            LoginLocal = new(Login, LoginCanRun);
            ResetPasswordClick = new(ResetPassword, CanResetPassword);
        }

    }
}
