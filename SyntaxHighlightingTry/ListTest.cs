using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; 

namespace SyntaxHighlightingTry
{
    public class ListTest
    {
        
        public ListTest(string value, Image img) { Str = value; Image = img; }
        public string Str { get; set; }
        public Image Image { get; set; }
        
    }
}
