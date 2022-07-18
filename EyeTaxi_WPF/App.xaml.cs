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

namespace EyeTaxi_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static Container Container = new();
        public static DataFacade DataFacade = new();
        public static string AdminSubFilePath = "dataset";
        public static string UserSubFilePath = "dataset/Users";
        public static string DriverSubFilePath = "dataset/Drivers";

        public App()
        {
            User user = new("test2", "testingEYETAXI1", "kenanysbv@gmail.com", "055");
            UserService.Write(user);
            // kenanShekili2

            DataFacade.Load();

            CreateDirectorys();
            Register();
        }

        void CreateDirectorys()
        {
            Directory.CreateDirectory(AdminSubFilePath);
            Directory.CreateDirectory(DriverSubFilePath);
            Directory.CreateDirectory(UserSubFilePath);
        }


        void Register()
        {
            Container.RegisterSingleton<LoginPageVM>();
            Container.RegisterSingleton<EnterSecurityVM>();
            Container.RegisterSingleton<SignUpVM>();
            Container.RegisterSingleton<ForgotPasswordVM>();
            Container.RegisterSingleton<DataFacade>();

            Container.Verify();
        }

    }
}
