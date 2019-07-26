using System.IO;

namespace DocumentGenerator.Document.Template.Element
{
    public class Image : BaseElement
    {
        public string path { get; set; }

        public override Layout.Element.BaseElement Generate(Default defaultValues)
        {
            Layout.Element.Image image = (Layout.Element.Image)base.Generate(new Layout.Element.Image(), defaultValues);
            image.path = Utils.GetRandomStringFromArray(Directory.GetFiles(path));

            return image;
        }
    }
}
