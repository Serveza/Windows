using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.Services.Maps;
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
    public sealed partial class EventPage : Page
    {
        public EventPage()
        {
            this.InitializeComponent();
        }

        private async void init()
        {
            EventNameText.Text = App.Core.EventToDisplay.name;
            StartText.Text = App.Core.EventToDisplay.startTime;
            EndText.Text = App.Core.EventToDisplay.endTime;
            DesciptionText.Text = App.Core.EventToDisplay.desciption;
            App.Core.LocationCore.AddOnMap(MapBar, App.Core.EventToDisplay);
            if (App.Core.EventToDisplay.address == null)
            {
                MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(App.Core.EventToDisplay.pos);
                try
                {
                    App.Core.EventToDisplay.address = result.Locations[0].Address.StreetNumber + " " + result.Locations[0].Address.Street + ", " + result.Locations[0].Address.Town;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            try
            {
                AddressText.Text = App.Core.EventToDisplay.address;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                AddressText.Text = "";
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            init();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
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
    }
}
