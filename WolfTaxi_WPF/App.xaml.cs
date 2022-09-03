using System;
using System.Collections.Generic;
using SimpleInjector;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WolfTaxi_WPF.MVVM.ViewModels;
using WolfTaxi_WPF.MVVM.Views;
using WolfTaxi_WPF.Facade;
using WolfTaxi_WPF.MVVM.Models.DerivedClasses;
using WolfTaxi_WPF.MVVM.Models.GeneralClasses;
using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.Services;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CloudinaryDotNet;
using System.Windows.Media;

namespace WolfTaxi_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region Members
        private static DataFacade dataFacade = new();

        public static DataFacade DataFacade { get => dataFacade; set { dataFacade = value; } }
        public static SimpleInjector.Container Container = new();
        public static EnterWindow EnterWindow;
        public static Cloudinary cloudinary = new(new Account("kysbv", "338497835255375", "iz_nsuDxVjxd-zU2xeDncLQDt64"));
        public static AppWindow AppWindow;
        public static AdminPanel AdminPanel;
        public static Comparer.Comparer Comparer = new Comparer.Comparer();
        public static string AdminSubFilePath = "dataset";
        public static string UserSubFilePath = "dataset/Users";
        public static string DriverSubFilePath = "dataset/Drivers";
        public static string FastTaxiIconSource = "https://res.cloudinary.com/kysbv/image/upload/v1658306801/WolfTaxi/taxi_type_fast.png";
        public static string ComfortTaxiIconSource = "https://res.cloudinary.com/kysbv/image/upload/v1658306801/WolfTaxi/taxi_type_comfort.png";
        public static string LuxTaxiIconSource = "https://res.cloudinary.com/kysbv/image/upload/v1658306801/WolfTaxi/taxi_type_lux.png";
        public static string WolfLogoSource = "https://res.cloudinary.com/kysbv/image/upload/v1658898883/WolfTaxi/wolf_logo.png";
        public static string DriverProfilePhoto = "https://res.cloudinary.com/kysbv/image/upload/v1658570801/WolfTaxi/driver_pp.png";
        public static string SuccesSoundEffect = "https://res.cloudinary.com/kysbv/video/upload/v1661935108/WolfTaxi/success-sound-effect.mp3";
        public static string ErrorSoundEffect = "https://res.cloudinary.com/kysbv/video/upload/v1661936264/WolfTaxi/error-sound.mp3";
        public static string NotificationSoundEffect = "https://res.cloudinary.com/kysbv/video/upload/v1661940169/WolfTaxi/notification-sound.mp3";
        public static string DriverCloudinaryFolderPath = "WolfTaxi/ClientPhotos/DriverPhotos";
        public static string UserCloudinaryFolderPath = "WolfTaxi/ClientPhotos/UserPhotos";
        public static string TempCloudinaryFolderPath = "WolfTaxi/ClientPhotos/TempPhotos";



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

            DataFacade.Load();
            DataFacade.Remember = false;
            //Container.GetInstance<EditDriverVM>().Driver = DataFacade.Drivers[0];

            DataFacade.Drivers = new List<Driver>() {
                new Driver("Kamil Kamilli", "kamilliKamil123", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw m8 gran coupe competition", 2022, "77-ZZ-777", TaxiTypes.Fast, 1.4f)),
                new Driver("Driver 1.Driver", "kamilliKamil123", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
                new Driver("Driver 2.Driver", "kamilliKamil123", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
                new Driver("Driver 3.Driver", "kamilliKamil123", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
                new Driver("Driver 4.Driver", "kamilliKamil123", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
                new Driver("Driver 4.Driver", "kamilliKamil123", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
                new Driver("Driver 4.Driver", "kamilliKamil123", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
                new Driver("Driver 4.Driver", "kamilliKamil123", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
                new Driver("Driver 4.Driver", "kamilliKamil123", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
                new Driver("Driver 5.Driver", "kamilliKamil123", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw m235i xDrive ", 2022, "77-ZZ-777", TaxiTypes.Comfort, 1.4f))
            };
            //DataFacade.Save();

            //User user = new("test2", "testingWolfTAXI1", "kenanysbv@gmail.com", "055");
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
            Container.RegisterSingleton<WelcomePageVM>();
            Container.RegisterSingleton<AppWindowVM>();
            Container.RegisterSingleton<AdminPanelVM>();
            Container.RegisterSingleton<AddDriverVM>();
            Container.RegisterSingleton<EditDriverVM>();
            Container.RegisterSingleton<EditTextblockVM>();
            Container.RegisterSingleton<LoginPageVM>();

            Container.Verify();
        }

        public static void ToAdminPanel()
        {
            if (EnterWindow != null)
            {
                EnterWindow.Reset();
                EnterWindow.Close();
            }
            AdminPanel = new();
            AdminPanel.Reset();
            AdminPanel.ShowDialog();
        }

        public static void ToEnterWindow()
        {
            if (AdminPanel != null)
            {
                AdminPanel.Reset();
                AdminPanel.Close();
            }

            EnterWindow = new();
            EnterWindow.Reset();
            EnterWindow.ShowDialog();
        }

        public static void ToAppWindow()
        {
            if (EnterWindow != null)
            {
                EnterWindow.Reset();
                EnterWindow.Close();
            }
            if (AdminPanel != null)
            {
                AdminPanel.Reset();
                AdminPanel.Close();
            }
            AppWindow = new();
            AppWindow.ShowDialog();
        }
        #endregion
    }
}
