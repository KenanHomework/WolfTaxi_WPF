using WolfTaxi_WPF.Commands;
using WolfTaxi_WPF.Interfaces;
using WolfTaxi_WPF.MVVM.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WolfTaxi_WPF.MVVM.ViewModels
{
    public class AdminLoginVM : IEnterPage, IResetable
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

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Commands

        public RelayCommand LoginLocal { get; set; }
         
        public RelayCommand UserClick { get; set; }

        #endregion

        #region Methods

        public void Reset()
        {
            Username = String.Empty;
            Password = String.Empty;
        }

        public void LoginRun(object param)
        {
            if (Password == "Admin123" && Username == "Admin") 
            App.ToAdminPanel();
            else
                new MessageBoxCustom("Incorrect !", MessageType.Warning, MessageButtons.Ok).ShowDialog();
        }

        public bool LoginCanRun(object param) => AllInfoCorrect();

        public bool AllInfoCorrect()
        => !string.IsNullOrWhiteSpace(Password) &&
           !string.IsNullOrWhiteSpace(Username) &&
           Regex.IsMatch(Username, "^([A-Za-z0-9]){4,20}$") &&
           Regex.IsMatch(Password, "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$");

        #endregion

        public AdminLoginVM()
        {
            LoginLocal = new(LoginRun, LoginCanRun);
        }
    }


}
