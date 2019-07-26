using DocumentGenerator.Document.Element;

namespace DocumentGenerator.Document.Layout.Element
{
    public abstract class BaseElement : Shift
    {
        public string type { get; set; }
        public Location location { get; set; }
        public string alignmentX { get; set; }
        public string alignmentY { get; set; }

        public abstract string GetValue();
    }
}
