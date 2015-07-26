using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
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
    public sealed partial class AddCommentPage : Page
    {
        private int note;
        public AddCommentPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            if (App.Core.isBeer)
                init(App.Core.BeerToDisplay);
            else
                init(App.Core.PubToDisplay);
            note = 3;
        }

        private void init(Model.Pub pub)
        {
            Title.Text = pub.name;
        }

        private void init(Model.Beer beer)
        {
            Title.Text = beer.name;
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
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //send
            try
            {
                if (App.Core.isBeer)
                {
                    Debug.WriteLine("comment beer");
                    Classes.Network.AddComBeer addComBeer = new Classes.Network.AddComBeer();
                    addComBeer.SetParam(App.Core.netWork.token, App.Core.PubToDisplay.id, CommentText.Text, note);
                    await addComBeer.GetJsonAsync();
                }
                else
                {
                    Debug.WriteLine("comment bar");
                    Classes.Network.AddComBar addComBar = new Classes.Network.AddComBar();
                    addComBar.SetParam(App.Core.netWork.token, App.Core.PubToDisplay.id, CommentText.Text, note);
                    await addComBar.GetJsonAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            //cancel
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
            }
        }

        private void DisplayNote(bool one, bool two, bool tree, bool four, bool five)
        {
            note = 0;
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

        private void SetNote(Image note)
        {
            if (note == Note1)
                DisplayNote(true, false, false, false, false);
            else if (note == Note2)
                DisplayNote(true, true, false, false, false);
            else if (note == Note3)
                DisplayNote(true, true, true, false, false);
            else if (note == Note4)
                DisplayNote(true, true, true, true, false);
            else if (note == Note5)
                DisplayNote(true, true, true, true, true);

        }

        private void Note1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Image note = sender as Image;
            SetNote(note);
        }

    }
}
