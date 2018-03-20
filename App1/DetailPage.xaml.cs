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
using App1.Models;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailPage : Page
    {
        public News news;

        public DetailPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is News)
            {
                this.news = e.Parameter as News;
                int i = 0;
                foreach(Tuple<int, Tuple<string, string>> t in news._paragraph.sentences)
                {
                    switch (t.Item1)
                    {
                        case 1:
                            TextBlock tb = new TextBlock();
                            tb.TextWrapping = TextWrapping.Wrap;
                            tb.Text = t.Item2.Item2;
                            tb.Margin = new Thickness(0, 5, 0, 5);
                            ScrollStackPanel.Children.Insert(i++, tb);
                            break;
                        case 2:
                            break;
                        case 3:
                            Image img = new Image();
                            BitmapImage bitmapImage = new BitmapImage();
                            Uri uri = new Uri(t.Item2.Item1);
                            bitmapImage.UriSource = uri;
                            img.Source = bitmapImage;
                            img.Width = 500;
                            img.Margin = new Thickness(20);
                            img.HorizontalAlignment = HorizontalAlignment.Left;
                            ScrollStackPanel.Children.Insert(i++, img);
                            break;
                        default:
                            break;
                     

                    }
                }
            }
            else
            {
                string placeholder = "";
                this.news = new News(placeholder, placeholder, placeholder, placeholder, placeholder, placeholder, placeholder);
            }

            ConnectedAnimation animation1 = ConnectedAnimationService.GetForCurrentView().GetAnimation("animation1");
            if(animation1 != null)
            {
                animation1.TryStart(DetailTopPanel);
            }
            

        }


        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);
            ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("animation2", DetailTopPanel);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(UpdatePage));
        }
    }
}
