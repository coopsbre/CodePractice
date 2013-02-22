using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxHighlightingTry
{
    public class Line
    {
        public Dictionary<int, List<LineItem>> _lineCollection { get; set; }
        public List<LineItem> _lineItemCollection { get; set; } 
    }
}
