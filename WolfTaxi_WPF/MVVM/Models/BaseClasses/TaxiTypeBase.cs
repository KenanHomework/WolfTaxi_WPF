using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WolfTaxi_WPF.Enums;

namespace WolfTaxi_WPF.MVVM.Models.BaseClasses
{
    public abstract class TaxiTypeBase
    {

        #region Members

        private string name = string.Empty;

        private string iconSource = string.Empty;

        private float price = 0.0f;

        private TaxiTypes types = TaxiTypes.Comfort;



        public TaxiTypes Type
        {
            get { return types; }
            set { types = value; OnPropertyChanged(); }
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }

        public float Price
        {
            get { return price; }
            set { price = value; OnPropertyChanged(); }
        }

        public string IconSource
        {
            get { return iconSource; }
            set { iconSource = value; OnPropertyChanged(); }
        }

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public TaxiTypeBase(string name, string iconSource, TaxiTypes type, float price)
        {
            this.Name = name;
            this.IconSource = iconSource;
            this.Price = price;
            this.Type = type;
        }

        public override string ToString() => $"{Name}";

        public TaxiTypeBase()
        {
            Name = "Empty";
            IconSource = App.ComfortTaxiIconSource;
            Type = TaxiTypes.Comfort;
            Price = 0.0f;
        }
    }
}
