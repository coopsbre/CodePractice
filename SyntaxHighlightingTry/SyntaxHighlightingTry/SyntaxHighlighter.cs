using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyntaxHighlightingTry
{
    public class SyntaxHighlighter
    {
        public string _CurrentLineText { get; set; }
        public int _StartPosition { get; set; }
        public int _EndPosition { get; set; }
        public int _Length { get; set; } 
        public Line _LineClass { get; set; }
        public Dictionary<int, SyntaxHighlightItem> _LanguageRules { get; set; }
        public string _FoundString { get; set; } 

        public void ColourRichTextBoxLine(System.Windows.Forms.RichTextBox richTextBox, Dictionary<int,SyntaxHighlightItem> languageRules)
        {
            this._LanguageRules = languageRules; 

            _CurrentLineText = richTextBox.Text;

            string[] testArray = _CurrentLineText.Split(' ');

            //Line line = new Line();
            // line.PopulateLineCollection(pattern); 
            _LineClass = new Line();
            _LineClass._lineCollection = new Dictionary<int, List<LineItem>>();
            _LineClass._lineItemCollection = new List<LineItem>();

            //Lets go through the pattern. 
            TraversePattern(testArray);

            if (_LineClass._lineItemCollection.Count > 0)
            {
                foreach (LineItem lineItem in _LineClass._lineItemCollection)
                {

                    //AppendText(richTextBox, lineItem._ForeColor, lineItem._Text); 
                    
                    richTextBox.Select(lineItem._StartPosition, lineItem._Length); 
                    richTextBox.SelectionColor = lineItem._ForeColor; 
                    richTextBox.SelectionFont = lineItem._Font;


                   // richTextBox.SelectionLength = 0;
                }
                richTextBox.SelectionStart = richTextBox.Text.Length; 
            }
            
        }

        private void TraversePattern(string[] arrayOfText)
        {
            //Need to traverse through each string in the word.
            foreach (string text in arrayOfText)
            {
                //if the beginning of the item is a ' special case. 
                //With this special case need to make all characters following it be green.
                if (text.Contains("'") == true)
                {
                    int indexOfPunctuation = text.IndexOf("'");

                    string characterstobegreenerised = text.Substring(indexOfPunctuation);

                  // var lineItems = _LanguageRules.Where(item => item.Value._LanguageCategory == LanguageCategories.Comment);

                   foreach (var item in _LanguageRules)
                   {
                       if (item.Value._LanguageCategory == LanguageCategories.Comment)
                       {
                           LineItem lineItem = new LineItem();
                           lineItem._StartPosition = indexOfPunctuation;
                           lineItem._Text = characterstobegreenerised;
                           lineItem._Length = text.Length - characterstobegreenerised.Length;
                           lineItem._ForeColor = item.Value._ForeColor;
                           lineItem._Font = item.Value._Font;
                           _LineClass._lineItemCollection.Add(lineItem);
                           break;
                       }

                   }
                   MessageBox.Show("Test");
                }
                else
                {
                    foreach (var item in _LanguageRules)
                    {
                        string pattern = string.Empty;
                        string subject = string.Empty;

                        if (item.Value._Subject.Length == 0)
                            subject = text;
                        else if (item.Value._Subject.Length > 0)
                            subject = item.Value._Subject;

                        if (item.Value._Pattern.Length == 0)
                        {
                            if (text == "'")
                            {
                                pattern = text;
                            }
                            else if (text != "'")
                            {
                                //pattern = text; 
                                pattern = "\\b" + text + "\\b";
                            }

                        }
                        else if (item.Value._Pattern.Length > 0)
                            pattern = item.Value._Pattern;

                        if (TestRegularExpressionMatch(pattern, subject, false) == true)
                        {
                            //This could be pattern/subject here.
                            TestRegularExpressionMatch(pattern, _CurrentLineText, true);
                            LineItem lineItem = new LineItem();
                            lineItem._StartPosition = this._StartPosition;
                            lineItem._Text = _FoundString;
                            lineItem._Length = this._Length;
                            lineItem._ForeColor = item.Value._ForeColor;
                            lineItem._Font = item.Value._Font;
                            _LineClass._lineItemCollection.Add(lineItem);
                            break;
                        }
                    }
                }

            }
        }

        private bool TestRegularExpressionMatch(string pattern, string subject, bool setStartAndEndPositions)
        {
            bool found = false;
            Regex regEx = new Regex(pattern);
            Match match = regEx.Match(subject);

            while (match.Success)
            {
                string startPosition = string.Empty;
                string length = string.Empty;

                startPosition = match.Index.ToString();
                if (setStartAndEndPositions)
                {
                    _StartPosition = match.Index;
                    _EndPosition = (match.Length) - 1 + _StartPosition;
                    _Length = match.Length;
                }
                length = match.Length.ToString();

                if (Convert.ToInt32(length) > 0)
                    _FoundString += subject.Substring(Convert.ToInt32(startPosition), Convert.ToInt32(length));

                found = match.Success;
                match = match.NextMatch();

                if (match.NextMatch().Success == false)
                    return found;
            }

            return match.Success;
        }
    }
}
