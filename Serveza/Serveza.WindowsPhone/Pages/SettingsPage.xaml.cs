using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
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
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            init();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
           
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
                e.Handled = true;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            App.Core.settings.notification = notificationToggle.IsOn;
            App.Core.settings.notification = notificationToggle.IsOn;
            App.Core.settings.useConstLocation = folowMeToggle.IsOn;
            App.Core.settings.scope = sliderScope.Value;
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        private void init()
        {
            UserImage.Fill = Utils.Utils.UrlToFillSource(App.Core.User.imageUrl);
            sliderScope.Value = (int)App.Core.settings.scope;
            notificationToggle.IsOn = App.Core.settings.notification;
            liveTileToggle.IsOn = App.Core.settings.liveTile;
            folowMeToggle.IsOn = App.Core.settings.useConstLocation;
        }
    }
}
