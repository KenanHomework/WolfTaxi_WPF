using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using WolfTaxi_WPF.Commands;
using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.MVVM.Models.DerivedClasses;
using WolfTaxi_WPF.MVVM.Views;
using WolfTaxi_WPF.MVVM.Views.Pages;
using WolfTaxi_WPF.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WolfTaxi_WPF.MVVM.ViewModels
{
    public class ProfilePageVM
    {

        #region Members

        private User user;

        public string ToolTipStr { get; set; } = "Password Requirements :\n* Use at least 8 characters \n* Use upper and lower case characters \n* Use 1 or more numbers \n* Recommend use special characters";
        public bool IsEditing { get; set; } = false;
        private int SecurityCode { get; set; }


        public ProfilePage Page { get; set; }
        public string UrlTempPP { get; set; } = string.Empty;
        bool dialogResult = false;
        OpenFileDialog ofd;

        public User User
        {
            get { return user; }
            set { user = value; }
        }

        #endregion

        #region Methods

        public bool SendCode()
        {
            if (User.Email == App.DataFacade.User.Email)
                return true;

            SendMail();
            EnterSecurityCode enter = new();
            enter.Reset();
            enter.Code = SecurityCode;
            enter.ShowDialog();
            if (enter.DialogResult != DialogResult.Success)
                return false;

            return true;
        }

        public void SendMail()
        {
            SecurityCode = new Random().Next(1000, 9999);
            EmailService.SendSecurityCode(User.Email, "Email OTP: ", SecurityCode.ToString(), "WolfTaxi");
        }

        public void Browse(object param)
        {
            ofd = new OpenFileDialog();
            ofd.Filter = "Image File (* png)| *.png";
            dialogResult = (bool)ofd.ShowDialog();
            CloudinaryService.DestroyImage("testuserpp", App.TempCloudinaryFolderPath);
            UrlTempPP = CloudinaryService.UploadImage(ofd.FileName, "testuserpp", App.TempCloudinaryFolderPath);
            Page.ProfilePhoto.ImageSource = null;
            Page.ProfilePhoto.ImageSource = BitmapService.GetBitmapImageFromUrl(UrlTempPP);
            SoundService.Succes();
        }

        public void ShowPassRun(object param)
        {
            EnterUserPassword enterAdmin = new();
            enterAdmin.ShowDialog();
            if (enterAdmin.Succes == false) return;
            ShowPassword(true);
        }

        public void SaveRun(object param)
        {
            if (!SendCode())
            {
                CMessageBox.Show($"{User.Email} Not Verified !", CMessageTitle.Error, CMessageButton.Ok, CMessageButton.None);
                Page.EmailIsTrue = false;
                return;
            }

            Page.EmailIsTrue = true;

            if (dialogResult)
                CloudinaryService.DestroyImage("tempdriverpp", App.TempCloudinaryFolderPath);
            User.SourceOfPP = dialogResult ? CloudinaryService.UploadImage(ofd.FileName, User.Username, App.UserCloudinaryFolderPath) : App.DriverProfilePhoto;
            App.DataFacade.User = this.User;
            SoundService.Succes();
        }

        public void ShowPassword(bool show)
        {
            Page.ShowPass.Visibility = show ? Visibility.Collapsed : Visibility.Visible;
            Page.PasswordGRID.Visibility = show ? Visibility.Visible : Visibility.Collapsed;
        }

        #endregion

        #region Commands

        public RelayCommand BrowsePPSource { get; set; }

        public RelayCommand EditCommand { get; set; }

        public RelayCommand ShowPass { get; set; }

        public RelayCommand EditEmail { get; set; }

        public RelayCommand Save { get; set; }


        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public ProfilePageVM()
        {
            BrowsePPSource = new(Browse);
            ShowPass = new(ShowPassRun);
            Save = new(SaveRun);
        }

    }
}
