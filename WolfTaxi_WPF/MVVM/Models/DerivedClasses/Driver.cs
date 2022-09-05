using WolfTaxi_WPF.MVVM.Models.BaseClasses;
using WolfTaxi_WPF.MVVM.Models.GeneralClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WolfTaxi_WPF.Interfaces;
using WolfTaxi_WPF.Services;

namespace WolfTaxi_WPF.MVVM.Models.DerivedClasses
{
    public class Driver : Human
    {

        #region Members

        private float balance = 0;
        private Location location = new();
        private Taxi taxi = new();
        private List<Move> history = new();
        private string sourceOfPP = App.DriverProfilePhoto;
        private int rating = 0;

        public int Rating
        {
            get
            {
                try
                {
                    int totalRating = 0;
                    History.ForEach(m => totalRating += m.Rating);
                    return (int)(totalRating / History.Count);
                }
                catch (Exception) { }
                return 0;
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

        public float GetPrice(float km) => km * App.DataFacade.TypeOfPPK[(int)Taxi.Type];

        #endregion

        #region Override

        public override bool Equals(object? obj)
        {
            try { return this.ID == ((Driver)obj).ID; }
            catch (Exception) { }
            return false;
        }
        public override string ToString() => @$"Username: {Username} Password: {Password} Phone: {Phone} Email: {Email} ID:{ID} ";

        #endregion

        ~Driver()
        {
            if (SourceOfPP != App.DriverProfilePhoto)
                CloudinaryService.DestroyImage(SourceOfPP);
        }

        public Driver() : base()
        {
            Username = String.Empty;
            Password = new();
            Phone = String.Empty;
            Email = String.Empty;
            Rating = 0;
            Balance = 0;
            Location = new();
            Taxi = new();
            History = new();
            SourceOfPP = "https://res.cloudinary.com/kysbv/image/upload/v1658570801/WolfTaxi/driver_pp.png";
        }

        public Driver(Driver driver) : this()
        {
            Username = driver.Username;
            Password = driver.Password;
            Phone = driver.Phone;
            Email = driver.Email;
            Rating = driver.Rating;
            Balance = driver.Balance;
            Location = driver.Location;
            Taxi = driver.Taxi;
            History = driver.History;
            SourceOfPP = driver.SourceOfPP;
        }

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

    }
}
