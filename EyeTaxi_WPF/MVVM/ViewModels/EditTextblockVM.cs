using WolfTaxi_WPF.MVVM.Models.DerivedClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WolfTaxi_WPF.MVVM.ViewModels
{
    public class EditTextblockVM
    {

        #region Members

        private Binding text;

        public Binding Text
        {
            get { return text; }
            set { text = value; OnPropertyChanged(); }
        }

        private string hit;

        public string Hit
        {
            get { return hit; }
            set { hit = value; OnPropertyChanged(); }
        }

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

    }
}
