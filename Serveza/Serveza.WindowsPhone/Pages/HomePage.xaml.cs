using Serveza.Classes.BeerList;
using Serveza.Classes.PubList;
using Serveza.Model;
using Serveza.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Serveza.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private BeerListViewModel _myBeerListViewModel;

        private PubListViewModel _myPubListViewModel;

        private PubListViewModel _neerPubListViewModel;

        private EventListViewModel _NotifEventListViewModel;
        public HomePage()
        {
            this.InitializeComponent();
        }

        private void init()
        {
            _myBeerListViewModel = new BeerListViewModel(MyBeerGrid);
            App.Core.MyBeerList.blwm = _myBeerListViewModel;

            _myPubListViewModel = new PubListViewModel(MyBarGrid);
            App.Core.MyPubList.plvm = _myPubListViewModel;

            _neerPubListViewModel = new PubListViewModel(BarNeerGrid);
            App.Core.NeerPubList.plvm = _neerPubListViewModel;
            _NotifEventListViewModel = new EventListViewModel(NotifGrid);
            App.Core.User.eventList.elvm = _NotifEventListViewModel;

            UserNameText.Text = App.Core.User.name;
            UserImage.Fill = Utils.Utils.UrlToFillSource(App.Core.User.imageUrl);
        }

        private void setLocation()
        {
            MapBar.Children.Clear();
            App.Core.LocationCore.SetUserPosition(MapBar);
            foreach (Pub p in App.Core.NeerPubList.list)
            {
                App.Core.LocationCore.AddOnMap(MapBar, p);
            }
        }

      /*  private async void GetUserEvent()
        {
            Debug.WriteLine("getUserEvent");
            App.Core.User.eventList.Clear();
            Classes.Network.GetEvent getEvent = new Classes.Network.GetEvent();
            getEvent.SetParam(App.Core.netWork.token, Classes.Network.EventType.NONE);
            var obj = await getEvent.GetJsonAsync();
            App.Core.User.eventList.LoadEvent(obj);
            Debug.WriteLine("getUserEvent done");
        }*/

        private async void NeerBars()
        {
            
            Serveza.Classes.Network.NeerBar request = new Serveza.Classes.Network.NeerBar();
            var geo = await App.Core.LocationCore.GetUserPosition();
            request.setParam(geo, App.Core.settings.scope);
            var obj = await request.GetJsonAsync();
            if (App.Core.NeerPubList.Load(obj))
                setLocation();
            
        }
        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            init();
            setLocation();
            //NeerBars();
            //GetUserEvent();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            MyBeerGrid.Children.Clear();
            MyBarGrid.Children.Clear();
            BarNeerGrid.Children.Clear();
            MapBar.Children.Clear();
            base.OnNavigatingFrom(e);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            NeerBars();
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SearchPage));
        }

        private void AppBarButton_Click_2(object sender, RoutedEventArgs e)
        {
            Utils.StorageApplication.SetValue("token", "toto");
            Frame.Navigate(typeof(MainPage));
        }

        private void AppBarButton_Click_3(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.SettingsPage));
        }

        private void AppBarButton_Click_4(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.AboutPage));
        }
    }
}
