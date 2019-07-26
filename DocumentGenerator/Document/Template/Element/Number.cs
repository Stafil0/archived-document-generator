using System;

namespace DocumentGenerator.Document.Template.Element
{
    public class Number : BaseTextElement
    {
        public int value { get; set; }
        public int[] range { get; set; }

        public override Layout.Element.BaseElement Generate(Default defaultValues)
        {
            Layout.Element.Number number = 
                (Layout.Element.Number)base.Generate(new Layout.Element.Number(), defaultValues);
            number.value = value != 0 ? value : Utils.GetRandomIntInRange(range);

            return number;
        }
    }
}
