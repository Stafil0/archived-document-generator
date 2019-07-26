using System;

namespace DocumentGenerator.Document.Template.Element
{
    public class Date : BaseTextElement
    {
        public string value { get; set; }
        public string[] range { get; set; }

        public override Layout.Element.BaseElement Generate(Default defaultValues)
        {
            Layout.Element.Date date = (Layout.Element.Date)base.Generate(new Layout.Element.Date(), defaultValues);
            DateTime dateTime = value != null ?
                Utils.GetDateFromString(value, defaultValues.templateDateFormat) :
                Utils.GetRandomDateInRange(range, defaultValues.templateDateFormat);
            date.value = String.Format(format, dateTime);

            return date;
        }
    }
}
