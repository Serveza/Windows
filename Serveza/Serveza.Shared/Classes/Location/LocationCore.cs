using Serveza.MapPoint;
using Serveza.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Documents;

namespace Serveza.Classes.Location
{
    public class LocationCore
    {
        private Geolocator geolocator;

        public LocationCore()
        {
            geolocator = new Geolocator();
            geolocator.DesiredAccuracyInMeters = 50;
        }

        public async void SetUserPosition(MapControl map)
        {
            UserMapPoint userMapPoint = new UserMapPoint();

            Geoposition geoposition = await geolocator.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(5), timeout: TimeSpan.FromSeconds(10));
            map.Children.Add(userMapPoint);
            var location = new Geopoint(new BasicGeoposition()
        {
            Latitude = geoposition.Coordinate.Latitude,
            Longitude = geoposition.Coordinate.Longitude
        });
            MapControl.SetLocation(userMapPoint, location);
            await map.TrySetViewAsync(location, 10.0, 0, 0, MapAnimationKind.Bow);
        }


        public async Task<Geoposition> GetUserPosition()
        {
            Geoposition geoposition = await geolocator.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(5), timeout: TimeSpan.FromSeconds(10));
            return geoposition;
        }


        public void AddOnMap(MapControl map, Pub pub)
        {
            PubMapPoint pmb = new PubMapPoint(pub.name);
            map.Children.Add(pmb);
            MapControl.SetLocation(pmb, pub.pos);
        }

        public async void AddOnMap(MapControl map, Event eventt)
        {
            PubMapPoint pmb = new PubMapPoint(eventt.name);
            map.Children.Add(pmb);
            MapControl.SetLocation(pmb, eventt.pos);
            await map.TrySetViewAsync(eventt.pos, 10.0, 0, 0, MapAnimationKind.Bow);
        }


        public async Task<bool> GetRouteAndDirection(Pub pub, MapControl map, DirectionList.DirectionList list)
        {
            Geoposition geoposition = await geolocator.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(5), timeout: TimeSpan.FromSeconds(10));
            BasicGeoposition startLocation = new BasicGeoposition();
            startLocation.Latitude = geoposition.Coordinate.Latitude;
            startLocation.Longitude = geoposition.Coordinate.Longitude;
            Geopoint startPoint = new Geopoint(startLocation);

            BasicGeoposition endLocation = new BasicGeoposition();
            endLocation.Latitude = pub.latitude;
            endLocation.Longitude = pub.longitude;
            Geopoint endPoint = new Geopoint(endLocation);

            MapRouteFinderResult routeResult = await MapRouteFinder.GetDrivingRouteAsync(
                startPoint,
                endPoint,
                MapRouteOptimization.Time,
                MapRouteRestrictions.None);
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                list.Load(routeResult);
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Blue;
                viewOfRoute.OutlineColor = Colors.Black;
            }
            else
            {
                var message = new MessageDialog("Can't find Direction");
                await message.ShowAsync();
            }
            return true;
        }

        public async void GetRouteAndDirections(Pub pub, MapControl map, TextBlock tbOutputText)
        {
            Geoposition geoposition = await geolocator.GetGeopositionAsync(maximumAge: TimeSpan.FromMinutes(5), timeout: TimeSpan.FromSeconds(10));
            BasicGeoposition startLocation = new BasicGeoposition();
            startLocation.Latitude = geoposition.Coordinate.Latitude;
            startLocation.Longitude = geoposition.Coordinate.Longitude;
            Geopoint startPoint = new Geopoint(startLocation);

            BasicGeoposition endLocation = new BasicGeoposition();
            endLocation.Latitude = pub.latitude;
            endLocation.Longitude = pub.longitude;
            Geopoint endPoint = new Geopoint(endLocation);

            MapRouteFinderResult routeResult =
                await MapRouteFinder.GetDrivingRouteAsync(
                startPoint,
                endPoint,
                MapRouteOptimization.Time,
                MapRouteRestrictions.None);
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Display summary info about the route.
                tbOutputText.Inlines.Add(new Run()
                {
                    Text = "Total estimated time (minutes) = "
                        + routeResult.Route.EstimatedDuration.TotalMinutes.ToString()
                });
                tbOutputText.Inlines.Add(new LineBreak());
                tbOutputText.Inlines.Add(new Run()
                {
                    Text = "Total length (kilometers) = "
                        + (routeResult.Route.LengthInMeters / 1000).ToString()
                });
                tbOutputText.Inlines.Add(new LineBreak());
                tbOutputText.Inlines.Add(new LineBreak());

                // Display the directions.
                tbOutputText.Inlines.Add(new Run()
                {
                    Text = "DIRECTIONS"
                });
                tbOutputText.Inlines.Add(new LineBreak());

                foreach (MapRouteLeg leg in routeResult.Route.Legs)
                {
                    foreach (MapRouteManeuver maneuver in leg.Maneuvers)
                    {
                        tbOutputText.Inlines.Add(new Run()
                        {
                            Text = maneuver.InstructionText
                        });
                        tbOutputText.Inlines.Add(new LineBreak());
                    }
                }
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Yellow;
                viewOfRoute.OutlineColor = Colors.Black;

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                map.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await map.TrySetViewBoundsAsync(
                    routeResult.Route.BoundingBox,
                    null,
                    Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            }
            else
            {
                tbOutputText.Text =
                    "A problem occurred: " + routeResult.Status.ToString();
            }
            Debug.WriteLine(tbOutputText.Text);

        }


        public async Task<string> GetPubAdress(Pub pub)
        {
            BasicGeoposition location = new BasicGeoposition();
            location.Latitude = pub.latitude;
            location.Longitude = pub.longitude;
            Geopoint pointToReverseGeocode = new Geopoint(location);

            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(pointToReverseGeocode);

            string res = "";
            try
            {
                res = result.Locations[0].Address.StreetNumber + " " + result.Locations[0].Address.Street + ", " + result.Locations[0].Address.Town;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            return res;
        }
    }
}
