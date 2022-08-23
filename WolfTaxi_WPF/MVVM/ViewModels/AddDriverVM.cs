using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WolfTaxi_WPF.Commands;
using WolfTaxi_WPF.Interfaces;
using WolfTaxi_WPF.MVVM.Models.DerivedClasses;
using WolfTaxi_WPF.MVVM.Views;
using WolfTaxi_WPF.Services;

namespace WolfTaxi_WPF.MVVM.ViewModels
{
    public class AddDriverVM : IResetable
    {
        #region Members

        public AddDriver Window { get; set; }

        private Driver driver = new();

        bool dialogResult = false;
        OpenFileDialog ofd;

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

        public RelayCommand Close { get; set; }

        #endregion

        #region Methods

        public void Browse(object param)
        {
            ofd = new OpenFileDialog();
            ofd.Filter = "Image File (* png)| *.png";
            dialogResult = (bool)ofd.ShowDialog();
            CloudinaryService.UploadImage(ofd.FileName, "tempdriverpp", App.TempCloudinaryFolderPath);
            Window.ProfilePhoto.ImageSource = null;
            Window.ProfilePhoto.ImageSource = BitmapService.GetBitmapImageFromUrl(CloudinaryService.GetSource(App.TempCloudinaryFolderPath, "tempdriverpp"));
        }

        public bool CandAddDriver(object param) => AllInfoCorrect();

        public void AddDriver(object param)
        {
            if (dialogResult)
                CloudinaryService.DestroyImage("tempdriverprofilephoto");
            Driver.SourceOfPP = dialogResult ? CloudinaryService.UploadImage(ofd.FileName, Driver.Username, App.DriverCloudinaryFolderPath) : App.DriverProfilePhoto;
            App.DataFacade.AddDriver(Driver);
            Reset();
            Window.Close();
        }

        public void CloseAD(object param)
        {
            Reset();
            Window.Close();
        }

        public void Reset()
        {
            Driver = new();
        }

        public bool AllInfoCorrect()
            => !string.IsNullOrWhiteSpace(Driver.Username) &&
               !string.IsNullOrWhiteSpace(Driver.Email) &&
               !string.IsNullOrWhiteSpace(Driver.Phone) &&
               !string.IsNullOrWhiteSpace(Driver.Taxi.Model) &&
               !string.IsNullOrWhiteSpace(Driver.Taxi.Number) &&
               Driver.Phone.Length == 10 &&
               !string.IsNullOrWhiteSpace(Window.Password.Password) &&
               Regex.IsMatch(Driver.Username, "^([A-Za-z0-9]){4,20}$") &&
               Regex.IsMatch(Driver.Email, "\\b[\\w\\.-]+@[\\w\\.-]+\\.\\w{2,4}\\b") &&
               Regex.IsMatch(Window.Password.Password, "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$") &&
               Regex.IsMatch(Driver.Phone, "(\\+?( |-|\\.)?\\d{3}( |-|\\.)?)?(\\(?\\d{3}\\)?|\\d{2})( |-|\\.)?\\d{2}\\d{2}") &&
               RegxService.CheckControl(ref Window.Location, 3, Color.FromRgb(179, 179, 179)) &&
            RegxService.CheckControl(ref Window.Model, 3, Color.FromRgb(179, 179, 179)) &&
            RegxService.CheckCarYear(ref Window.Year, Color.FromRgb(179, 179, 179)) &&
            RegxService.CheckControl(ref Window.Number, 7, Color.FromRgb(179, 179, 179));

        #endregion

        public AddDriverVM()
        {
            BrowsePPSource = new(Browse);
            Add = new(AddDriver, CandAddDriver);
            Close = new(CloseAD);
        }
    }
}