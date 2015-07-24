using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Serveza.Utils
{
    class NavigationControl
    {
        internal static void Navigate(Type page)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            rootFrame.Navigate(page);
        }
    }
}
