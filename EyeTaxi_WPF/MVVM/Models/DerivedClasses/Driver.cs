using EyeTaxi_WPF.MVVM.Models.BaseClasses;
using EyeTaxi_WPF.MVVM.Models.GeneralClasses;
using System.Collections.Generic;

namespace EyeTaxi_WPF.MVVM.Models.DerivedClasses
{
    public class Driver : Human
    {

        #region Members

        public float Balance { get; set; } = 0;

        public Location Location { get; set; } = new();

        public Taxi Taxi { get; set; } = new();

        public List<Move> History { get; set; } = new();

        public override string SubFilePath => App.DriverSubFilePath;

        #endregion

        #region Methods

        public void AddMove(Move move) => History.Add(move);

        #endregion


        public Driver() : base() { }

        public Driver(string username, string password, string email, string phone, Location location, Taxi taxi) : base(username, password, email, phone)
        {
            Location = location;
            Taxi = taxi;
        }

        public override string ToString() => @$"Username: {Username} Password: {Password} Phone: {Phone} Email: {Email}
        ~ Taxi ~ Model: {Taxi.Model} Year: {Taxi.Year} Color: {Taxi.Color}";


    }
}
