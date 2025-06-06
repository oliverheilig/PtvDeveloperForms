﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Ptv.XServer.Controls.Map.Symbols;
using Ptv.XServer.Controls.Map.Layers.Shapes;
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
using ToolTip = System.Windows.Controls.ToolTip;
using PTV.Developer.Clients.routing.Api;
using PTV.Developer.Clients.routing.Client;
using PTV.Developer.Clients.routing.Model;


namespace PtvDeveloperForms
{
    public partial class Form1 : Form
    {
        private static readonly string myApiKey = ""; // Get your free key at https://developer.myptv.com/;
        public Form1()
        {
            InitializeComponent();
        }

        private ShapeLayer shapeLayer;
        private async void Form1_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(myApiKey))
            {
                MessageBox.Show("You need an api key! Get your free key at https://developer.myptv.com/");
                Application.ExitThread();
                return;
            }

            // Increase the number of parallel requests
            System.Net.ServicePointManager.DefaultConnectionLimit = 8;

            // initializes the map sytle
            comboBox1.SelectedIndex = 0;

            // The start and end locations Karlsrhe -> Berlin
            Point pStart = new Point(8.403951, 49.00921);
            Point pDest = new Point(13.408333, 52.518611);

            formsMap1.MaxZoom = 20;

            // Create a new shape layer containing the route result.
            // The LocalOffset parameter (located at Central Europe) reduces jitter at deep zoom levels.
            shapeLayer = new ShapeLayer("Routing") { LocalOffset = new Point(10, 50) };
            formsMap1.Layers.Add(shapeLayer);

            // Add markers for start and destination
            AddMarker(pStart, Colors.Green, "Karlsruhe");
            AddMarker(pDest, Colors.Red, "Berlin");

            // Set the map focus
            formsMap1.SetEnvelope(new MapRectangle(pStart, pDest).Inflate(1.25));

            var apiKey = new Dictionary<string, string>() { ["apiKey"] = myApiKey };
            var configuration = new Configuration()
            {
                ApiKey = apiKey,
                BasePath = "https://api.myptv.com/routing/v1"
            };
            RoutingApi routingApi = new RoutingApi(configuration);

            // Calculate the route
            var routeResult = await routingApi.CalculateRoutePostAsync(new RouteRequest(waypoints: new List<Waypoint>
                {
                    new Waypoint{OffRoad = new OffRoadWaypoint(longitude: pStart.X, latitude: pStart.Y)},
                    new Waypoint{OffRoad = new OffRoadWaypoint(longitude: pDest.X, latitude: pDest.Y)}
                }),
                results: new List<Results> {
                    Results.POLYLINE,
            }, vehicle: new Vehicle { });

