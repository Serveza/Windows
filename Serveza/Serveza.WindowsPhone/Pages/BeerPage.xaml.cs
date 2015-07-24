using Serveza.Classes.BeerList;
using Serveza.Classes.CommentList;
using Serveza.Classes.PubList;
using Serveza.Model;
using Serveza.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.UI.Core;
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
    public sealed partial class BeerPage : Page
    {
        private CommentListViewModel clwm;
        private CommentList comList;

     //   private CoreDispatcher dispatcher;

        private PubListViewModel plvm;
        private PubList pubList;
        public BeerPage()
        {
            this.InitializeComponent();
            Init(App.Core.BeerToDisplay);
        }

        public void Init(Beer beer)
        {

            beer.GetInfo();
            BeerName.Text = beer.name;
            //title.Text == null ? "" : title.Text;
            ProductText.Text = ( beer.product == null ? beer.price.ToString() + "€" : beer.product);
        //    DegreeText.Text = beer.degre + " %";

            clwm = new CommentListViewModel(CommentList);
            comList = new CommentList(clwm);
            plvm = new PubListViewModel(BarList);
            pubList = new PubList(plvm);

            pubList.Add(new Pub("Imaginaire", 50.6404403, 3.064249));
            pubList.Add(new Pub("Capsule", 50.6405435, 3.0601101));
            pubList.Add(new Pub("Chopp'ing", 50.6393988, 3.058493));
            pubList.Add(new Pub("O'Scotland", 50.6308288, 3.0552842));

            comList.Add(new Comment("Good One", "Thomas", 3));
            comList.Add(new Comment("Good One", "Thomas", 3));
            comList.Add(new Comment("Good One", "Thomas", 3));
            comList.Add(new Comment("Good One", "Thomas", 3));
            comList.Add(new Comment("Good One", "Thomas", 3));
            comList.Add(new Comment("Good One", "Thomas", 3));
            comList.Add(new Comment("Good One", "Thomas", 3));

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            App.Core.LocationCore.SetUserPosition(MapBar);
            foreach (Pub p in pubList.list)
            {
                App.Core.LocationCore.AddOnMap(MapBar, p);
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            MapBar.Children.Clear();
            App.Core.LocationCore.SetUserPosition(MapBar);
            foreach (Pub p in pubList.list)
            {
                App.Core.LocationCore.AddOnMap(MapBar, p);
            }
        }


        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                MapBar.Children.Clear();
                rootFrame.GoBack();
                e.Handled = true;
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.AddCommentPage));
        }
    }

}
