using EyeTaxi_WPF.MVVM.Models.BaseClasses;
using EyeTaxi_WPF.MVVM.Models.GeneralClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EyeTaxi_WPF.MVVM.Models.DerivedClasses
{
    public class Driver : Human
    {

        #region Members

        private float balance = 0;
        private Location location = new();
        private Taxi taxi = new();
        private List<Move> history = new();
        private string sourceOfPP = "https://res.cloudinary.com/kysbv/image/upload/v1658570801/EyeTaxi/driver_pp.png";
        private int rating = 0;

        public int Rating
        {
            get
            {
                int totalRating = 0;
                History.ForEach(m => totalRating += m.Rating);
                return (int)(totalRating / History.Count);
            }
            set { rating = value; }
        }
        public string SourceOfPP { get => sourceOfPP; set { sourceOfPP = value; OnPropertyChanged(); } }
        public float Balance { get => balance; set { balance = value; OnPropertyChanged(); } }
        public Location Location { get => location; set { location = value; OnPropertyChanged(); } }
        public Taxi Taxi { get => taxi; set { taxi = value; OnPropertyChanged(); } }
        public List<Move> History
        {
            get => history;
            set
            {
                history = value;
                OnPropertyChanged();
            }
        }
        public override string SubFilePath => App.DriverSubFilePath;

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


        public Driver() : base() { }

        public Driver(string username, string password, string email, string sourceOfPP, string phone, Location location, Taxi taxi) : base(username, password, email, phone)
        {
            SourceOfPP = sourceOfPP;
            Location = location;
            Taxi = taxi;
        }

        public Driver(string username, string password, string email, string phone, Location location, Taxi taxi) : base(username, password, email, phone)
        {
            Location = location;
            Taxi = taxi;
        }

        public override string ToString() => @$"Username: {Username} Password: {Password} Phone: {Phone} Email: {Email} PricePerKm: {Taxi.Type.PricePerKm}
        ~ Taxi ~ Model: {Taxi.Model} Year: {Taxi.Year} Color: {Taxi.Color}";


    }
}
