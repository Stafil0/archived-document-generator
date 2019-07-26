using DocumentGenerator.Document.Element;

namespace DocumentGenerator.Document.Template.Element
{
    public abstract class BaseElement : Shift
    {
        public string type { get; set; }
        public Location location { get; set; }
        public string[] alignmentX { get; set; }
        public string[] alignmentY { get; set; }

        public abstract Layout.Element.BaseElement Generate(Default defaultValues);
    }
}
