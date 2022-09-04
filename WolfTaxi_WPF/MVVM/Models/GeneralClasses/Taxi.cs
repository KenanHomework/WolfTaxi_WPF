using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.MVVM.Models.BaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WolfTaxi_WPF.MVVM.Models.GeneralClasses
{
    public class Taxi
    {

        #region Members

        private TaxiTypes taxiTypes = TaxiTypes.Comfort;


        private Uri iconSource;
        private string model = string.Empty;
        private int year = 1970;
        private string number = String.Empty;


        public Uri IconSource { get => iconSource; set { iconSource = value; OnPropertyChanged(); } }
        public TaxiTypes Type
        {
            get { return taxiTypes; }
            set
            {
                taxiTypes = value;
                IconSource = new Uri($"https://res.cloudinary.com/kysbv/image/upload/v1658306801/WolfTaxi/taxi_type_{value.ToString().ToLower()}.png");
                OnPropertyChanged();
            }
        }
        public string Model { get => model; set { model = value; OnPropertyChanged(); } }
        public int Year { get => year; set { year = value; OnPropertyChanged(); } }
        public string Number { get => number; set { number = value; OnPropertyChanged(); } }
        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion


        public Taxi()
        {
            Model = string.Empty;
            Year = 1970;
            Number = string.Empty;
            Type = TaxiTypes.Fast;
        }

        public Taxi(string model, int year, string number, TaxiTypes type, float pricePerKm)
        {
            Model = model;
            Year = year;
            Number = number;
            Type = type;
        }
    }
}
