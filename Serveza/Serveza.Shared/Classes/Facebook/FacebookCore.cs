using Facebook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Serveza.Classes.Facebook
{
    public class FacebookCore
    {
        private const string ApplicationId = "820922618027551";
        private const string AppSecret = "afcd049fa7b19a025f8405f9c3f2bb77";

        private string _token;
        private Windows.UI.Xaml.Controls.WebView BrowserControl;

        public string destinationURL
        {
            get
            {

                return String.Format(@"https://www.facebook.com/dialog/oauth?client_id={0}&scope=publish_actions,read_mailbox,user_posts,user_status&redirect_uri=http://www.facebook.com/connect/login_success.html&response_type=token",
                    ApplicationId
                    );
            }
        }

        public bool isLogin { get; private set; }

        public FacebookClient client { get { return new FacebookClient(_token); } }
     
        public FacebookCore(Windows.UI.Xaml.Controls.WebView BrowserControl)
        {
            
            this.BrowserControl = BrowserControl;
            _token = "";
            this.BrowserControl.NavigationCompleted += BrowserControl_NavigationCompleted;
        }

        void BrowserControl_NavigationCompleted(Windows.UI.Xaml.Controls.WebView sender, Windows.UI.Xaml.Controls.WebViewNavigationCompletedEventArgs args)
        {
            var url = args.Uri.Fragment;
            if (url.Contains("access_token") && url.Contains("#"))
            {
                url = (new Regex("#")).Replace(url, "?", 1);
                try 
                {
                    _token = Utils.Utils.ParseQueryString(new Uri(url), "access_token");
                }
                catch(Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
    }
}
