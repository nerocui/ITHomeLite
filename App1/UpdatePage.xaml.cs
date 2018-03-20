using App1.Models;
using Microsoft.Toolkit.Parsers.Rss;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UpdatePage : Page
    {
        public ObservableCollection<News> NewsList { get; set; }
        public HttpWebRequest request;
        public News _storeditem;
        public UpdatePage()
        {
            this.InitializeComponent();
            //RefreshPage();
            NewsList = new ObservableCollection<News>();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }
        
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.refreshed)
            {
                MyProgressRing.IsActive = true;
                MyProgressRing.Visibility = Visibility.Visible;
                string feed = null;
                using (var client = new HttpClient())
                {
                    try
                    {
                        feed = await client.GetStringAsync("https://www.ithome.com/rss/");
                    }
                    catch { }
                }

                if (feed != null)
                {
                    var parser = new RssParser();
                    var rss = parser.Parse(feed);

                    foreach (var element in rss)
                    {
                        string title = element.Title;
                        if (title.Contains("辣品"))
                            continue;
                        string link = element.FeedUrl;
                        string time = element.PublishDate.ToString();
                        string description = element.Content;
                        string picurl = element.ImageUrl;
                        string summary = element.Summary;
                        string author = element.Author;
                        News news = new News(title, link, time, description, picurl, summary, author);
                        NewsList.Add(news);
                    }
                }
                MyProgressRing.IsActive = false;
                MyProgressRing.Visibility = Visibility.Collapsed;
                App.refreshed = true;
            }
            
        }


        private async void DisplayNoWifiDialog(string value)
        {
            ContentDialog noWifiDialog = new ContentDialog
            {
                Title = "No wifi connection",
                Content = value,
                CloseButtonText = "Ok"
            };

            ContentDialogResult result = await noWifiDialog.ShowAsync();
        }


        private void AdaptiveGridViewControl_ItemClick(object sender, ItemClickEventArgs e)
        {
            News n = e.ClickedItem as News;
            //var animation = pre
            AdaptiveGridViewControl.PrepareConnectedAnimation("animation1", n, "UpdateItem");
            _storeditem = n;
            Frame.Navigate(typeof(DetailPage), n);

        }

        private async void AdaptiveGridViewControl_Loaded(object sender, RoutedEventArgs e)
        {
            if(AdaptiveGridViewControl != null && App.refreshed)
            {
                AdaptiveGridViewControl.ScrollIntoView(_storeditem, ScrollIntoViewAlignment.Default);
                AdaptiveGridViewControl.UpdateLayout();
                ConnectedAnimation animation2 = ConnectedAnimationService.GetForCurrentView().GetAnimation("animation2");
                if (animation2 != null)
                {
                    await AdaptiveGridViewControl.TryStartConnectedAnimationAsync(animation2, _storeditem, "UpdateItem");
                }
            }
            
            
        }
    }
}
