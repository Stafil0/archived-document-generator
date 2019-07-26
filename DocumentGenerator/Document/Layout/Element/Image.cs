namespace DocumentGenerator.Document.Layout.Element
{
    public class Image : BaseElement
    {
        public string path { get; set; }

        public override string GetValue()
        {
            return path;
        }
    }
}
