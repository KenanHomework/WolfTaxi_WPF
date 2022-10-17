using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WolfTaxi_WPF.Enums;
using WolfTaxi_WPF.MVVM.Views;

namespace WolfTaxi_WPF.MVVM.ViewModels
{
    public class AskRouteVM
    {
        #region Members

        public AskRoute Window { get; set; }

        public bool IsRouteAccepted { get; set; } = false;

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion


        #region Methods

        public void InitializeMap()
        {
            //Window.AKMap.Map = new Map(BasemapStyle.ArcGISNova);
            //Window.AKMap.LocationDisplay.IsEnabled = true;
            //Window.AKMap.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.CompassNavigation;
        }


        #endregion
    }
}
