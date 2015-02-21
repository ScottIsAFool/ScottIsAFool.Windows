using System;

namespace ScottIsAFool.Windows.Core.Attributes
{
    public class Description : Attribute
    {
        public string Text;

        public Description(string text)
        {
            Text = text;
        }
    }
}