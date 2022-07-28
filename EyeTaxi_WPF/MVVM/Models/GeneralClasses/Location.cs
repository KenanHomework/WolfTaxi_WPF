using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WolfTaxi_WPF.MVVM.Models.GeneralClasses
{
    public class Location
    {
        #region Members

        private float latitude;

        private float longitude;

        private string getString;

        public string GetString
        {
            get { return $"{Latitude} {Longitude}"; }
            set { getString = value; }
        }


        public float Latitude
        {
            get { return latitude; }
            set { latitude = value; OnPropertyChanged(); }
        }

        public float Longitude
        {
            get { return longitude; }
            set { longitude = value; OnPropertyChanged(); }
        }

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public Location(float lonlatitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public Location()
        {

        }
    }
}
