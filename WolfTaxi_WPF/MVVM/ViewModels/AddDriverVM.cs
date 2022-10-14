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
using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.Interfaces;
using WolfTaxi_WPF.MVVM.Models.DerivedClasses;
using WolfTaxi_WPF.MVVM.Views;
using WolfTaxi_WPF.Services;

namespace WolfTaxi_WPF.MVVM.ViewModels
{
    public class AddDriverVM : IResetable
    {
        #region Members

        public string UrlTempPP { get; set; } = string.Empty;
        public string FinCode { get; set; } = string.Empty;
        public string PasswordTemp { get; set; } = string.Empty;
        public AddDriver Window { get; set; }
        bool dialogResult = false;
        OpenFileDialog ofd;
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

        public RelayCommand Close { get; set; }

        #endregion

        #region Methods

        public void Browse(object param)
        {
            ofd = new OpenFileDialog();
            ofd.Filter = "Image File (* png)| *.png";
            dialogResult = (bool)ofd.ShowDialog();
            CloudinaryService.DestroyImage("tempdriverpp", App.TempCloudinaryFolderPath);
            UrlTempPP = CloudinaryService.UploadImage(ofd.FileName, "tempdriverpp", App.TempCloudinaryFolderPath);
            Window.ProfilePhoto.ImageSource = null;
            Window.ProfilePhoto.ImageSource = BitmapService.GetBitmapImageFromUrl(UrlTempPP);
            SoundService.Succes();
        }

        public bool CandAddDriver(object param) => AllInfoCorrect();

        public void AddDriver(object param)
        {
            if (dialogResult)
                CloudinaryService.DestroyImage("tempdriverpp", App.TempCloudinaryFolderPath);
            Driver.SourceOfPP = dialogResult ? CloudinaryService.UploadImage(ofd.FileName, Driver.ID.ToString(), App.DriverCloudinaryFolderPath) : App.DriverProfilePhoto;

            Driver temp = new(Driver.Username, PasswordTemp, FinCode, Driver.Email, Driver.SourceOfPP, Driver.Phone, Driver.Location, Driver.Taxi);

            ProcessResult res;

            try
            {
                res = App.DataFacade.AddDriver(temp);
            }
            catch (Exception exc)
            {
                SoundService.Error();
                CMessageBox.Show(exc.Message, CMessageTitle.Error, CMessageButton.Ok, CMessageButton.None);
                return;
            }

            if (res != ProcessResult.Success)
                return;
            Reset();
            SoundService.Succes();
            Window.Close();
        }

        public void CloseAD(object param)
        {
            Reset();
            SoundService.Notification();
            Window.Close();
        }

        public void Reset()
        {
            Driver = new();
            FinCode = String.Empty;
            PasswordTemp = String.Empty;
        }

        public bool AllInfoCorrect()
            => !string.IsNullOrWhiteSpace(Driver.Username) &&
               !string.IsNullOrWhiteSpace(Driver.Email) &&
               !string.IsNullOrWhiteSpace(Driver.Phone) &&
               !string.IsNullOrWhiteSpace(Driver.Taxi.Model) &&
               !string.IsNullOrWhiteSpace(Driver.Taxi.Number) &&
               !string.IsNullOrWhiteSpace(FinCode) &&
               !string.IsNullOrWhiteSpace(Window.Password.Password) &&
               Driver.Phone.Length == 10 &&
               Regex.IsMatch(Driver.Username, "(-?([A-Z].\\s)?([A-Z][a-z]+)\\s?)+([A-Z]'([A-Z][a-z]+))?") &&
               Regex.IsMatch(Driver.Email, "\\b[\\w\\.-]+@[\\w\\.-]+\\.\\w{2,4}\\b") &&
               Regex.IsMatch(Window.Password.Password, "^(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$") &&
               Regex.IsMatch(Driver.Phone, "^((\\050|055|099|077|070|010|051)|0)\\s?(4(099|055|050|077|070|010)\\d{1}|\\d{1})\\/?(((\\s?|\\.?)\\d{2}){3}|((\\s?|\\.)\\d{3}){2})\\b") &&
               Regex.IsMatch(FinCode, "(([a-zA-Z0-9]).{6})") &&
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
            UrlTempPP = CloudinaryService.GetSource("tempdriverpp", App.TempCloudinaryFolderPath);
        }
    }
}