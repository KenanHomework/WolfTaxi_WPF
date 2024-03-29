﻿using System;
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
using System.Text.Json;
using WolfTaxi_WPF.MVVM.Models.BaseClasses;

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
        public static string UserProfilePhoto = "https://res.cloudinary.com/kysbv/image/upload/v1663668239/WolfTaxi/user_icon.png";
        public static string SuccesSoundEffect = "https://res.cloudinary.com/kysbv/video/upload/v1661935108/WolfTaxi/success-sound-effect.mp3";
        public static string ErrorSoundEffect = "https://res.cloudinary.com/kysbv/video/upload/v1661936264/WolfTaxi/error-sound.mp3";
        public static string NotificationSoundEffect = "https://res.cloudinary.com/kysbv/video/upload/v1661940169/WolfTaxi/notification-sound.mp3";
        public static string ImgContact = "https://res.cloudinary.com/kysbv/image/upload/v1662239454/WolfTaxi/img_contact.png";
        public static string ImgHistory = "https://res.cloudinary.com/kysbv/image/upload/v1662239454/WolfTaxi/img_history.png";
        public static string ImgMap = "https://res.cloudinary.com/kysbv/image/upload/v1662239454/WolfTaxi/img_map.png";
        public static string ImgInfo = "https://res.cloudinary.com/kysbv/image/upload/v1662239917/WolfTaxi/img_info.png";
        public static string ImgLogout = "https://res.cloudinary.com/kysbv/image/upload/v1662240039/WolfTaxi/img_logout.png";
        public static string ImgAdjusment = "https://res.cloudinary.com/kysbv/image/upload/v1662242706/WolfTaxi/img_adjustment.png";
        public static string TgBtnMouseOver = "https://res.cloudinary.com/kysbv/image/upload/v1662239454/WolfTaxi/tgBtn_MouseOver.png";
        public static string TgBtnDefault = "https://res.cloudinary.com/kysbv/image/upload/v1662239448/WolfTaxi/tgBtn_default.png";
        public static string DriverCloudinaryFolderPath = "WolfTaxi/ClientPhotos/DriverPhotos";
        public static string UserCloudinaryFolderPath = "WolfTaxi/ClientPhotos/UserPhotos";
        public static string TempCloudinaryFolderPath = "WolfTaxi/ClientPhotos/TempPhotos";
        public static List<TaxiTypeBase> AllTaxiType;

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
            DataFacade.Load();
            ReadAllTaxiType();
            using (var jsonDoc = JsonDocument.Parse(File.ReadAllText("appconfig.json")))
            {
                JsonElement key = jsonDoc.RootElement.GetProperty("EsriAPIKey");
                Esri.ArcGISRuntime.ArcGISRuntimeEnvironment.ApiKey = key.ToString();
            }

            // Test -> testUser123



            DataFacade.Drivers = new List<Driver>() {
                new Driver("Kamil Kamilli",   "kamilliKamil123", "7FO01S3", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw m8 gran coupe", 2022, "77-ZZ-777",TaxiTypes.Fast, 1.4f)),
            };

            #region Drivers

            //DataFacade.Drivers = new List<Driver>() {
            //    new Driver("Kamil Kamilli",   "kamilliKamil123", "7FO01S3", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw m8 gran coupe", 2022, "77-ZZ-777", TaxiTypes.Fast, 1.4f)),
            //    new Driver("Driver 1.Driver", "kamilliKamil123", "IPJ801S", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
            //    new Driver("Driver 2.Driver", "kamilliKamil123", "A123456", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
            //    new Driver("Driver 3.Driver", "kamilliKamil123", "ASD245H", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
            //    new Driver("Driver 4.Driver", "kamilliKamil123", "AJNDSU2", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
            //    new Driver("Driver 4.Driver", "kamilliKamil123", "IUN891K", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
            //    new Driver("Driver 4.Driver", "kamilliKamil123", "A12AD2R", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
            //    new Driver("Driver 4.Driver", "kamilliKamil123", "B123456", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
            //    new Driver("Driver 4.Driver", "kamilliKamil123", "A12B656", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw x6m xDrive", 2022, "77-ZZ-777", TaxiTypes.Lux, 1.4f)) ,
            //    new Driver("Driver 5.Driver", "kamilliKamil123", "C123456", "kamil@kamilli.com", "0555555555", new(), new Taxi("bmw m235i xDrive ", 2022, "77-ZZ-777", TaxiTypes.Comfort, 1.4f))
            //};
            #endregion
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
            Container.RegisterSingleton<EnterWindowVM>();
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
            Container.RegisterSingleton<ProfilePageVM>();
            Container.RegisterSingleton<MapPageVM>();
            Container.RegisterSingleton<HistoryPageVM>();
            Container.RegisterSingleton<AskRouteVM>();

            Container.Verify();
        }

        void WriteAllTaxiType()
        {
            JSONService.Write("dataset/all_taxi.json", AllTaxiType);
        }

        void ReadAllTaxiType()
        {
            try
            {
                AllTaxiType = JSONService.Read<List<TaxiTypeBase>>("dataset/dataFacade.json");
            }
            catch (Exception) { AllTaxiType = new() { new ComfortTaxiType(), new FastTaxiType(), new LuxTaxiType() }; }
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
            if (AppWindow != null)
            {
                AppWindow.Reset();
                AppWindow.Close();
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
            Container.GetInstance<ProfilePageVM>().User = new User(DataFacade.User);
            Container.GetInstance<MapPageVM>().User = DataFacade.User;
            Container.GetInstance<HistoryPageVM>().User = DataFacade.User;
            AppWindow.ShowDialog();
        }


        #endregion
    }
}