            // The result is GeoJson, need to parse it vis Json.NET
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
                Stroke = new SolidColorBrush(Colors.Blue)
            };
            SetTooltip(mp, $"Distance: {routeResult.Distance / 1000} km, Travel Time: {routeResult.TravelTime / 60} min");

            shapeLayer.Shapes.Add(mp);
        }

        // The tooltip seems to be broken for polylines in the latest WPF version, this is a workaround.
        private void SetTooltip(System.Windows.FrameworkElement fe, string tooltip)
        {
            fe.MouseEnter += (o, s) =>
            {
                if (!(formsMap1.WrappedMap.ToolTip is ToolTip))
                    formsMap1.WrappedMap.ToolTip = new ToolTip { Content = tooltip };
                ((ToolTip)formsMap1.WrappedMap.ToolTip).IsOpen = true;
            };
            fe.MouseLeave += (o, s) => { if (formsMap1.WrappedMap.ToolTip is ToolTip) ((ToolTip)formsMap1.WrappedMap.ToolTip).IsOpen = false; };
        }

        private void AddMarker(Point p, Color color, string toolTip)
        {
            // craetae a pin-style symbol
            var pin = new Pin
            {
                Color = color,
                Width = 40,
                Height = 40,
                ToolTip = toolTip,
            };

            // move in-front of route
            ShapeCanvas.SetZIndex(pin, 10);

            // Sets Anchor and Location of the pin.
            ShapeCanvas.SetAnchor(pin, LocationAnchor.RightBottom);
            ShapeCanvas.SetLocation(pin, p);

            // Adds the pin to the layer.
            shapeLayer.Shapes.Add(pin);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            formsMap1.Layers.Remove(formsMap1.Layers["Road"]);
            formsMap1.Layers.Remove(formsMap1.Layers["Satellite"]);

            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    // Add the PTV-Developer raster map as base map
                    formsMap1.Layers.Add(new TiledLayer("Road")
                    {
                        TiledProvider = new RemoteTiledProvider
                        {
                            MinZoom = 0,
                            MaxZoom = 22,
                            RequestBuilderDelegate = (x, y, z) =>
                               $"https://api.myptv.com/rastermaps/v1/image-tiles/{z}/{x}/{y}?style=silkysand&apiKey={myApiKey}",
                        },
                        IsBaseMapLayer = true,
                        Copyright = $"© {DateTime.Now.Year} PTV Group, HERE",
                        Caption = MapLocalizer.GetString(MapStringId.Background),
                        Icon = ResourceHelper.LoadBitmapFromResource("Ptv.XServer.Controls.Map;component/Resources/Background.png")
                    });
                    break;
                case 1:
                    // Add the PTV-Developer satellite map as base map
                    formsMap1.Layers.Add(new TiledLayer("Satellite")
                    {
                        TiledProvider = new RemoteTiledProvider
                        {
                            MinZoom = 0,
                            MaxZoom = 20,
                            RequestBuilderDelegate = (x, y, z) =>
                               $"https://api.myptv.com/rastermaps/v1/satellite-tiles/{z}/{x}/{y}?apiKey={myApiKey}",
                        },
                        IsBaseMapLayer = true,
                        Copyright = $"© {DateTime.Now.Year} PTV Group, HERE",
                        Caption = MapLocalizer.GetString(MapStringId.Aerials),
                        Icon = ResourceHelper.LoadBitmapFromResource("Ptv.XServer.Controls.Map;component/Resources/Aerials.png")
                    });
                    break;
                case 2:
                    // Add the PTV-Developer satellite map as base map
                    formsMap1.Layers.Add(new TiledLayer("Satellite")
                    {
                        TiledProvider = new RemoteTiledProvider
                        {
                            MinZoom = 0,
                            MaxZoom = 20,
                            RequestBuilderDelegate = (x, y, z) =>
                               $"https://api.myptv.com/rastermaps/v1/satellite-tiles/{z}/{x}/{y}?apiKey={myApiKey}",
                        },
                        IsBaseMapLayer = true,
                        Copyright = $"© {DateTime.Now.Year} PTV Group, HERE",
                        Caption = MapLocalizer.GetString(MapStringId.Aerials),
                        Icon = ResourceHelper.LoadBitmapFromResource("Ptv.XServer.Controls.Map;component/Resources/Aerials.png")
                    });
                    // Add the PTV-Developer raster map as overlay
                    formsMap1.Layers.Add(new TiledLayer("Road")
                    {
                        TiledProvider = new RemoteTiledProvider
                        {
                            MinZoom = 0,
                            MaxZoom = 22,
                            RequestBuilderDelegate = (x, y, z) =>
                               $"https://api.myptv.com/rastermaps/v1/image-tiles/{z}/{x}/{y}?style=silkysand&layers=transport,labels&apiKey={myApiKey}",
                        },
                        IsBaseMapLayer = true,
                        Copyright = $"© {DateTime.Now.Year} PTV Group, HERE",
                        Caption = MapLocalizer.GetString(MapStringId.Background),
                        Icon = ResourceHelper.LoadBitmapFromResource("Ptv.XServer.Controls.Map;component/Resources/Background.png")
                    });
                    break;
            }
        }
    }
}
