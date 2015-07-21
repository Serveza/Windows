using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class NewUserPage : Page
    {
        public NewUserPage()
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

        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //ok
            if (passOneEntry.Password != passTwoEntry.Password)
            {
                var messageDialog = new MessageDialog("Password doesn't match");
                await messageDialog.ShowAsync();
                return;
            }
            if (App.Core.netWork.CreateAccount(firstnameEntry.Text, lastnameEntry.Text, mailEntry.Text, passOneEntry.Password))
            {
               /* App.Core.User.mail = mailEntry.Text;
                if (App.Core.User.Connect(passOneEntry.Password))
                    Frame.Navigate(typeof(Pages.HomePage));*/
            }
            var messageDialogtwo = new MessageDialog("Can't create your account");
            await messageDialogtwo.ShowAsync();
               

        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            //cancel
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //openFacebookConnection;
        }
    }
}
