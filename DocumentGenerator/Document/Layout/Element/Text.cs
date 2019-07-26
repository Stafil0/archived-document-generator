namespace DocumentGenerator.Document.Layout.Element
{
    public class Text : BaseTextElement
    {
        public string value { get; set; }

        public override string GetValue()
        {
            return value;
        }
    }
}
