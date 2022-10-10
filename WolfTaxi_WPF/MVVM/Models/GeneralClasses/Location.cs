using Esri.ArcGISRuntime.Geometry;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WolfTaxi_WPF.MVVM.Models.GeneralClasses
{
    public class Location
    {
        #region Members

        public MapPoint Point { get; set; } = null;

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion

        public Location(double x, double y) => Point = new(x,y, SpatialReferences.Wgs84);

        public Location()
        {
            
        }
    }
}
