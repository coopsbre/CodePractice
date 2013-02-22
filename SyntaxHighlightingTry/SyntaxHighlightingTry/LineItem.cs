using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxHighlightingTry
{
    
        public class LineItem
        {
            public string _Text { get; set; }
            public int _StartPosition { get; set; }
            public int _EndPostion { get; set; }
            public int _Length { get; set; }
            public Font _Font { get; set; }
            public Color _ForeColor { get; set; }
        }
    
}
