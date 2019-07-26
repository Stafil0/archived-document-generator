using System;

namespace DocumentGenerator.Document.Template.Element
{
    public class Date : BaseTextElement
    {
        public string value { get; set; }
        public string[] range { get; set; }

        public override Layout.Element.BaseElement Generate(Default defaultValues)
        {
            Layout.Element.Date element = new Layout.Element.Date();
            DateTime date = value != null ?
                Utils.GetDateFromString(value, defaultValues.templateDateFormat) :
                Utils.GetRandomDateInRange(range, defaultValues.templateDateFormat);
            element.value = String.Format(format, date);

            return element;
        }
    }
}
