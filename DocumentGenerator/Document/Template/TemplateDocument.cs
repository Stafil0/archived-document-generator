using DocumentGenerator.Document.Template.Element;
using System.Collections.Generic;

namespace DocumentGenerator.Document.Template
{
    public class TemplateDocument : Shift
    {
        public List<BaseElement> elements { get; set; }
        public Default defaultValues { get; set; }
        public string background { get; set; }
        public Size size { get; set; }
    }
}
