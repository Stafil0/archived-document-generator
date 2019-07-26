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

        protected virtual Layout.Element.BaseElement Generate(Layout.Element.BaseElement baseElement, Default defaultValues)
        {
            baseElement.type = type;
            baseElement.location = location;
            baseElement.alignmentX = Utils.GetRandomStringFromArray(alignmentX ??
                defaultValues.alignmentX);
            baseElement.alignmentY = Utils.GetRandomStringFromArray(alignmentY ??
                defaultValues.alignmentY);

            return baseElement;
        }
    }
}
