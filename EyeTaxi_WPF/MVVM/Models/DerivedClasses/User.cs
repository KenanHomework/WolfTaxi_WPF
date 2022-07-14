using EyeTaxi_WPF.MVVM.Models.BaseClasses;
using EyeTaxi_WPF.MVVM.Models.GeneralClasses;
using System.Collections.Generic;

namespace EyeTaxi_WPF.MVVM.Models.DerivedClasses
{
    public class User : Human
    {

        #region Members

        public List<Move> History { get; set; } = new();

        public override string SubFilePath => "dataset/Users";

        #endregion

        #region Methods

        public void AddMove(Move move) => History.Add(move);

        #endregion

        public User() : base() { }

        public User(string username, Hash password, string email, string phone) : base(username, password, email, phone) { }

        public override string ToString() => $"Username: {Username} Password: {Password} Phone: {Phone} Email: {Email}";
    }
}
