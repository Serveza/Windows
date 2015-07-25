using Serveza.Classes.DirectionList;
using Serveza.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Serveza.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BarPage : Page
    {
        public string appbarTileId = "SecondaryTile";
        private BeerListViewModel _beerListViewModel;
        private EventListViewModel _eventListViewModel;
        private DirectionListViewModel _directionListViewModel;
        private DirectionList _dl;
        public BarPage()
        {
            this.InitializeComponent();
            _beerListViewModel = new BeerListViewModel(beerGrid);
            _eventListViewModel = new EventListViewModel(eventGrid);
            _directionListViewModel = new DirectionListViewModel(DirectionGrid);
        }

        public void Init(Model.Pub pub)
        {
            BarNameText.Text = pub.name;
            appbarTileId += pub.url;
            DistanceText.Text = pub.dist.ToString() + " Km";
            AddressText.Text = (pub.address == null ? "" : pub.address);
            pub.beerList.blwm = _beerListViewModel;
            pub.eventList.elvm = _eventListViewModel;
            _dl = new DirectionList(_directionListViewModel);
            DisplayOnMap();
        }

        private void DisplayOnMap()
        {
            App.Core.LocationCore.SetUserPosition(MapBar);
            App.Core.LocationCore.AddOnMap(MapBar, App.Core.PubToDisplay);
            App.Core.LocationCore.GetRouteAndDirection(App.Core.PubToDisplay, MapBar, _dl);
            //App.Core.LocationCore.GetRouteAndDirections(App.Core.PubToDisplay, MapBar, DirectionText);
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Init(App.Core.PubToDisplay);
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
                e.Handled = true;
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            MapBar.Children.Clear();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.AddCommentPage));
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            DisplayOnMap();
        }

        private void AppBarToggleButton_Click(object sender, RoutedEventArgs e)
        {
            //fav
        }
    }
}
