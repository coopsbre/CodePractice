using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyntaxHighlightingTry
{
        public class SyntaxHighlightItem
        {
            public LanguageCategories _LanguageCategory { get; set; }
            public string _Subject { get; set; }
            public string _Pattern { get; set; }
            public Font _Font { get; set; }
            public Color _ForeColor { get; set; }

        }

        public enum LanguageCategories
        {
            KeyWord,
            Operator,
            Comparison,
            Logical,
            Number,
            Comment,
            Word
        }
    
}
