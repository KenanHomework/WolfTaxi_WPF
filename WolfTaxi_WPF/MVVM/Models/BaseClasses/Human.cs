﻿using WolfTaxi_WPF.MVVM.Models.GeneralClasses;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WolfTaxi_WPF.Interfaces;

namespace WolfTaxi_WPF.MVVM.Models.BaseClasses
{
    public abstract class Human : IComparable
    {

        #region Members

        private Guid guid;
        private string username = string.Empty;
        private Hash password = new();
        private string email = string.Empty;
        private string phone = string.Empty;


        public Guid ID
        {
            get { return guid; }
            set { guid = value; }
        }

        public string Username { get => username; set { username = value; OnPropertyChanged(); } }

        public Hash Password { get => password; set { password = value; OnPropertyChanged(); } }

        public string Email { get => email; set { email = value; OnPropertyChanged(); } }

        public string Phone { get => phone; set { phone = value; OnPropertyChanged(); } }

        public abstract string SubFilePath { get; }

        #endregion

        #region Methods

        public string GetPath() => $"{SubFilePath}/{Username}.json";

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Implemets

        public int CompareTo(Human? other)
        {
            if (this.guid == other.guid) return 0;
            else return -1;
        }

        public int CompareTo(object? obj)
        {
            if (obj is Human other)
            {
                if (this.guid == other.guid) return 0;
                else return -1;
            }
            else
            {
                if (this.ToString() == obj.ToString()) return 0;
                else return -1;
            }
        }


        #endregion

        public Human() { ID = Guid.NewGuid(); }

        protected Human(string username, string password, string email, string phone) : this(username, password)
        {
            Email = email;
            Phone = phone;
        }

        protected Human(string username, string password) : this()
        {
            Username = username;
            Password = new Hash(password);
        }
    }
}
