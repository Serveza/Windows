using Serveza.Classes.BeerList;
using Serveza.Classes.CommentList;
using Serveza.Classes.PubList;
using Serveza.Model;
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
    public sealed partial class BeerPage : Page
    {
        private CommentListViewModel clwm;
        private CommentList comList;

        //   private CoreDispatcher dispatcher;

        private PubListViewModel plvm;
        private PubList pubList;
        public BeerPage()
        {
            this.InitializeComponent();

        }

        public async void Init(Beer beer)
        {
            Classes.Network.GetBeerInfo getBeerInfo = new Classes.Network.GetBeerInfo();
            getBeerInfo.SetParam(beer.id);
            beer.GetInfo(await getBeerInfo.GetJsonAsync());
            BeerName.Text = beer.name;
            ProductText.Text = (beer.price != 0 ? beer.price.ToString() + "€" : "");
            degreeText.Text = beer.degre.ToString();
            DesciptionText.Text = beer.desc;
            breweryText.Text = beer.product;
            BeerImage.Fill = Utils.Utils.UrlToFillSource(beer.image);

            clwm = new CommentListViewModel(CommentList);
            comList = new CommentList(clwm);
            plvm = new PubListViewModel(BarList);
            BeerList tmp = new BeerList();
            tmp.Add(beer);
            pubList = App.Core.NeerPubList.SearchBarByBeer(tmp);
            pubList.plvm = plvm;
            GetComment();
            foreach (Pub p in pubList.list)
            {
                App.Core.LocationCore.AddOnMap(MapBar, p);
            }
        }

        private async void GetComment()
        {
            try
            {
                Classes.Network.GetComBeer getbarComment = new Classes.Network.GetComBeer();
                getbarComment.SetParam(App.Core.BeerToDisplay.id);
                var obj = await getbarComment.GetJsonAsync();
                comList.Load(obj);
                DisplayNote(comList.moyen);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            App.Core.LocationCore.SetUserPosition(MapBar);
            Init(App.Core.BeerToDisplay);


        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            MapBar.Children.Clear();
            App.Core.LocationCore.SetUserPosition(MapBar);
            foreach (Pub p in pubList.list)
            {
                App.Core.LocationCore.AddOnMap(MapBar, p);
            }
        }


        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                MapBar.Children.Clear();
                rootFrame.GoBack();
                e.Handled = true;
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

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Pages.AddCommentPage));
        }

        private async void favButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.Core.BeerToDisplay.isFav == false)
            {
                try
                {
                    Classes.Network.AddFavBeer addFavBar = new Classes.Network.AddFavBeer();
                    addFavBar.SetParam(App.Core.netWork.token, App.Core.BeerToDisplay.id);
                    await addFavBar.GetJsonAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
                App.Core.BeerToDisplay.isFav = true;
                App.Core.MyBeerList.Add(App.Core.BeerToDisplay);
                SetFav(true);
            }
        }

        private void SetFav(bool p)
        {
            // throw new NotImplementedException();
        }
    }

}
