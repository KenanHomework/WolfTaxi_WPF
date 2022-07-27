using EyeTaxi_WPF.Enums;
using EyeTaxi_WPF.Facade;
using EyeTaxi_WPF.Interfaces;
using EyeTaxi_WPF.MVVM.Models.DerivedClasses;
using EyeTaxi_WPF.MVVM.ViewModels;
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

namespace EyeTaxi_WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for EnterWindow.xaml
    /// </summary>
    public partial class EnterWindow : Window, IResetable
    {

        SignUp sign = new();

        AdminLogin adminLogin = new();

        LoginPage login = new();

        public EnterWindow()
        {
            InitializeComponent();

            App.AppWindow = new();
            App.AdminPanel = new();

            #region Implement Commands

            App.Container.GetInstance<LoginPageVM>().LoginClick = new(p =>
            {
                ProcessResult res = App.DataFacade.Login
                                                (
                                                    App.Container.GetInstance<LoginPageVM>().Username,
                                                    App.Container.GetInstance<LoginPageVM>().Password
                                                );


                if (res == ProcessResult.Success)
                    App.ToAppWindow();
                else
                    new MessageBoxCustom(res.ToString(), MessageType.Warning, MessageButtons.Ok).ShowDialog();
            });

            App.Container.GetInstance<LoginPageVM>().SignUpClick = new(p => { Frame.Navigate(sign); });

            App.Container.GetInstance<LoginPageVM>().AdminClick = new(p => { Frame.Navigate(adminLogin); });

            App.Container.GetInstance<SignUpVM>().SignIn = new(p => { Frame.Navigate(login); });

            App.Container.GetInstance<AdminLoginVM>().UserClick = new(p => { Frame.Navigate(login); });

            App.Container.GetInstance<WelcomePageVM>().SignUp = new(p => { Frame.Navigate(sign); });

            App.Container.GetInstance<WelcomePageVM>().SignIn = new(p => { Frame.Navigate(login); });

            #endregion

            App.EnterWindow = this;

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

        public void Reset()
        {
            sign.Reset();
            adminLogin.Reset();
            login.Reset();
        }

    }
}
