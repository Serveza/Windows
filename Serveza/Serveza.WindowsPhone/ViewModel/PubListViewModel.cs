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
    public class PubListViewModel : ViewModelBase
    {
        private PubListView _pubListView;
        private ObservableCollection<Pub> _list;
        private CoreDispatcher dispatcher;
        public ObservableCollection<Pub> list
        {
            get { return _list; }
            set
            {
                _list = value;
                update();
            }
        }

        public PubListViewModel(Grid mainGrid)
        {
            list = new ObservableCollection<Pub>();
            _pubListView = new PubListView();
            _pubListView.SetValue(Grid.RowProperty, 0);
            mainGrid.Children.Add(_pubListView);
            _pubListView.listViewP.SelectionChanged += listViewP_SelectionChanged;
        }

        void listViewP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                try
                {
                    App.Core.PubToDisplay = _list[_pubListView.listViewP.SelectedIndex];
                    rootFrame.Navigate(typeof(Pages.BarPage));
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
                _pubListView.listViewP.DataContext = _list;
            });
        }
    }
}
