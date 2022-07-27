using WolfTaxi_WPF.MVVM.Models.BaseClasses;
using WolfTaxi_WPF.MVVM.Models.GeneralClasses;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WolfTaxi_WPF.MVVM.Models.DerivedClasses
{
    public class User : Human
    {

        #region Members

        private List<Move> history = new();


        public List<Move> History { get => history; set { history = value; OnPropertyChanged(); } }

        public override string SubFilePath => App.UserSubFilePath;

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Methods

        public void AddMove(Move move) => History.Add(move);

        #endregion

        public User() : base() { }

        public User(string username, string password, string email, string phone) : base(username, password, email, phone) { }

        public User(string username, string password) : base(username, password) { }

        public override string ToString() => $"Username: {Username} Password: {Password} Phone: {Phone} Email: {Email}";
    }
}
