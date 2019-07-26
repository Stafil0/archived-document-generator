using DocumentGenerator.Document.Layout.Element;
using System.Collections.Generic;

namespace DocumentGenerator.Document.Layout
{
    public class LayoutDocument : Shift
    {
        public List<BaseElement> elements { get; set; }
        public string background { get; set; }
        public Size size { get; set; }
    }
}
