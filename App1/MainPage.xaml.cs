using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace App1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(1050, 800);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }

        private void appNavi_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void appNavi_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                contentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;
                NavView_Navigate(item);
            }
            //MyLottieMain.AutoPlay = false;
            //MyLottieMain.Visibility = Visibility.Collapsed;
        }

        private void appNavi_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                contentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                // find NavigationViewItem with Content that equals InvokedItem
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavView_Navigate(item as NavigationViewItem);

            }
            MyLottieMain.AutoPlay = false;
            MyLottieMain.Visibility = Visibility.Collapsed;
        }


        private void NavView_Navigate(NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "update":
                    contentFrame.Navigate(typeof(UpdatePage));
                    break;

                case "defender":
                    contentFrame.Navigate(typeof(DefenderPage));
                    break;

                case "backup":
                    contentFrame.Navigate(typeof(BackupPage));
                    break;

                case "trouble":
                    contentFrame.Navigate(typeof(TroubleshootPage));
                    break;
            }
        }
    }
}
