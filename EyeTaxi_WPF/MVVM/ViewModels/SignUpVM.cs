using EyeTaxi_WPF.Commands;
using EyeTaxi_WPF.Enums;
using EyeTaxi_WPF.Interfaces;
using EyeTaxi_WPF.MVVM.Models.DerivedClasses;
using EyeTaxi_WPF.MVVM.Views;
using EyeTaxi_WPF.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EyeTaxi_WPF.MVVM.ViewModels
{
    public class SignUpVM : IEnterPage
    {

        #region Members

        public SignUp Window { get; set; } = null;

        private string username;

        public string Username
        {
            get { return username; }
            set { username = value; OnPropertyChanged(); }
        }

        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged(); }
        }

        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged(); }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
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

        public RelayCommand SignIn { get; set; }

        public RelayCommand SignUp { get; set; }

        #endregion

        #region Methods

        public void Reset()
        {
            Username = String.Empty;
            Email = String.Empty;
            Phone = String.Empty;
            Password = String.Empty;
        }

        public void Signup(object paam)
        {
            if (File.Exists($"{App.UserSubFilePath}/{Username}.json"))
            {
                new MessageBoxCustom("User already Sign Up !", MessageType.Warning, MessageButtons.Ok).ShowDialog();
                return;
            }
            User user = new(Username, Password, Email, Phone);
            UserService.Write(user);
            Window.DialogResult = DialogResult.Success;
            new MessageBoxCustom("Sign Up Succesed !", MessageType.Info, MessageButtons.Ok).ShowDialog();
        }

        public bool CanSignUp(object param) => AllInfoCorrect();

        public bool AllInfoCorrect()
        => !string.IsNullOrWhiteSpace(Username) &&
            !string.IsNullOrWhiteSpace(Email) &&
            !string.IsNullOrWhiteSpace(Phone) &&
            !string.IsNullOrWhiteSpace(Password) &&
            Regex.IsMatch(Username, "^([A-Za-z0-9]){4,20}$") &&
            Regex.IsMatch(Email, "\\b[\\w\\.-]+@[\\w\\.-]+\\.\\w{2,4}\\b") &&
            Regex.IsMatch(Password, "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$") &&
            Regex.IsMatch(Phone, "(\\+?( |-|\\.)?\\d{3}( |-|\\.)?)?(\\(?\\d{3}\\)?|\\d{2})( |-|\\.)?\\d{2}\\d{2}");

        #endregion

        public SignUpVM()
        {
            SignUp = new(Signup, CanSignUp);
        }

    }
}
