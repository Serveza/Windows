using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace Serveza.Utils
{
    public class Utils
    {
        internal static ImageBrush UrlToFillSource(string url)
        {
            try
            {
                var bitmapImage = new BitmapImage();
                ImageBrush brush = new ImageBrush();
                bitmapImage.UriSource = new Uri(url);
                brush.ImageSource = bitmapImage;
                return brush;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return UrlToFillSource("ms-appx:///Assets/beer.png");
            }
        }

        internal static async void DisplayStatusCode(HttpStatusCode code)
        {
            MessageDialog message = null;
            switch (code)
            {

                case (HttpStatusCode.Forbidden):
                    message = new MessageDialog("Forbidden");
                    break;
                case (HttpStatusCode.GatewayTimeout):
                    message = new MessageDialog("Timeout");
                    break;
                case (HttpStatusCode.NotFound):
                    message = new MessageDialog("Not Found");
                    break;
            }
            if (message != null)
                await message.ShowAsync();
        }

        internal static string ParseQueryString(Uri uri, string search)
        {
            WwwFormUrlDecoder decoder = new WwwFormUrlDecoder(uri.Query);
            return decoder.GetFirstValueByName(search);
        }
    }
}
