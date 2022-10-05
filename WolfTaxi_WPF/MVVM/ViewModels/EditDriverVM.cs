using WolfTaxi_WPF.MVVM.Models.DerivedClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WolfTaxi_WPF.Commands;
using WolfTaxi_WPF.Services;
using WolfTaxi_WPF.MVVM.Views;
using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.Interfaces;

namespace WolfTaxi_WPF.MVVM.ViewModels
{
    public class EditDriverVM : IResetable
    {

        #region Members

        public Driver RefDriver { get; set; }

        private Driver driver;

        public Driver Driver
        {
            get { return driver; }
            set { driver = value; OnPropertyChanged(); }
        }

        #endregion

        #region Commands

        public RelayCommand SaveCommand { get; set; }

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Methods

        public void Save(object param)
        {
            try
            {
                DriverService.UpdateDriver(Driver);
            }
            catch (Exception) { throw; }

            SoundService.Succes();
        }

        public void Reset()
        {
            Driver = new(RefDriver);
        }

        #endregion

        public EditDriverVM()
        {
            SaveCommand = new(Save);
        }

    }
}
