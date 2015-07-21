﻿using Serveza.Classes.Facebook;
using Serveza.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
                dynamic result = await client.GetTaskAsync("me");
                string id = result.id;
                string email = result.email;
                string fn = result.first_name;
                string ln = result.last_name;
                UserImage.Fill = core.getUserImage(id);
                ChangePictureAnim.Begin();
                if (fn != null)
                    firstnameEntry.Text = fn;
                if (ln != null)
                    lastnameEntry.Text = ln;
                if (email != null)
                    mailEntry.Text = email;
            }
        }
    }
}