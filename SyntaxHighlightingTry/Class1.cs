using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace SyntaxHighlightingTry
{
    public class MyListBox : ListBox 
    {
        public MyListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;
            
        }

        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            MessageBox.Show("Test");
        }
    }


}
