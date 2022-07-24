using System;
using System.Collections.Generic;
using SimpleInjector;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using EyeTaxi_WPF.MVVM.ViewModels;
using EyeTaxi_WPF.MVVM.Views;
using EyeTaxi_WPF.Facade;
using EyeTaxi_WPF.MVVM.Models.DerivedClasses;
using EyeTaxi_WPF.MVVM.Models.GeneralClasses;
using EyeTaxi_WPF.Enums;
using EyeTaxi_WPF.Services;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EyeTaxi_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region Members

        public static SimpleInjector.Container Container = new();
        public static DataFacade DataFacade = new();
        public static EnterWindow EnterWindow;
        public static AdminPanel AdminPanel;
        public static string AdminSubFilePath = "dataset";
        public static string UserSubFilePath = "dataset/Users";
        public static string DriverSubFilePath = "dataset/Drivers";

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion


        public App()
        {
            CreateDirectorys();
            Register();
            //JSONService.Write($"{DriverSubFilePath}/drivers.json",);
            //AdminPanel = new();
            DataFacade.Load();
            Container.GetInstance<EditDriverVM>().Driver = DataFacade.Drivers[0];
            //DataFacade.Drivers = new List<Driver>() { new Driver("Kamil Kamilli", "kamilliKamil123", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw m8 gran coupe competition", 2022, "77-ZZ-777", ConsoleColor.Black)) };
            //DataFacade.Save();


            //User user = new("test2", "testingEYETAXI1", "kenanysbv@gmail.com", "055");
            //UserService.Write(user);
            // kenanShekili2


            //DataFacade.Save();
        }

        #region Methods

        void CreateDirectorys()
        {
            Directory.CreateDirectory(AdminSubFilePath);
            Directory.CreateDirectory(DriverSubFilePath);
            Directory.CreateDirectory(UserSubFilePath);
        }

        void Register()
        {
            Container.RegisterSingleton<EnterSecurityVM>();
            Container.RegisterSingleton<SignUpVM>();
            Container.RegisterSingleton<ForgotPasswordVM>();
            Container.RegisterSingleton<DataFacade>();
            Container.RegisterSingleton<AdminLoginVM>();
            Container.RegisterSingleton<AdminPanelVM>();
            Container.RegisterSingleton<EditDriverVM>();
            Container.RegisterSingleton<LoginPageVM>();

            Container.Verify();
        }

        public static void ToAdminPanel()
        {
            EnterWindow.Reset();
            AdminPanel.Reset();
            EnterWindow.Close();
            AdminPanel.ShowDialog();
        }

        public static void ToEnterWindow()
        {
            EnterWindow.Reset();
            AdminPanel.Reset();
            AdminPanel.Close();
            EnterWindow.ShowDialog();
        }

        #endregion
    }
}
