using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WolfTaxi_WPF.Commands;
using WolfTaxi_WPF.MVVM.Models.DerivedClasses;

namespace WolfTaxi_WPF.MVVM.ViewModels
{
    public class AddDriverVM
    {
        #region Members

        private Driver driver = new();

        public Driver Driver
        {
            get { return driver; }
            set { driver = value; OnPropertyChanged(); }
        }

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Commands

        public RelayCommand BrowsePPSource { get; set; }

        public RelayCommand Add { get; set; }

        #endregion

        #region Methods

        public void Browse(object param)
        {

        }

        public void AddDriver(object param)
        {

        }

        #endregion

        public AddDriverVM()
        {
            BrowsePPSource = new(Browse);
            Add = new(AddDriver);
        }
    }
}