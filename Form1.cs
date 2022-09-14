using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RoutingClient.Model;
using Ptv.XServer.Controls.Map.Symbols;
using Ptv.XServer.Controls.Map.Layers.Shapes;
using System.Windows.Controls;
using Newtonsoft.Json;
using System.Windows.Media;
using Ptv.XServer.Controls.Map;
using Ptv.XServer.Controls.Map.Layers.Tiled;
using Ptv.XServer.Controls.Map.Localization;
using Ptv.XServer.Controls.Map.TileProviders;
using Ptv.XServer.Controls.Map.Tools;
using Color = System.Windows.Media.Color;
using Colors = System.Windows.Media.Colors;
using Point = System.Windows.Point;

namespace PtvDeveloperForms
{
    public partial class Form1 : Form
    {
        private static readonly string apiKey = ""; // Get your free key at https://developer.myptv.com/;
        public Form1()
        {
            InitializeComponent();
        }

        private ShapeLayer shapeLayer;
        private void Form1_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(apiKey))
            {
                MessageBox.Show("You need an api key! Get your free key at https://developer.myptv.com/");
                Application.ExitThread();
                return;
            }

            formsMap1.Layers.Add(new TiledLayer("Raster")
            {
                TiledProvider = new RemoteTiledProvider
                {
                    MinZoom = 0,
                    MaxZoom = 22,
                    RequestBuilderDelegate = (x, y, z) =>
                       $"https://api.myptv.com/rastermaps/v1/image-tiles/{z}/{x}/{y}?style=silica&apiKey={apiKey}",
                },
                IsBaseMapLayer = true,
                Copyright = "© 2022 PTV Group, HERE",
                Caption = MapLocalizer.GetString(MapStringId.Background),
                Icon = ResourceHelper.LoadBitmapFromResource("Ptv.XServer.Controls.Map;component/Resources/Background.png")
            });

            shapeLayer = new ShapeLayer("Routing");
            formsMap1.Layers.Add(shapeLayer);

            var routingApi = new RoutingClient.Api.RoutingApi(new RoutingClient.Client.Configuration
            {
                ApiKey = new Dictionary<string, string>
                {
                    ["apiKey"] = apiKey
                }
            });

            Point pStart = new Point(8.4, 49);
            Point pDest = new Point(10, 50);

            AddMarker(pStart, Colors.Green, "Start");
            AddMarker(pDest, Colors.Red, "Destination");

            // Set map focus
            formsMap1.SetEnvelope(new MapRectangle(pStart, pDest).Inflate(1.25));

            var routeResult = routingApi.CalculateRoutePost(new RouteRequest(waypoints: new List<Waypoint>
                {
                    new Waypoint{OffRoad = new OffRoadWaypoint{Longitude = pStart.X, Latitude = pStart.Y}},
                    new Waypoint{OffRoad = new OffRoadWaypoint{Longitude = pDest.X, Latitude = pDest.Y}}
                }),
                results: new List<Results> {
                    Results.POLYLINE,
            });

            // result is GeoJson, need to parse it vis Json.NET
            dynamic polyline = JsonConvert.DeserializeObject(routeResult.Polyline);
            var points = new PointCollection();
            foreach (var c in polyline.coordinates)
                points.Add(new Point((double)c[0], (double)c[1]));

            // Add the route polygon
            var mp = new MapPolyline
            {
                StrokeLineJoin = PenLineJoin.Round,
                StrokeStartLineCap = PenLineCap.Round,
                StrokeEndLineCap = PenLineCap.Round,
                MapStrokeThickness = 25,
                ScaleFactor = .1,
                Points = points,
                Stroke = new SolidColorBrush(Colors.Blue),
                ToolTip = $"Distance: {routeResult.Distance / 1000} km, Travel Time: {routeResult.TravelTime / 60} min"
            };

            shapeLayer.Shapes.Add(mp);
        }
        private void AddMarker(Point p, Color color, string toolTip)
        {

            var pin = new Pin
            {
                // Sets the color of the pin to green if it's the start waypoint. Otherwise red.
                Color = color,
                Width = 40,
                Height = 40,
                // Sets the name of the pin to Start it it's the start waypoint. Otherwise to End.
                ToolTip = toolTip,
            };

            // move in-front of route
            ShapeCanvas.SetZIndex(pin, 10);

            // Sets Anchor and Location of the pin.
            ShapeCanvas.SetAnchor(pin, LocationAnchor.RightBottom);
            ShapeCanvas.SetLocation(pin, p);


            //// Adds the pin to the layer.
            shapeLayer.Shapes.Add(pin);
        }
    }
}
