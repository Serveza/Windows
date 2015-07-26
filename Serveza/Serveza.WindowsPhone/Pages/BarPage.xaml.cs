using Serveza.Classes.DirectionList;
using Serveza.ViewModel;
using System;
using System.Diagnostics;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace Serveza.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BarPage : Page
    {
        public string appbarTileId = "SecondaryTile";
        private BeerListViewModel _beerListViewModel;
        private CommentListViewModel _commentListViewModel;
        private EventListViewModel _eventListViewModel;
        private DirectionListViewModel _directionListViewModel;
        private DirectionList _dl;
        public BarPage()
        {
            this.InitializeComponent();
            _beerListViewModel = new BeerListViewModel(beerGrid);
            _eventListViewModel = new EventListViewModel(eventGrid);
            _directionListViewModel = new DirectionListViewModel(DirectionGrid);
            _commentListViewModel = new CommentListViewModel(CommentGrid);
        }

        public void Init(Model.Pub pub)
        {
            BarNameText.Text = pub.name;

            linkWebSite.NavigateUri = new Uri(pub.website);

            appbarTileId += pub.url;
            DistanceText.Text = pub.dist.ToString() + " Km";
            AddressText.Text = (pub.address == null ? "" : pub.address);
            linkWebSite.Content = pub.website;
            barimage.Fill = Utils.Utils.UrlToFillSource(pub.image);

            pub.beerList.blwm = _beerListViewModel;
            pub.commentList.clvm = _commentListViewModel;

            _dl = new DirectionList(_directionListViewModel);
            DisplayOnMap();
            SetFav(pub.isFav);
        }

        private void SetFav(bool fav)
        {
            if (!fav)
            {
                favButton.Label = "favorite";
                favButton.IsEnabled = true;
            }
            else
            {
                favButton.Label = "unfavorite";
                favButton.IsEnabled = false;
            }
        }

        private async void GetEvent()
        {
            try
            {
                Classes.Network.GetEvent getEvent = new Classes.Network.GetEvent();
                getEvent.SetParam(App.Core.netWork.token, App.Core.PubToDisplay);
                Debug.WriteLine("GetEvent By Bar");
                var jobj = await getEvent.GetJsonAsync();

                App.Core.PubToDisplay.LoadEvent(jobj);
                App.Core.PubToDisplay.eventList.elvm = _eventListViewModel;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

        }

        private async void DisplayOnMap()
        {
            App.Core.LocationCore.SetUserPosition(MapBar);
            App.Core.LocationCore.AddOnMap(MapBar, App.Core.PubToDisplay);
            await App.Core.LocationCore.GetRouteAndDirection(App.Core.PubToDisplay, MapBar, _dl);
            DistanceText.Text = _dl.dist.ToString() + " Km";
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;

            Init(App.Core.PubToDisplay);
            GetEvent();
            GetComment();
        }

       

        private async void GetComment()
        {
            try
            {
                Classes.Network.GetBarComment getbarComment = new Classes.Network.GetBarComment();
                getbarComment.SetParam(App.Core.PubToDisplay.id);
                var obj = await getbarComment.GetJsonAsync();
                App.Core.PubToDisplay.commentList.Load(obj);
                DisplayNote(App.Core.PubToDisplay.commentList.moyen);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void DisplayNote(int note)
        {
            if (note == 1)
                DisplayNote(true, false, false, false, false);
            else if (note == 2)
                DisplayNote(true, true, false, false, false);
            else if (note == 3)
                DisplayNote(true, true, true, false, false);
            else if (note == 4)
                DisplayNote(true, true, true, true, false);
            else if (note == 5)
                DisplayNote(true, true, true, true, true);
        }

        private void DisplayNote(bool one, bool two, bool tree, bool four, bool five)
        {
            int note = 0;
            if (one)
            {
                Note1.Source = Utils.Utils.StringToImage("ms-appx:///Assets/beer.png");
                note++;
            }
            else
                Note1.Source = Utils.Utils.StringToImage("ms-appx:///Assets/beer-d.png");

            if (two)
            {
                Note2.Source = Utils.Utils.StringToImage("ms-appx:///Assets/beer.png");
                note++;
            }
            else
                Note2.Source = Utils.Utils.StringToImage("ms-appx:///Assets/beer-d.png");

            if (tree)
            {
                Note3.Source = Utils.Utils.StringToImage("ms-appx:///Assets/beer.png");
                note++;
            }
            else
                Note3.Source = Utils.Utils.StringToImage("ms-appx:///Assets/beer-d.png");

            if (four)
            {
                Note4.Source = Utils.Utils.StringToImage("ms-appx:///Assets/beer.png");
                note++;
            }
            else
                Note4.Source = Utils.Utils.StringToImage("ms-appx:///Assets/beer-d.png");

            if (five)
            {
                Note5.Source = Utils.Utils.StringToImage("ms-appx:///Assets/beer.png");
                note++;
            }
            else
                Note5.Source = Utils.Utils.StringToImage("ms-appx:///Assets/beer-d.png");

        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
                e.Handled = true;
            }
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            MapBar.Children.Clear();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.AddCommentPage));
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            DisplayOnMap();
        }

        private async void AppBarToggleButton_Click(object sender, RoutedEventArgs e)
        {
            //fav
            if (App.Core.PubToDisplay.isFav == false)
            {
                try
                {
                    Classes.Network.AddFavBar addFavBar = new Classes.Network.AddFavBar();
                    addFavBar.SetParam(App.Core.netWork.token, App.Core.PubToDisplay.id);
                    await addFavBar.GetJsonAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                App.Core.PubToDisplay.isFav = true;
                App.Core.MyPubList.Add(App.Core.PubToDisplay);
                SetFav(true);
            }
        }
    }
}
