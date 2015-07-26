﻿using Newtonsoft.Json.Linq;
using Serveza.Utils;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.Web.Syndication;

namespace NotificationTask
{
    public sealed class NotificationTask : IBackgroundTask
    {
        private string _obj;
        public string obj
        {
            get { return _obj; }
            set { UpdateTile(value); }
        }

        private void UpdateTile(string value)
        {
            JObject obj = JObject.Parse(value);
            UpdateTile(obj);
        }
        BackgroundTaskDeferral deferral;
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            // Get a deferral, to prevent the task from closing prematurely 
            // while asynchronous code is still running.
            deferral = taskInstance.GetDeferral();
            Debug.WriteLine("run");
            // Download the feed.
            if (StorageApplication.GetValue("liveTile", "false") != "false")
            {
                string token = StorageApplication.GetValue("token", "toto");

                if (token != "toto")
                {
            //        GetJsonAsync(token);
                }
            }
           // Debug.WriteLine("UpdateTile");
            // Inform the system that the task is finished.
            
        }

        public  async void GetJsonAsync(string token)
        {
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);
            updater.Clear();

            Uri uri = new Uri("http://serveza.kokakiwi.net/api/user/notifications?api_token=" + token + "&update=true");
            using (var client = new HttpClient())
            {
                obj = await client.GetStringAsync(uri);
            }
        }


        private void UpdateTile(JObject obj)
        {
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);
            updater.Clear();
            Debug.WriteLine("UpdateTile");
            try
            {
                JArray NotificationList = obj["notifications"].ToObject<JArray>();
                foreach (JObject Notification in NotificationList)
                {

                    XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquarePeekImageAndText01);

                    string Name = Notification["name"].ToObject<string>() == null ? "" : Notification["name"].ToObject<string>();
                    string ImageUri = Notification["bar_image"].ToObject<string>() == null ? "none" : Notification["bar_image"].ToObject<string>();
                    tileXml.GetElementsByTagName(textElementName)[0].InnerText = "New Event";
                    tileXml.GetElementsByTagName(textElementName)[1].InnerText = Name;
                    tileXml.GetElementsByTagName(textElementName)[2].InnerText = Name;
                    tileXml.GetElementsByTagName("image")[0].Attributes.GetNamedItem("src").InnerText = "http://a5.mzstatic.com/us/r30/Purple5/v4/5a/2e/e9/5a2ee9b3-8f0e-4f8b-4043-dd3e3ea29766/icon128-2x.png";

                    updater.Update(new TileNotification(tileXml));
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            deferral.Complete();
        }

        private static async Task<SyndicationFeed> GetMSDNBlogFeed()
        {
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);
            updater.Clear();
            SyndicationFeed feed = null;

            try
            {
                // Create a syndication client that downloads the feed.  
                SyndicationClient client = new SyndicationClient();
                client.BypassCacheOnRetrieve = true;
                client.SetRequestHeader(customHeaderName, customHeaderValue);

                // Download the feed. 
                feed = await client.RetrieveFeedAsync(new Uri(feedUrl));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return feed;
        }

        private static void UpdateTile()
        {


        }

        private static void UpdateTile(SyndicationFeed feed)
        {
            // Create a tile update manager for the specified syndication feed.
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);
            updater.Clear();

            // Keep track of the number feed items that get tile notifications. 
            int itemCount = 0;

            // Create a tile notification for each feed item.
            foreach (var item in feed.Items)
            {
                XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquarePeekImageAndText01);
                Debug.WriteLine("item count : " + itemCount.ToString());
                var title = item.Title;
                string titleText = title.Text == null ? "" : title.Text;
                tileXml.GetElementsByTagName(textElementName)[0].InnerText = titleText;
                tileXml.GetElementsByTagName(textElementName)[1].InnerText = titleText;
                tileXml.GetElementsByTagName(textElementName)[2].InnerText = titleText;
                tileXml.GetElementsByTagName("image")[0].Attributes.GetNamedItem("src").InnerText = "http://a5.mzstatic.com/us/r30/Purple5/v4/5a/2e/e9/5a2ee9b3-8f0e-4f8b-4043-dd3e3ea29766/icon128-2x.png";
                Debug.WriteLine(tileXml.GetXml());

                var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
                var toastTextElements = toastXml.GetElementsByTagName("text");
                toastTextElements[0].InnerText = titleText;
                var toast = new ToastNotification(toastXml);

                ToastNotificationManager.CreateToastNotifier().Show(toast);

                // Create a new tile notification. 
                updater.Update(new TileNotification(tileXml));

                // Don't create more than 5 notifications.
                if (itemCount++ > 5) break;
            }
        }

        // Although most HTTP servers do not require User-Agent header, others will reject the request or return 
        // a different response if this header is missing. Use SetRequestHeader() to add custom headers. 
        static string customHeaderName = "User-Agent";
        static string customHeaderValue = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";

        static string textElementName = "text";
        static string feedUrl = @"http://blogs.msdn.com/b/MainFeed.aspx?Type=BlogsOnly";
    }
}
