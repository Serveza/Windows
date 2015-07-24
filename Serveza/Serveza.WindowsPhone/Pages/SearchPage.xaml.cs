using Serveza.Classes.BeerList;
using Serveza.Model;
using Serveza.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public sealed partial class SearchPage : Page
    {
        private BeerListViewModel blvm;
        public SearchPage()
        {
            this.InitializeComponent();
            init();
        }

        private void init()
        {
            blvm = new BeerListViewModel(beerListSelected);
            App.Core.SearchBeerList.blwm = blvm;
            SearchBox.DataContext = App.Core.MyBeerList.list;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.Core.NeerBeer = App.Core.NeerPubList.getBeerList();
        }

        private void SearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            Debug.WriteLine(sender.Text);
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                sender.DataContext = App.Core.NeerBeer.Search(sender.Text);
                Debug.WriteLine(App.Core.NeerBeer.Search(sender.Text).Count);
            }
        }

        private void SearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            Beer beer = args.SelectedItem as Beer;
            sender.Text = "";
            //   sender.DataContext = null;
            App.Core.SearchBeerList.Add(beer);
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            App.Core.SearchBeerList.Clear();
        }
    }
}
