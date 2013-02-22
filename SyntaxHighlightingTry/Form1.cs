using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SyntaxHighlightingTry
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            CodeEditorTake2 codeEditor = new CodeEditorTake2();

            this.panel1.Controls.Add(codeEditor); 
            codeEditor.Dock = DockStyle.Fill;
            this.panel1.Dock = DockStyle.Fill;

        }

       
    }
}
