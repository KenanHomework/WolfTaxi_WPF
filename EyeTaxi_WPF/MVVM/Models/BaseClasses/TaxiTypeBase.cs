using EyeTaxi_WPF.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi_WPF.MVVM.Models.BaseClasses
{
    public abstract class TaxiTypeBase
    {
        #region Members

        private TaxiTypes taxiTypes;

        public TaxiTypes Type
        {
            get { return taxiTypes; }
            set { taxiTypes = value; OnPropertyChanged(); }
        }

        private float pricePerKm;

        public float PricePerKm
        {
            get { return pricePerKm; }
            set { pricePerKm = value; OnPropertyChanged(); }
        }

        public string IconSource { get; set; }

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion


        public TaxiTypeBase(TaxiTypes type, float pricePerKm, string iconSource)
        {
            Type = type;
            PricePerKm = pricePerKm;
            IconSource = iconSource;
        }

        public TaxiTypeBase() { }

    }
}
