using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models
{
    class NewsParser
    {


        public Paragraph parse(string d)
        {
            Paragraph p = new Paragraph();
            string s = CutHeadTail(d);
            int len = s.Length - 1;
            string[] items = s.Split(new string[] { "</p><p>" }, StringSplitOptions.RemoveEmptyEntries);
            foreach(string item in items)
            {
                string temp = CutHeadTail(item);
                if (temp.Contains("<img"))
                {
                    string[] imgurl = temp.Split(new string[] { "src=\"" }, StringSplitOptions.RemoveEmptyEntries);
                    imgurl = imgurl[1].Split('\"');
                    temp = imgurl[0];
                    Tuple<string, string> t = new Tuple<string, string>(temp, "");
                    Tuple<int, Tuple<string, string>> entry = new Tuple<int, Tuple<string, string>>(3, t);
                    p.sentences.Add(entry);
                }
                else
                {
                    Tuple<string, string> t = new Tuple<string, string>("", temp);
                    Tuple<int, Tuple<string, string>> entry = new Tuple<int, Tuple<string, string>>(1, t);
                    p.sentences.Add(entry);
                }
            }



            return p;
        }


        public string CutHeadTail(string d)
        {
            int len = d.Length - 1;
            if (d[0].Equals('<') && d[2].Equals('>')
                && d[len].Equals('>') && d[len - 2].Equals('/')
                && d[len - 3].Equals('<'))
            {
                return d.Substring(3, len - 6);
            }//ToDo after debug, remove the indicator
            else return d;
        }
    }
}
