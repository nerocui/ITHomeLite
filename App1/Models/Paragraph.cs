using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Models
{
    public class Paragraph
    {
        //1 for sentence, inner tuple second string
        //2 for hyper link, inner tuple first string
        //3 for picture url, inner tuple first string
        //for the inner tuple, the first string is link, the second string is text
        public List<Tuple<int, Tuple<string, string>>> sentences;
        public Paragraph()
        {
            sentences = new List<Tuple<int, Tuple<string, string>>>();
        }

    }
}
