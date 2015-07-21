using Serveza.Classes.BeerList;
using Serveza.Classes.PubList;
using Serveza.Model;
using Serveza.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Serveza.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        private BeerList _myBeerList;
        private BeerListViewModel _myBeerListViewModel;
        private PubList _myPubList;
        private PubListViewModel _myPubListViewModel; 
        public HomePage()
        {
            this.InitializeComponent();
            init();
        }

        private void init()
        {
            _myBeerListViewModel = new BeerListViewModel(MyBeerGrid);
            _myBeerList = new BeerList(_myBeerListViewModel);
            _myBeerList.Add(new Beer("Guiness", 4.2));
            _myBeerList.Add(new Beer("Leff", 5));
            _myBeerList.Add(new Beer("Rince Cochon", 8.5));
            _myBeerList.Add(new Beer("Cuvee des trols", 7));
            _myPubListViewModel = new PubListViewModel(MyBarGrid);
            _myPubList = new PubList(_myPubListViewModel);
            _myPubList.Add(new Pub("Imaginaire"));
            _myPubList.Add(new Pub("Capsule"));
            _myPubList.Add(new Pub("Chopp'ing"));
            _myPubList.Add(new Pub("O'Scotland"));

        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }
    }
}
