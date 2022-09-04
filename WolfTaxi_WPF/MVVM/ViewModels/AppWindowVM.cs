using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WolfTaxi_WPF.Commands;
using WolfTaxi_WPF.Interfaces;
using WolfTaxi_WPF.MVVM.Models.DerivedClasses;
using WolfTaxi_WPF.MVVM.Views;
using WolfTaxi_WPF.MVVM.Views.Pages;
using WolfTaxi_WPF.Services;

namespace WolfTaxi_WPF.MVVM.ViewModels
{
    public class AppWindowVM : IResetable
    {
        #region Members

        public User User { get; set; }

        public AppWindow Window { get; set; }

        #endregion

        #region Commands

        //public RelayCommand Logout { get; set; }

        //public RelayCommand About { get; set; }


        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        #region Methods

        public void InfoGithubClick()
        {
            WebService.OpenWebsiteWithUrl("https://github.com/KenanHomework/WolfTaxi_WPF.git");
        }

        public void AboutClick() => Window.Frame.Navigate(new AboutPage());

        public void AdjustmentClick()=> Window.Frame.Navigate(new AdjustmentPage());

        public void LogoutClick()
        {
            App.DataFacade.Exit();
            Reset();
            SoundService.Notification();
            App.ToEnterWindow();
        }

        public void Reset()
        {

        }

        #endregion

        public AppWindowVM()
        {

        }
    }
}
