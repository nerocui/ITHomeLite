using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models
{
    public class News
    {
        public string _title { set; get; }
        public string _link { set; get; }
        public string _time { set; get; }
        public string _description { set; get; }
        public string _first_pic_url { set; get; }
        

        public News(string title, string link, string time, string description)
        {
            _title = title;
            _link = link;
            _time = time;
            int len = description.Length-12;
            string temp = description.Substring(9, len);
            _description = temp;
            
            try
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(temp);
                var items = doc.DocumentNode.Descendants();
                foreach (var de in items)
                {
                    if (de.Name.Equals("img") && !de.Attributes["src"].Value.ToString().Equals(""))
                    {
                        _first_pic_url = de.Attributes["src"].Value.ToString();
                        break;
                    }
                    else
                    {
                        _first_pic_url = "ms-appx:///Assets/itnews.jpg";

                    }

                }
            }
            catch (Exception)
            {
                _first_pic_url = "ms-appx:///Assets/itnews.jpg";
            }
        }
    }
}
