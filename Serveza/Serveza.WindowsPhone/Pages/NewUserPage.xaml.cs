using Newtonsoft.Json.Linq;
using Serveza.Classes.Facebook;
using Serveza.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
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
    public sealed partial class NewUserPage : Page, IWebAuthenticationBrokerContinuable
    {
        FacebookCore core;
        public NewUserPage()
        {
            this.InitializeComponent();
            core = new FacebookCore();
            profilePictureUrl = "";
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

        }

        private async Task<bool> Register(string firstName, string lastName, string mail, string pass, string avatar = "")
        {
            if (passOneEntry.Password != passTwoEntry.Password)
            {
                var messageDialog = new MessageDialog("Password doesn't match");
                await messageDialog.ShowAsync();
                return false;
            }
            Serveza.Classes.Network.Register register = new Serveza.Classes.Network.Register();
            register.SetParam(firstName, lastName, mail, pass, avatar);
            JObject obj = await register.GetJsonAsync();
            if (obj != null)
            {
                try
                {
                    if (obj["email"].ToObject<string>() == mailEntry.Text)
                    {
                        var messageDialogt = new MessageDialog("Account registered !");
                        await messageDialogt.ShowAsync();

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
            var messageDialogtwo = new MessageDialog("Can't create your account");
            await messageDialogtwo.ShowAsync();
            return false;

        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (await Register(firstnameEntry.Text, lastnameEntry.Text, mailEntry.Text, passOneEntry.Password, profilePictureUrl))
            {
                if (await App.Core.Connect(mailEntry.Text, passOneEntry.Password))
                    Frame.Navigate(typeof(HomePage));
            }
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            //cancel
            Frame.GoBack();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Uri _callbackUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
            Debug.WriteLine(_callbackUri);
            core.LoginAndContinue();
        }

        public async void ContinueWithWebAuthenticationBroker(WebAuthenticationBrokerContinuationEventArgs args)
        {
            core.ContinueAuthentication(args);
            if (core.AccessToken != null)
            {
                Facebook.FacebookClient client = core.Client;

                dynamic result = await client.GetTaskAsync("/me");
                string id = result.id;

                string name = result.name;

                string[] plit = name.Split(' ');
                profilePictureUrl = string.Format("https://graph.facebook.com/{0}/picture?type={1}&access_token={2}", id, "square", core.AccessToken);

                UserImage.Fill = core.getUserImage(id);
                ChangePictureAnim.Begin();

                lastnameEntry.Text = plit[0];
                firstnameEntry.Text = plit[1];
               
                // Register(plit[0], plit[1], id, id, profilePictureUrl);
            }
        }

        public string profilePictureUrl { get; set; }
    }
}
