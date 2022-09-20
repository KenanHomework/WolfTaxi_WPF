using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.MVVM.Views;
using WolfTaxi_WPF.Services;

namespace WolfTaxi_WPF.MVVM.ViewModels
{
    public class EnterWindowVM
    {

        #region Members

         public SignUp sign ;

         public AdminLogin adminLogin ;

        public LoginPage login ;

        public EnterWindow Window { get; set; }

        #endregion


        public EnterWindowVM()
        {
            #region Implement Commands

            App.Container.GetInstance<LoginPageVM>().LoginClick = new(p =>
            {
                ProcessResult res = App.DataFacade.Login
                                                (
                                                    App.Container.GetInstance<LoginPageVM>().Username,
                                                    App.Container.GetInstance<LoginPageVM>().Password
                                                );


                if (res == ProcessResult.Success)
                {
                    App.DataFacade.Remember = App.Container.GetInstance<LoginPageVM>().Remember;
                    SoundService.Succes();
                    App.ToAppWindow();
                }
                else
                    CMessageBox.Show(res.ToString(), CMessageTitle.Warning, CMessageButton.Ok, CMessageButton.None);

            });

            App.Container.GetInstance<LoginPageVM>().SignUpClick = new(p => { Window.Frame.Navigate(sign); });

            App.Container.GetInstance<LoginPageVM>().AdminClick = new(p => { Window.Frame.Navigate(adminLogin); });

            App.Container.GetInstance<SignUpVM>().SignIn = new(p => { Window.Frame.Navigate(login); });

            App.Container.GetInstance<AdminLoginVM>().UserClick = new(p => { Window.Frame.Navigate(login); });

            App.Container.GetInstance<WelcomePageVM>().SignUp = new(p => { Window.Frame.Navigate(sign); });

            App.Container.GetInstance<WelcomePageVM>().SignIn = new(p => { Window.Frame.Navigate(login); });

            App.Container.GetInstance<WelcomePageVM>().Admin = new(p => { Window.Frame.Navigate(adminLogin); });

            #endregion

        }

        public void Reset()
        {
            sign.Reset();
            adminLogin.Reset();
            login.Reset();
        }

    }
}
