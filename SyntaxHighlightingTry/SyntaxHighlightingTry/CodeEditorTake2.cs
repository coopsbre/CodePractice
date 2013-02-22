using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions; 

namespace SyntaxHighlightingTry
{
    public partial class CodeEditorTake2 : UserControl
    {
        private MyListBox listBoxAutoComplete = new MyListBox();
        public Dictionary<int, SyntaxHighlightItem> dRules { get; set; }
        public SyntaxHighlighter _SyntaxHighligher { get; set; } 

         public CodeEditorTake2()
        {
            InitializeComponent();
            ConvertXMLFileInto();
            _SyntaxHighligher = new SyntaxHighlighter(); 
        }

         public void ConvertXMLFileInto()
         {
             TranslateXML_IntoHighlighterList translator = new TranslateXML_IntoHighlighterList();
             translator.TranslateXMLFile(@"F:\CAREER\C#\CodeEditor\SyntaxHighlighterTry\SyntaxHighlighterTry\VBKeyWords.xml");
             dRules = translator._LanguageRuleList; 
         }


        private void updateNumberLabel()
        {
            //we get index of first visible char and number of first visible line
            Point pos = new Point(0, 0);

            
            int firstIndex = richTextBox1.GetCharIndexFromPosition(pos);
            int firstLine = richTextBox1.GetLineFromCharIndex(firstIndex);

            //now we get index of last visible char and number of last visible line
            pos.X = ClientRectangle.Width;
            pos.Y = ClientRectangle.Height;
            int lastIndex = richTextBox1.GetCharIndexFromPosition(pos);
            int lastLine = richTextBox1.GetLineFromCharIndex(lastIndex);

            //this is point position of last visible char, we'll use its Y value for calculating numberLabel size
            pos = richTextBox1.GetPositionFromCharIndex(lastIndex);

            
            int currentLineNumber = 0;
            //finally, renumber label
            numberLabel.Text = "";
            for (int i = firstLine; i <= lastLine + 1; i++)
            {
                currentLineNumber = i + 1; 
                numberLabel.Text += currentLineNumber + "\n";
                tsLineNumber.Text = currentLineNumber.ToString();
            }

        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

           // updateNumberLabel();

            RichTextBox rt = sender as RichTextBox; 

            //Set syntax colouring. 
            _SyntaxHighligher.ColourRichTextBoxLine(rt, dRules);

            //Use this but later on.
            bool laterOn = false;

            if (laterOn == true)
            {
                Point point = this.richTextBox1.GetPositionFromCharIndex(richTextBox1.SelectionStart);
                point.Y += (int)Math.Ceiling(this.richTextBox1.Font.GetHeight()) + 2;
                point.X += 2; // for Courier, may need a better method
                this.listBoxAutoComplete.Location = point;
                this.listBoxAutoComplete.DrawItem += listBoxAutoComplete_DrawItem;
                this.richTextBox1.Controls.Add(this.listBoxAutoComplete);
            }

        }

        void listBoxAutoComplete_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void richTextBox1_VScroll(object sender, EventArgs e)
        {
            //move location of numberLabel for amount of pixels caused by scrollbar
            //int d = richTextBox1.GetPositionFromCharIndex(0).Y % (richTextBox1.Font.Height + 1);
            //numberLabel.Location = new Point(0, d);

           // updateNumberLabel();
        }

        private void richTextBox1_Resize(object sender, EventArgs e)
        {
            //richTextBox1_VScroll(null, null);
        }

        private void richTextBox1_FontChanged(object sender, EventArgs e)
        {
           // updateNumberLabel();
           //richTextBox1_VScroll(null, null);
        }
    }
}
