using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Location;
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
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WolfTaxi_WPF.MVVM.Models.GeneralClasses;
using Location = Esri.ArcGISRuntime.Location.Location;
using WolfTaxi_WPF.Commands;
using MaterialDesignThemes.Wpf;
using WolfTaxi_WPF.Enums;
using Esri.ArcGISRuntime.UI.Controls;
using System.Xml.Serialization;

namespace WolfTaxi_WPF.MVVM.Views.Pages
{
    /// <summary>
    /// Interaction logic for MapPage.xaml
    /// </summary>
    public partial class MapPage : Page
    {
        // Variables for tracking the navigation route.
        private RouteTracker _tracker;
        private RouteResult _routeResult;
        private Route _route;

        // List of driving directions for the route.
        private IReadOnlyList<DirectionManeuver> _directionsList;

        // Graphics to show progress along the route.
        private Graphic _routeAheadGraphic;
        private Graphic _routeTraveledGraphic;

        private readonly MapPoint _start = new MapPoint(-117.160386727, 32.706608, SpatialReferences.Wgs84);

        private readonly MapPoint _destination = new MapPoint(-117.147230, 32.730467, SpatialReferences.Wgs84);

        // Feature service for routing in San Diego.
        private readonly Uri _routingUri = new Uri("https://sampleserver6.arcgisonline.com/arcgis/rest/services/NetworkAnalysis/SanDiego/NAServer/Route");

        public string CaseInfo { get; set; } = string.Empty;

        public RelayCommand StartPreprocess { get; set; }

        public MapPage()
        {
            InitializeComponent();
            InitializeUIElements();
            InitializeMap();
            // Create the map view.
            DataContext = this;
            StartPreprocess = new(StartPreprocessRun, StartPreprocessCanRun);
        }

        public void StartPreprocessRun(object param)
        {
            CMessageBox.Show("Starting Preprocessing.... ", CMessageTitle.Info, CMessageButton.Ok, CMessageButton.None);
        }

        public bool StartPreprocessCanRun(object param)
        {
            return true;
        }

        public void InitializeUIElements()
        {
            StartAddress.Items.Add(new AddressComboBoxItemInfo("My Location", "CrosshairsGps"));
            StartAddress.Items.Add(new AddressComboBoxItemInfo("Select From Map", "MapMarkerRadius"));
            DestinationAddress.Items.Add(new AddressComboBoxItemInfo("Select From Map", "MapMarkerRadius"));
            StartAddress.Items.Add(new AddressComboBoxItemInfo("Address __ 1"));
            StartAddress.Items.Add(new AddressComboBoxItemInfo("Address __ 2"));
            DestinationAddress.Items.Add(new AddressComboBoxItemInfo("Address __ 3"));
            DestinationAddress.Items.Add(new AddressComboBoxItemInfo("Address __ 4"));
            DestinationAddress.Items.Add(new AddressComboBoxItemInfo("Address __ 5"));

        }

        public void StartInitialize() => _ = Initialize();

        public void InitializeMap()
        {
            WTMap.Map = new Map(BasemapStyle.ArcGISNova);
            WTMap.LocationDisplay.IsEnabled = true;
            WTMap.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.CompassNavigation;
        }

        private async Task Initialize()
        {
            try
            {
                // Add event handler for when this sample is unloaded.
                Unloaded += SampleUnloaded;

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
                WTMap.GraphicsOverlays.Add(new GraphicsOverlay());

                // Add graphics for the stops.
                SimpleMarkerSymbol stopSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Diamond, Color.OrangeRed, 20);
                WTMap.GraphicsOverlays[0].Graphics.Add(new Graphic(_start, stopSymbol));
                WTMap.GraphicsOverlays[0].Graphics.Add(new Graphic(_destination, stopSymbol));

                // Create a graphic (with a dashed line symbol) to represent the route.
                _routeAheadGraphic = new Graphic(_route.RouteGeometry) { Symbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Dash, Color.BlueViolet, 5) };

                // Create a graphic (solid) to represent the route that's been traveled (initially empty).
                _routeTraveledGraphic = new Graphic { Symbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, Color.LightBlue, 3) };

                // Add the route graphics to the map view.
                WTMap.GraphicsOverlays[0].Graphics.Add(_routeAheadGraphic);
                WTMap.GraphicsOverlays[0].Graphics.Add(_routeTraveledGraphic);

