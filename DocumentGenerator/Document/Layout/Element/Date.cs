namespace DocumentGenerator.Document.Layout.Element
{
    public class Date : BaseTextElement
    {
        public string value { get; set; }

        public override string GetValue()
        {
            return value;
        }
    }
}
