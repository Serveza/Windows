using Newtonsoft.Json.Linq;
using Serveza.Classes.BeerList;
using Serveza.Classes.Facebook;
using Serveza.Classes.Network;
using Serveza.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WinRTXamlToolkit.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Serveza
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        FacebookCore core;
        
        public MainPage()
        {
            this.InitializeComponent();
            core = new FacebookCore();
            this.NavigationCacheMode = NavigationCacheMode.Required;
            // progress.Visibility = Windows.UI.Xaml.Visibility.Collapsed;


        }


        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.RegisterBackgroundTask();
            Debug.WriteLine("oken : " + Utils.StorageApplication.GetValue("token", "toto"));
            if (Utils.StorageApplication.GetValue("token", "toto") != "toto")
                Connect(Utils.StorageApplication.GetValue("name", ""), Utils.StorageApplication.GetValue("pass", ""));
        }

        private async void Connect(string name, string pass)
        {
            try
            {
                ConnectAnnim.Begin();
                UserNameText.IsReadOnly = true;
                bool isConnect = await App.Core.Connect(name, pass);
                if (isConnect)
                {
                    ConnectAnnim.Stop();
                    Frame.Navigate(typeof(Pages.HomePage));
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            UserNameText.IsReadOnly = false;
            ConnectAnnim.Stop();
            CancelCoAnnim.Begin();
            var message = new MessageDialog("Can't connect");
            await message.ShowAsync();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Connect(UserNameText.Text, PassWordText.Password);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.NewUserPage));
        }

        private async void RegisterBackgroundTask()
        {
            var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
            if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
            {
                Debug.WriteLine("Register");
                foreach (var task in BackgroundTaskRegistration.AllTasks)
                {
                    if (task.Value.Name == taskName)
                    {
                        task.Value.Unregister(true);
                    }
                }

                BackgroundTaskBuilder taskBuilder = new BackgroundTaskBuilder();
                taskBuilder.Name = taskName;
                taskBuilder.TaskEntryPoint = taskEntryPoint;
                taskBuilder.SetTrigger(new TimeTrigger(15, false));
                var registration = taskBuilder.Register();
            }
        }

        private const string taskName = "NotificationTask";
        private const string taskEntryPoint = "NotificationTask.NotificationTask";

        private void PassWordText_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter)
            {
                // PassWordText.Focus(FocusState.Pointer);
                Connect(UserNameText.Text, PassWordText.Password);
            }
        }

        public async void ContinueWithWebAuthenticationBroker(WebAuthenticationBrokerContinuationEventArgs args)
        {
            core.ContinueAuthentication(args);
            if (core.AccessToken != null)
            {
                Facebook.FacebookClient client = core.Client;

                dynamic result = await client.GetTaskAsync("/me");
                string id = result.id;

                Connect(id, id);
            }
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Uri _callbackUri = WebAuthenticationBroker.GetCurrentApplicationCallbackUri();
            Debug.WriteLine(_callbackUri);
            core.LoginAndContinue();
        }
    }
}
