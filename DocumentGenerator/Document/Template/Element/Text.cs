using System;

namespace DocumentGenerator.Document.Template.Element
{
    public class Text : BaseTextElement
    {
        public string[] value { get; set; }
        public string path { get; set; }

        public override Layout.Element.BaseElement Generate(Default defaultValues)
        {
            Layout.Element.Text text = new Layout.Element.Text();
            string textValue = value != null ?
                Utils.GetRandomStringFromArray(value) :
                Utils.GetRandomValueFromFile(path);
            text.value = String.Format(format, textValue);

            return text;
        }
    }
}