                // Set the map viewpoint to show the entire route.
                await WTMap.SetViewpointGeometryAsync(_route.RouteGeometry, 100);

                // Enable the navigation button.
                StartNavigationButton.IsEnabled = true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
        }

        private void StartNavigation(object sender, RoutedEventArgs e)
        {
            // Disable the start navigation button.
            StartNavigationButton.IsEnabled = false;

            // Get the directions for the route.
            _directionsList = _route.DirectionManeuvers;

            // Create a route tracker.
            _tracker = new RouteTracker(_routeResult, 0, true);

            // Handle route tracking status changes.
            _tracker.TrackingStatusChanged += TrackingStatusUpdated;

            // Turn on navigation mode for the map view.
            WTMap.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.Navigation;
            WTMap.LocationDisplay.AutoPanModeChanged += AutoPanModeChanged;

            // Add a data source for the location display.
            var simulationParameters = new SimulationParameters(DateTimeOffset.Now, 40.0);
            var simulatedDataSource = new SimulatedLocationDataSource();
            simulatedDataSource.SetLocationsWithPolyline(_route.RouteGeometry, simulationParameters);
            WTMap.LocationDisplay.DataSource = new RouteTrackerDisplayLocationDataSource(simulatedDataSource, _tracker);

            // Use this instead if you want real location:
            // WTMap.LocationDisplay.DataSource = new RouteTrackerLocationDataSource(new SystemLocationDataSource(), _tracker);

            // Enable the location display (this wil start the location data source).
            WTMap.LocationDisplay.IsEnabled = true;
        }

        private void TrackingStatusUpdated(object sender, RouteTrackerTrackingStatusChangedEventArgs e)
        {
            TrackingStatus status = e.TrackingStatus;

            // Start building a status message for the UI.
            System.Text.StringBuilder statusMessageBuilder = new System.Text.StringBuilder("Route Status:\n");

            // Check the destination status.
            if (status.DestinationStatus == DestinationStatus.NotReached || status.DestinationStatus == DestinationStatus.Approaching)
            {
                statusMessageBuilder.AppendLine("Distance remaining: " +
                                            status.RouteProgress.RemainingDistance.DisplayText + " " +
                                            status.RouteProgress.RemainingDistance.DisplayTextUnits.PluralDisplayName);

                statusMessageBuilder.AppendLine("Time remaining: " +
                                                status.RouteProgress.RemainingTime.ToString(@"hh\:mm\:ss"));

                if (status.CurrentManeuverIndex + 1 < _directionsList.Count)
                {
                    statusMessageBuilder.AppendLine("Next direction: " + _directionsList[status.CurrentManeuverIndex + 1].DirectionText);
                }

                // Set geometries for progress and the remaining route.
                _routeAheadGraphic.Geometry = status.RouteProgress.RemainingGeometry;
                _routeTraveledGraphic.Geometry = status.RouteProgress.TraversedGeometry;
            }
            else if (status.DestinationStatus == DestinationStatus.Reached)
            {
                statusMessageBuilder.AppendLine("Destination reached.");

                // Set the route geometries to reflect the completed route.
                _routeAheadGraphic.Geometry = null;
                _routeTraveledGraphic.Geometry = status.RouteResult.Routes[0].RouteGeometry;

                // Navigate to the next stop (if there are stops remaining).
                if (status.RemainingDestinationCount > 1)
                {
                    _tracker.SwitchToNextDestinationAsync();
                }
                else
                {
                    Dispatcher.BeginInvoke((Action)delegate ()
                    {
                        // Stop the simulated location data source.
                        WTMap.LocationDisplay.DataSource.StopAsync();
                    });
                }
            }

            Dispatcher.BeginInvoke((Action)delegate ()
            {
                // Show the status information in the UI.
                //MessagesTextBlock.Text = statusMessageBuilder.ToString();
            });
        }

        private void AutoPanModeChanged(object sender, LocationDisplayAutoPanMode e)
        {
            // Turn the recenter button on or off when the location display changes to or from navigation mode.
            RecenterButton.IsEnabled = e != LocationDisplayAutoPanMode.Navigation;
        }

        private void WTMap_GeoViewTapped(object sender, GeoViewInputEventArgs e)
        {
            // Get the user-tapped location
            MapPoint mapLocation = e.Location;

            // Project the user-tapped map point location to a geometry
            Geometry myGeometry = GeometryEngine.Project(mapLocation, SpatialReferences.Wgs84);

            // Convert to geometry to a traditional Lat/Long map point
            MapPoint projectedLocation = (MapPoint)myGeometry;

            // Format the display callout string based upon the projected map point (example: "Lat: 100.123, Long: 100.234")
            string mapLocationDescription = string.Format("Lat: {0:F3} Long:{1:F3}", projectedLocation.Y, projectedLocation.X);

            // Create a new callout definition using the formatted string
            CalloutDefinition myCalloutDefinition = new CalloutDefinition($"{CaseInfo} Location:", mapLocationDescription);

            // Display the callout
            WTMap.ShowCalloutAt(mapLocation, myCalloutDefinition);
        }


        private void RecenterButton_Click(object sender, RoutedEventArgs e)
        {
            // Change the mapview to use navigation mode.
            WTMap.LocationDisplay.AutoPanMode = LocationDisplayAutoPanMode.CompassNavigation;
        }

        private void SampleUnloaded(object sender, RoutedEventArgs e)
        {
            // Stop the tracker.
            if (_tracker != null)
            {
                _tracker.TrackingStatusChanged -= TrackingStatusUpdated;
                _tracker = null;
            }

            // Stop the location data source.
            WTMap.LocationDisplay?.DataSource?.StopAsync();
        }

        private void StartAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddressSelectorSelectionChanged(AddressRouteType.Start);
        }

        private void DestinationAddress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AddressSelectorSelectionChanged(AddressRouteType.Destination);
        }

        public void AddressSelectorSelectionChanged(AddressRouteType type)
        {
            void MapSelection(bool isTrue)
            {
                if (isTrue)
                {

                    try
                    {
                        WTMap.GeoViewTapped += WTMap_GeoViewTapped;
                    }
                    catch (Exception) { }

                    CaseInfo = type.ToString();
                }
                else
                {
                    try
                    {
                        WTMap.GeoViewTapped -= WTMap_GeoViewTapped;
                        WTMap.DismissCallout();
                    }
                    catch (Exception) { }

                }
            }

            int index = type == AddressRouteType.Start ? StartAddress.SelectedIndex : DestinationAddress.SelectedIndex;

            MapSelection(false);

            if (index == -1)
                return;

            switch (index)
            {
                case 0:
                    {
                        if (type == AddressRouteType.Start)
                            CMessageBox.Show(WTMap.LocationDisplay.Location.ToString(), CMessageTitle.Info, CMessageButton.Ok, CMessageButton.None);
                        else
                            MapSelection(true);
                        return;
                    }
                case 1:
                    {
                        if (type == AddressRouteType.Start)
                            MapSelection(true);
                        return;
                    }
                default:
                    break;
            }

            MapSelection(false);
        }
    }

    // This location data source uses an input data source and a route tracker.
    // The location source that it updates is based on the snapped-to-route location from the route tracker.
    public class RouteTrackerDisplayLocationDataSource : LocationDataSource
    {
        private LocationDataSource _inputDataSource;
        private RouteTracker _routeTracker;

        public RouteTrackerDisplayLocationDataSource(LocationDataSource dataSource, RouteTracker routeTracker)
        {
            // Set the data source
            _inputDataSource = dataSource ?? throw new ArgumentNullException(nameof(dataSource));

            // Set the route tracker.
            _routeTracker = routeTracker ?? throw new ArgumentNullException(nameof(routeTracker));

            // Change the tracker location when the source location changes.
            _inputDataSource.LocationChanged += InputLocationChanged;

            // Update the location output when the tracker location updates.
            _routeTracker.TrackingStatusChanged += TrackingStatusChanged;
        }

        private void InputLocationChanged(object sender, Location e)
        {
            // Update the tracker location with the new location from the source (simulation or GPS).
            _routeTracker.TrackLocationAsync(e);
        }

        private void TrackingStatusChanged(object sender, RouteTrackerTrackingStatusChangedEventArgs e)
        {
            // Check if the tracking status has a location.
            if (e.TrackingStatus.DisplayLocation != null)
            {
                // Call the base method for LocationDataSource to update the location with the tracked (snapped to route) location.
                UpdateLocation(e.TrackingStatus.DisplayLocation);
            }
        }

        protected override Task OnStartAsync() => _inputDataSource.StartAsync();

        protected override Task OnStopAsync() => _inputDataSource.StartAsync();


    }

}