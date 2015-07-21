using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Serveza.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        protected ViewModelBase()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handle = this.PropertyChanged;
            if (handle != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handle(this, e);
            }
        }


        public void Dispose()
        {
            this.OnDispose();
        }

        protected virtual void OnDispose()
        {

        }
    }
}
