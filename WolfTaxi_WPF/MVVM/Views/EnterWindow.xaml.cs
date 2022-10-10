using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.Facade;
using WolfTaxi_WPF.Interfaces;
using WolfTaxi_WPF.MVVM.Models.DerivedClasses;
using WolfTaxi_WPF.MVVM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WolfTaxi_WPF.Services;

namespace WolfTaxi_WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for EnterWindow.xaml
    /// </summary>
    public partial class EnterWindow : Window, IResetable
    {



        public EnterWindow()
        {
            InitializeComponent();

            App.AppWindow = new();
            App.AdminPanel = new();

            if (App.DataFacade.Remember && App.DataFacade.User != null)
            {
                if (App.DataFacade.Login(App.DataFacade.User) == ProcessResult.Success)
                {
                    SoundService.Succes();
                    App.ToAppWindow();
                    return;
                }
            }
            App.EnterWindow = this;

            DataContext = App.Container.GetInstance<EnterWindowVM>();
            App.Container.GetInstance<EnterWindowVM>().Window = this;
            App.Container.GetInstance<EnterWindowVM>().sign = new();
            App.Container.GetInstance<EnterWindowVM>().adminLogin = new();
            App.Container.GetInstance<EnterWindowVM>().login = new();

            Frame.Navigate(new WelcomePage());
        }

        private void ResizeButton_Click(object sender, RoutedEventArgs e)
        {


            if (sender is Button btn)
            {
                switch (btn.Content.ToString())
                {
                    case "_":
                        this.WindowState = WindowState.Minimized;
                        break;
                    case "X":
                        App.DataFacade.Save();
                        Application.Current.Shutdown();
                        break;
                    default:
                        break;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        public void Reset() => App.Container.GetInstance<EnterWindowVM>().Reset();

    }
}
