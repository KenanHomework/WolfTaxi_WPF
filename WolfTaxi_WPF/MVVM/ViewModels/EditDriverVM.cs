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
using System.Windows;
using System.Security.Cryptography;
using System.Windows.Forms;
using WolfTaxi_WPF.MVVM.Views.Pages;
using System.Text.RegularExpressions;
using System.Windows.Media;

namespace WolfTaxi_WPF.MVVM.ViewModels
{
    public class EditDriverVM : IResetable
    {

        #region Members

        public EditDriver Page { get; set; }

        bool dialogResult = false;

        public string ToolTipStr { get; set; } = "Password Requirements :\n* Use at least 8 characters \n* Use upper and lower case characters \n* Use 1 or more numbers \n* Recommend use special characters";

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

        public RelayCommand ShowPass { get; set; }


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
                DriverService.UpdateDriver(Driver, true);
            }
            catch (Exception exc)
            {
                SoundService.Error();
                CMessageBox.Show(exc.Message, CMessageTitle.Error, CMessageButton.Ok, CMessageButton.None);
                return;
            }

            SoundService.Succes();
            Page.Close();
        }

        public bool SaveCanRun(object param) => AllInfoCorrect();

        public void ShowPassword(bool show)
        {
            Page.ShowPass.Visibility = show ? Visibility.Collapsed : Visibility.Visible;
            Page.PasswordGRID.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
        }

        public void ShowPassRun(object param)
        {
            EnterUserPassword enterAdmin = new();
            enterAdmin.User = Driver;
            enterAdmin.ShowDialog();
            if (enterAdmin.Succes == false) return;
            ShowPassword(true);
        }

        public void Reset()
            => Driver = new(RefDriver);

        public bool AllInfoCorrect()
    => !string.IsNullOrWhiteSpace(Driver.Username) &&
       !string.IsNullOrWhiteSpace(Driver.Email) &&
       !string.IsNullOrWhiteSpace(Driver.Phone) &&
       !string.IsNullOrWhiteSpace(Driver.Taxi.Model) &&
       !string.IsNullOrWhiteSpace(Driver.Taxi.Number) &&
       Driver.Phone.Length == 10 &&
       Regex.IsMatch(Driver.Username, "(-?([A-Z].\\s)?([A-Z][a-z]+)\\s?)+([A-Z]'([A-Z][a-z]+))?") &&
       Regex.IsMatch(Driver.Email, "\\b[\\w\\.-]+@[\\w\\.-]+\\.\\w{2,4}\\b") &&
       Regex.IsMatch(Driver.Phone, "^((\\050|055|099|077|070|010|051)|0)\\s?(4(099|055|050|077|070|010)\\d{1}|\\d{1})\\/?(((\\s?|\\.?)\\d{2}){3}|((\\s?|\\.)\\d{3}){2})\\b") &&
       RegxService.CheckControl(ref Page.Model, 3, Color.FromRgb(179, 179, 179)) &&
       RegxService.CheckCarYear(ref Page.Year, Color.FromRgb(179, 179, 179)) &&
       RegxService.CheckControl(ref Page.Number, 7, Color.FromRgb(179, 179, 179));


        #endregion

        public EditDriverVM()
        {
            SaveCommand = new(Save, SaveCanRun);
            ShowPass = new(ShowPassRun);

        }

    }
}
