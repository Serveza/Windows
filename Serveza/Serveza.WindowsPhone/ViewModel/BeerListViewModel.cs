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
    public class BeerListViewModel : ViewModelBase
    {
        private BeerListView _beerListView;
        private ObservableCollection<Beer> _list;
        private CoreDispatcher dispatcher;
        public ObservableCollection<Beer> list
        {
            get { return _list; }
            set
            {
                _list = value;
                update();
            }
        }

        public BeerListViewModel(Grid mainGrid)
        {
            list = new ObservableCollection<Beer>();
            _beerListView = new BeerListView();
            _beerListView.SetValue(Grid.RowProperty, 0);
            mainGrid.Children.Add(_beerListView);
            _beerListView.listViewP.SelectionChanged += listViewP_SelectionChanged;
        }

        void listViewP_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                try
                {
                    App.Core.BeerToDisplay = _list[_beerListView.listViewP.SelectedIndex];
                    rootFrame.Navigate(typeof(Pages.BeerPage));
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
                _beerListView.listViewP.DataContext = _list;
            });
        }

        public void detach()
        {
            // _parent.Children.Remove(_beerListView);
        }
    }
}
