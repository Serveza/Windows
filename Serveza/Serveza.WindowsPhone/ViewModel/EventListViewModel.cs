using Serveza.Model;
using Serveza.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Serveza.ViewModel
{
    public class EventListViewModel
    {
        private EventListView _eventListView;
        private ObservableCollection<Event> _list;
        private CoreDispatcher dispatcher;

        public ObservableCollection<Event> list
        {
            get { return _list; }
            set
            {
                _list = value;
                update();
            }
        }

        public EventListViewModel(Grid mainGrid)
        {
            list = new ObservableCollection<Event>();
            _eventListView = new EventListView();
            _eventListView.SetValue(Grid.RowProperty, 0);
            mainGrid.Children.Add(_eventListView);
            _eventListView.pubListView.SelectionChanged += pubListView_SelectionChanged;
        }

        void pubListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                try
                {
                    App.Core.EventToDisplay = _list[_eventListView.pubListView.SelectedIndex];
                    rootFrame.Navigate(typeof(Pages.EventPage));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        private async void update()
        {
            dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;
            await this.dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
           {
               _eventListView.pubListView.DataContext = _list;
           });

        }
    }
}
