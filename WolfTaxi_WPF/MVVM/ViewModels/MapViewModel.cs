// Copyright 2021 Esri
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;

using System.Linq;
using System.Threading.Tasks;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Tasks.NetworkAnalysis;
using Esri.ArcGISRuntime.UI;

namespace FindARouteAndDirections
{
    class MapViewModel : INotifyPropertyChanged
    {

        enum RouteBuilderStatus
        {
            NotStarted, // No locations have been defined.
            SelectedStart, // Origin point exists.
            SelectedStartAndEnd // Origin and destination exist.
        }
        private RouteBuilderStatus _currentState = RouteBuilderStatus.NotStarted;

        private Graphic _startGraphic;
        private Graphic _endGraphic;
        private Graphic _routeGraphic;

        public MapViewModel()
        {
            SetupMap();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Map _map;
        public Map Map
        {
            get { return _map; }
            set
            {
                _map = value;
                OnPropertyChanged();
            }
        }

        private GraphicsOverlayCollection _graphicsOverlayCollection;
        public GraphicsOverlayCollection GraphicsOverlays
        {
            get { return _graphicsOverlayCollection; }
            set
            {
                _graphicsOverlayCollection = value;
                OnPropertyChanged();
            }
        }

        private List<string> _directions;
        public List<string> Directions
        {
            get { return _directions; }
            set
            {
                _directions = value;
                OnPropertyChanged();
            }
        }

        private void SetupMap()
        {

            Map = new Map(BasemapStyle.ArcGISStreets);

            GraphicsOverlay routeAndStopsOverlay = new GraphicsOverlay();
            this.GraphicsOverlays = new GraphicsOverlayCollection
            {
                routeAndStopsOverlay
            };

            var startOutlineSymbol = new SimpleLineSymbol(SimpleLineSymbolStyle.Solid, System.Drawing.Color.Blue, 2);
            _startGraphic = new Graphic(null, new SimpleMarkerSymbol
            {
                Style = SimpleMarkerSymbolStyle.Diamond,
                Color = System.Drawing.Color.Orange,
                Size = 8,
                Outline = startOutlineSymbol
            }
            );

            var endOutlineSymbol = new SimpleLineSymbol(style: SimpleLineSymbolStyle.Solid, color: System.Drawing.Color.Red, width: 2);
            _endGraphic = new Graphic(null, new SimpleMarkerSymbol
            {
                Style = SimpleMarkerSymbolStyle.Square,
                Color = System.Drawing.Color.Green,
                Size = 8,
                Outline = endOutlineSymbol
            }
            );

            _routeGraphic = new Graphic(null, new SimpleLineSymbol(
                style: SimpleLineSymbolStyle.Solid,
                color: System.Drawing.Color.Blue,
                width: 4
            ));

            routeAndStopsOverlay.Graphics.AddRange(new[] { _startGraphic, _endGraphic, _routeGraphic });

        }

        private void ResetState()
        {
            _startGraphic.Geometry = null;
            _endGraphic.Geometry = null;
            _routeGraphic.Geometry = null;
            Directions = null;
            _currentState = RouteBuilderStatus.NotStarted;
        }

        public async Task FindRoute()
        {

            var stops = new[] { _startGraphic, _endGraphic }.Select(graphic => new Stop(graphic.Geometry as MapPoint));

            var routeTask = await RouteTask.CreateAsync(new Uri("https://route-api.arcgis.com/arcgis/rest/services/World/Route/NAServer/Route_World"));
            RouteParameters parameters = await routeTask.CreateDefaultParametersAsync();
            parameters.SetStops(stops);
            parameters.ReturnDirections = true;
            parameters.ReturnRoutes = true;

            var result = await routeTask.SolveRouteAsync(parameters);

            if (result?.Routes?.FirstOrDefault() is Route routeResult)
            {
                _routeGraphic.Geometry = routeResult.RouteGeometry;
                Directions = routeResult.DirectionManeuvers.Select(maneuver => maneuver.DirectionText).ToList();
                _currentState = RouteBuilderStatus.NotStarted;
            }
            else
            {
                ResetState();
                throw new Exception("Route not found");
            }

        }

        public async Task HandleTap(MapPoint tappedPoint)
        {
            switch (_currentState)
            {
                case RouteBuilderStatus.NotStarted:
                    ResetState();
                    _startGraphic.Geometry = tappedPoint;
                    _currentState = RouteBuilderStatus.SelectedStart;
                    break;
                case RouteBuilderStatus.SelectedStart:
                    _endGraphic.Geometry = tappedPoint;
                    _currentState = RouteBuilderStatus.SelectedStartAndEnd;
                    await FindRoute();
                    break;
                case RouteBuilderStatus.SelectedStartAndEnd:
                    // Ignore map clicks while routing is in progress
                    break;
            }
        }

    }
}

