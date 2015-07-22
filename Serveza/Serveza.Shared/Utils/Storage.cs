using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;

namespace Serveza.Utils
{
    public class Storage
    {
        internal static string GetValue(string key, string defaultValue)
        {
            if (ApplicationData.Current.LocalSettings.Values.ContainsKey(key))
            {
                return (string)(ApplicationData.Current.LocalSettings.Values[key]);
            }
            return defaultValue;
        }
    }
}
