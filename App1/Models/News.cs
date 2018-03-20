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
        public string _summary { set; get; }
        public string _author { set; get; }
        public Paragraph _paragraph { set; get; }

        public News(string title, string link, string time, string description, string img_url, string summary, string author)
        {
            _title = title;
            _link = link;
            _time = time;
            _description = description;
            try
            {
                if(!img_url.Equals(""))
                    _first_pic_url = img_url;
                else _first_pic_url = "ms-appx:///Assets/itnews.jpg";
            } catch (NullReferenceException) {
                
                _first_pic_url = "ms-appx:///Assets/itnews.jpg";
            }
            _summary = summary;
            _author = author;
            NewsParser p = new NewsParser();
            _paragraph = p.parse(_description);
        }
    }
}
