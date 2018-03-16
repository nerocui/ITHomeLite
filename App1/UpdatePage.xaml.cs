using App1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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

        public UpdatePage()
        {
            this.InitializeComponent();
            //RefreshPage();
            NewsList = new ObservableCollection<News>();
        }
        
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MyProgressRing.IsActive = true;

            MyProgressRing.Visibility = Visibility.Visible;


            //StartWebRequest();
            //ObservableCollection<News> results = new ObservableCollection<News>();

            request = (HttpWebRequest)WebRequest.Create("https://www.ithome.com/rss/");
            request.UserAgent = @"Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1667.0 Safari/537.36";
            var response = (HttpWebResponse)await Task.Factory
                            .FromAsync<WebResponse>(request.BeginGetResponse,
                            request.EndGetResponse,
                            null);
            Debug.Assert(response.StatusCode == HttpStatusCode.OK);
            Stream receiveStream = response.GetResponseStream();



            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string temp = readStream.ReadToEnd();


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(temp);
            XmlNodeList nodeList;
            XmlNode root = doc.DocumentElement;
            nodeList = root.SelectNodes("//item");

            foreach (XmlNode item in nodeList)
            {

                string title = item.SelectSingleNode("title").InnerXml;
                string link = item.SelectSingleNode("link").InnerXml;
                string time = item.SelectSingleNode("pubDate").InnerXml;
                string description = item.SelectSingleNode("description").InnerXml;
                News news = new News(title, link, time, description);
                NewsList.Add(news);
            }

            
            MyProgressRing.IsActive = false;

            MyProgressRing.Visibility = Visibility.Collapsed;



        }


        public void StartWebRequest()
        {
            request = (HttpWebRequest)WebRequest.Create("https://www.ithome.com/rss/");
            request.UserAgent = @"Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1667.0 Safari/537.36";

            request.BeginGetResponse(new AsyncCallback(FinishWebRequest), null);
        }

        public void FinishWebRequest(IAsyncResult result)
        {
            ObservableCollection<News> results = new ObservableCollection<News>();

            HttpWebResponse response = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;
            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string temp = readStream.ReadToEnd();


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(temp);
            XmlNodeList nodeList;
            XmlNode root = doc.DocumentElement;
            nodeList = root.SelectNodes("//item");

            foreach (XmlNode item in nodeList)
            {

                string title = item.SelectSingleNode("title").InnerXml;
                string link = item.SelectSingleNode("link").InnerXml;
                string time = item.SelectSingleNode("pubDate").InnerXml;
                string description = item.SelectSingleNode("description").InnerXml;
                News news = new News(title, link, time, description);
                results.Add(news);
            }
            NewsList = results;
        }

        public void RefreshPage()
        {

            //StartWebRequest();
            /*
            ObservableCollection<News> result = new ObservableCollection<News>();

            request = (HttpWebRequest)WebRequest.Create("https://www.ithome.com/rss/");
            request.UserAgent = @"Mozilla/5.0 (Windows NT 6.2; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/32.0.1667.0 Safari/537.36";
            WebResponse response = request.GetResponse();
            // Get the stream associated with the response.
            Stream receiveStream = response.GetResponseStream();

            // Pipes the stream to a higher level stream reader with the required encoding format. 
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            string temp = readStream.ReadToEnd();


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(temp);
            XmlNodeList nodeList;
            XmlNode root = doc.DocumentElement;
            nodeList = root.SelectNodes("//item");

            foreach (XmlNode item in nodeList)
            {
                
                string title = item.SelectSingleNode("title").InnerXml;
                string link = item.SelectSingleNode("link").InnerXml;
                string time = item.SelectSingleNode("pubDate").InnerXml;
                string description = item.SelectSingleNode("description").InnerXml;
                News news = new News(title, link, time, description);
                result.Add(news);
            }
            NewsList = result;
            */
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void AdaptiveGridViewControl_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
