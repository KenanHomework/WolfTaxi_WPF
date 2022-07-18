using EyeTaxi_WPF.Enums;
using EyeTaxi_WPF.Facade;
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
    public partial class EnterWindow : Window
    {
        public EnterWindow()
        {
            InitializeComponent();

            #region Implement Commands

            App.Container.GetInstance<LoginPageVM>().LoginClick = new(p =>
            {
                ProcessResult res = App.DataFacade.Login
                                                (
                                                    App.Container.GetInstance<LoginPageVM>().Username,
                                                    App.Container.GetInstance<LoginPageVM>().Password
                                                );


                MessageBox.Show(res.ToString());
            });


            App.Container.GetInstance<LoginPageVM>().SignUpClick = new(p => { SignUp sign = new(); sign.Reset(); Frame.Navigate( sign); });

            App.Container.GetInstance<SignUpVM>().SignIn = new(p => { LoginPage login = new(); login.Reset(); Frame.Navigate(login); });

            #endregion

            Frame.Navigate(new LoginPage());
        }
        private void ResizeButton_Click(object sender, RoutedEventArgs e)
        {


            if (sender is Button btn)
            {
                switch (btn.Content.ToString())
                {
                    case "_":
                        Application.Current.MainWindow.WindowState = WindowState.Minimized;
                        break;
                    case "❒":
                        if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                            Application.Current.MainWindow.WindowState = WindowState.Normal;
                        else
                            Application.Current.MainWindow.WindowState = WindowState.Maximized;
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
    }
}
