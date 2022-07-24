using EyeTaxi_WPF.MVVM.Models.DerivedClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EyeTaxi_WPF.MVVM.ViewModels
{
    public class EditDriverVM
    {

        #region Members

        private Driver driver;
        private static string iD = "asddsa";

        public Driver Driver
        {
            get { return driver; }
            set { driver = value; OnPropertyChanged(); }
        }

        public static string ID { get => iD; set => iD = value; }
        #endregion


        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Methods

        public void Save() => App.DataFacade.UpdateDriverInfo(Driver);

        #endregion


        public EditDriverVM() { }








    }
}
