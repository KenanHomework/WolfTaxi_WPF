using WolfTaxi_WPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WolfTaxi_WPF.MVVM.ViewModels
{
    public class WelcomePageVM
    {
        #region Commands

        public RelayCommand SignUp { get; set; }

        public RelayCommand SignIn { get; set; }

        public RelayCommand Admin { get; set; }

        #endregion
    }
}
