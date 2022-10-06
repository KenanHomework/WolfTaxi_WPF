using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using FindARouteAndDirections;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WolfTaxi_WPF.MVVM.Views.Pages
{
    /// <summary>
    /// Interaction logic for MapPage.xaml
    /// </summary>
    public partial class MapPage : Page
    {
        public MapPage()
        {
            InitializeComponent();

            MapPoint mapCenterPoint = new MapPoint(-118.24532, 34.05398, SpatialReferences.Wgs84);
            MainMapView.SetViewpoint(new Viewpoint(mapCenterPoint, 144_447.638572));

            DataContext = new MapViewModel();
        }
    }
}
