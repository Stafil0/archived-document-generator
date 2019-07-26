using System;

namespace DocumentGenerator.Document.Layout.Element
{
    public class Number : BaseTextElement
    {
        public int value { get; set; }

        public override string GetValue()
        {
            return value.ToString();
        }
    }
}
