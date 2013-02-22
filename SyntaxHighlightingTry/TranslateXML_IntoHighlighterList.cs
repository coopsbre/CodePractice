using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SyntaxHighlightingTry
{
    public class TranslateXML_IntoHighlighterList
    {
          public Dictionary<int,SyntaxHighlightItem> _LanguageRuleList{get;set;} 

        public TranslateXML_IntoHighlighterList()
        {
            _LanguageRuleList = new Dictionary<int, SyntaxHighlightItem>(); 
        }

        
        public void TranslateXMLFile(string XMLFile)
        {
            XDocument xDoc = XDocument.Load(XMLFile);
            //Look for things under the "Lanuage Section".
            XElement element = xDoc.Descendants("Language").First();

            string elementName = string.Empty; 


            foreach (XElement xElement in element.Elements())
            {
                elementName = xElement.Name.ToString(); 

               // MessageBox.Show(xElement.Name.ToString());
                //First thing we are looking for is Categories 
                if (elementName == "Categories")
                {

                    TraverseSubElements(xElement); 
                }
                
               
            }
        }

        public void TraverseSubElements(XElement xElement)
        {
            SyntaxHighlightItem syntaxHighligherItem = null;
            string nodeName = string.Empty; 
            int dictionaryNumber = 0; 

            if (xElement.HasElements == true)
            {
               // MessageBox.Show(xElement.Name.ToString());
                foreach (XElement nextElement in xElement.Elements())
                {
                    dictionaryNumber += 1; 
                    syntaxHighligherItem = new SyntaxHighlightItem();
                    syntaxHighligherItem._LanguageCategory = this.SetLanguageCategory(nextElement.Name.ToString()); 
                    
                    if (nextElement.HasElements == true)
                    {
                        foreach (XElement childElement in nextElement.Elements())
                        {
                            if (childElement.Name.ToString() == "Subject")
                                syntaxHighligherItem._Subject = childElement.Value.Replace("\n","").Replace("\t","");

                            if (childElement.Name.ToString() == "Pattern")
                                syntaxHighligherItem._Pattern = childElement.Value.Replace("\n", "").Replace("\t", "");

                            if (childElement.HasElements == true)
                                TraverseGrandKids(childElement, syntaxHighligherItem); 

                        }
                    }
                    
                    //Add to the dictionary here.
                    _LanguageRuleList.Add(dictionaryNumber, syntaxHighligherItem); 
                }

            }

            //MessageBox.Show("Test");
        }

        private void TraverseGrandKids(XElement childElement, SyntaxHighlightItem syntaxHighlightItem)
        {
            string fontName = string.Empty;
            string foreColor = string.Empty; 

            float fontSize = 0f;
            int fs = 0;

            if (childElement.HasElements == true)
            {
                string childElementName = string.Empty;
                childElementName = childElement.Name.ToString(); 

                foreach (XElement xElement in childElement.Elements()) 
                {
                    string elementName = string.Empty;
                    elementName = xElement.Name.ToString();

                    if (elementName == "FontName")
                        fontName = xElement.Value;

                    if (elementName == "FontSize")
                        fs = Convert.ToInt32(xElement.Value);
                        fontSize = (float)fs;

                    if (elementName == "ForeColor")
                        foreColor = xElement.Value;
                }

                if (childElementName.Contains("Font"))
                    syntaxHighlightItem._Font = new System.Drawing.Font(fontName, fontSize); 

                if (childElementName.Contains("Color"))
                    syntaxHighlightItem._ForeColor = System.Drawing.ColorTranslator.FromHtml(foreColor);  
            }
        }

        private LanguageCategories SetLanguageCategory(string categoryName)
        {
            switch (categoryName)
            {
                case "KeyWords":
                    return LanguageCategories.KeyWord;

                case "Operators":
                    return LanguageCategories.Operator;

                case "Number":
                    return LanguageCategories.Number; 
                
                case "Comment":
                    return LanguageCategories.Comment; 

                default: return LanguageCategories.KeyWord;
            }

            return LanguageCategories.KeyWord;
        }


    }
}
