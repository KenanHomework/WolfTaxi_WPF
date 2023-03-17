using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Navigation;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Tasks.NetworkAnalysis;
using Esri.ArcGISRuntime.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
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
using WolfTaxi_WPF.MVVM.ViewModels;
using Color = System.Drawing.Color;

namespace WolfTaxi_WPF.MVVM.Views
{
    /// <summary>
    /// Interaction logic for AskRoute.xaml
    /// </summary>
    public partial class AskRoute : Window
    {

        public RouteTracker _tracker;
        public RouteResult _routeResult;
        public Route _route;

        // List of driving directions for the route.
        public IReadOnlyList<DirectionManeuver> _directionsList;

        // Graphics to show progress along the route.
        public Graphic _routeAheadGraphic;
        public Graphic _routeTraveledGraphic;

        public MapPoint _start = null;

        public MapPoint _destination = null;

        // Feature service for routing in San Diego.
        public readonly Uri _routingUri = new Uri("https://route-api.arcgis.com/arcgis/rest/services/World/Route/NAServer/Route_World");

        public readonly Uri _serviceUri = new Uri("https://geocode-api.arcgis.com/arcgis/rest/services/World/GeocodeServer");

        public AskRoute()
        {
            InitializeComponent();
            InitializeMap();
            Initialize();
            TaxiTypeListView.ItemsSource = App.AllTaxiType;
        }

        private async Task Initialize()
        {
            try
            {
                AKMap.GraphicsOverlays.Clear();



                // Create the route task, using the online routing service.
                RouteTask routeTask = await RouteTask.CreateAsync(_routingUri);

                // Get the default route parameters.
                RouteParameters routeParams = await routeTask.CreateDefaultParametersAsync();

                // Explicitly set values for parameters.
                routeParams.ReturnDirections = true;
                routeParams.ReturnStops = true;
                routeParams.ReturnRoutes = true;
                routeParams.OutputSpatialReference = SpatialReferences.Wgs84;

                // Create stops for each location.
                Stop stop1 = new Stop(_start) { Name = "Start Point" };
                Stop stop2 = new Stop(_destination) { Name = "Destination" };

                // Assign the stops to the route parameters.
                List<Stop> stopPoints = new List<Stop> { stop1, stop2 };
                routeParams.SetStops(stopPoints);

                // Get the route results.
                _routeResult = await routeTask.SolveRouteAsync(routeParams);
                _route = _routeResult.Routes[0];

                // Add a graphics overlay for the route graphics.
                AKMap.GraphicsOverlays.Add(new GraphicsOverlay());

                // Add graphics for the stops.
                var startSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Blue, 2);
                Graphic _startGraphic = new Graphic(_start, new SimpleMarkerSymbol
                {
                    Style = SimpleMarkerSymbolStyle.Circle,
                    Color = System.Drawing.Color.AliceBlue,
                    Size = 8,
                    Outline = startSymbol
                }
                );

                var endOutlineSymbol = new SimpleLineSymbol(style: SimpleLineSymbolStyle.Solid, color: System.Drawing.Color.Red, width: 2);
                Graphic _endGraphic = new Graphic(_destination, new SimpleMarkerSymbol
                {
                    Style = SimpleMarkerSymbolStyle.Square,
                    AngleAlignment = SymbolAngleAlignment.Map,
                    Color = System.Drawing.Color.Green,
                    Size = 8,
                    Outline = endOutlineSymbol
                }
                );



                SimpleMarkerSymbol stopSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Diamond, Color.OrangeRed, 20);
                AKMap.GraphicsOverlays[0].Graphics.Add(_startGraphic);
                AKMap.GraphicsOverlays[0].Graphics.Add(_endGraphic);

                // Create a graphic (with a dashed line symbol) to represent the route.
                _routeAheadGraphic = new Graphic(_route.RouteGeometry) { Symbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Dash, Color.BlueViolet, 5) };

                // Create a graphic (solid) to represent the route that's been traveled (initially empty).
                _routeTraveledGraphic = new Graphic { Symbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, Color.LightBlue, 3) };

                // Add the route graphics to the map view.
                AKMap.GraphicsOverlays[0].Graphics.Add(_routeAheadGraphic);
                AKMap.GraphicsOverlays[0].Graphics.Add(_routeTraveledGraphic);

                // Set the map viewpoint to show the entire route.
                await AKMap.SetViewpointGeometryAsync(_route.RouteGeometry, 100);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }


        public void InitializeMap()
        {
            AKMap.Map = new Map(BasemapStyle.ArcGISNova);
            AKMap.LocationDisplay.IsEnabled = true;
            AKMap.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.CompassNavigation;
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
                        Close();
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
