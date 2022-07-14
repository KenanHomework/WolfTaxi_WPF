using EyeTaxi_WPF.MVVM.Models.GeneralClasses;
using System;

namespace EyeTaxi_WPF.MVVM.Models.BaseClasses
{
    public abstract class Human
    {

        #region Members

        public string Username { get; set; } = string.Empty;

        public Hash Password { get; set; } = new();

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public abstract string SubFilePath { get; }

        #endregion

        public Human() { }

        protected Human(string username, Hash password, string email, string phone)
        {
            Username = username;
            Password = password;
            Email = email;
            Phone = phone;
        }

    }
}
