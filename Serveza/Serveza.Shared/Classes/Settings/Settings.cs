using System;
using System.Collections.Generic;
using System.Text;

namespace Serveza.Classes.Settings
{
    public class Settings
    {
        private double _scope;
        private bool _liveTile;
        private bool _notification;
        private bool _useConstLocation;

        public double scope
        {
            get { return _scope; }
            set
            {
                _scope = value;
                Utils.StorageApplication.SetValue("scope", _scope.ToString());
            }
        }
        public bool liveTile
        {
            get { return _liveTile; }
            set
            {
                _liveTile = value;
                Utils.StorageApplication.SetValue("liveTile", _liveTile.ToString());
            }
        }
        public bool notification
        {
            get { return _notification; }
            set
            {
                _notification = value;
                Utils.StorageApplication.SetValue("notification", _scope.ToString());
            }
        }
        public bool useConstLocation
        {
            get { return _useConstLocation; }
            set
            {
                _useConstLocation = value;
                Utils.StorageApplication.SetValue("useConstLocation", _scope.ToString());
            }
        }

        public Settings(bool init = true)
        {
            if (init)
            {
                _scope = Convert.ToDouble(Utils.StorageApplication.GetValue("scope", "10"));
                if (Utils.StorageApplication.GetValue("notification", "true") == "true")
                    _notification = true;
                else
                    _notification = false;
                if (Utils.StorageApplication.GetValue("useConstLocation", "true") == "true")
                    _useConstLocation = true;
                else
                    _useConstLocation = false;
                if (Utils.StorageApplication.GetValue("liveTile", "true") == "true")
                    _liveTile = true;
                else
                    _liveTile = false;
            }
        }
    }
}
