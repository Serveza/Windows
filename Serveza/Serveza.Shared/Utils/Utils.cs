using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
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

        internal static ImageSource StringToImage(string url)
        {
            BitmapImage bitmapImage = new BitmapImage();
            Uri uri = new Uri(url);
            bitmapImage.UriSource = uri;
            return bitmapImage;
        }

        internal static void CreateSecondTile(string id, string name, string content, string argument, string imageUri, TileSize size)
        {
            SecondaryTile secondaryTile = new SecondaryTile(id, name, argument, new Uri(imageUri), size);
        }

        internal static Color GetColorFromHex(string hexString)
        {
            if (hexString.StartsWith("#"))
            {
                hexString = hexString.Substring(1, 8);
            }
            var a = Convert.ToByte(Int32.Parse(hexString.Substring(0, 2),
                System.Globalization.NumberStyles.AllowHexSpecifier));
            var r = Convert.ToByte(Int32.Parse(hexString.Substring(2, 2),
                System.Globalization.NumberStyles.AllowHexSpecifier));
            var g = Convert.ToByte(Int32.Parse(hexString.Substring(4, 2),
                System.Globalization.NumberStyles.AllowHexSpecifier));
            var b = Convert.ToByte(Int32.Parse(hexString.Substring(6, 2),
                System.Globalization.NumberStyles.AllowHexSpecifier));
            return Color.FromArgb(a, r, g, b);
        }
    }
}
