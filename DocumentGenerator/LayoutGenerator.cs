using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentGenerator.Document.Layout;
using DocumentGenerator.Document.Template;

namespace DocumentGenerator
{
    public class LayoutGenerator
    {
        private LayoutDocument layoutDocument;
        private TemplateDocument templateDocument;
        private Encoding encoding;

        public LayoutGenerator(string jsonPath, Encoding encoding)
        {
            this.encoding = encoding;
            layoutDocument = InitLayoutDocument();
            templateDocument = Json.Parser.GetTemplateDocument(jsonPath, encoding);
        }

        private LayoutDocument InitLayoutDocument()
        {
            LayoutDocument document = new LayoutDocument();
            document.elements = new List<Document.Layout.Element.BaseElement>();
            return document;
        }

        public void GenerateLayout()
        {
            layoutDocument.size = templateDocument.size;
            layoutDocument.background = templateDocument.background != "" ?
                Utils.GetRandomStringFromArray(Directory.GetFiles(templateDocument.background)) : "";
            layoutDocument.rotation = Utils.GetRandomIntInRange(templateDocument.rotation);
            layoutDocument.shiftX = Utils.GetRandomIntInRange(templateDocument.shiftX);
            layoutDocument.shiftY = Utils.GetRandomIntInRange(templateDocument.shiftY);

            foreach (Document.Template.Element.BaseElement templateElement in templateDocument.elements)
            {
                Document.Layout.Element.BaseElement layoutElement = templateElement.Generate(templateDocument.defaultValues);
                layoutDocument.elements.Add(layoutElement);
            }
        }

        public void SaveLayout(string savePath, Encoding encoding)
        {
            Json.Parser.SaveObject(layoutDocument, savePath, encoding);
        }

        public void AddElementToLayout(string jsonPath, Encoding encoding)
        {
            LayoutDocument document = new LayoutDocument();
            document.elements = new List<Document.Layout.Element.BaseElement>();
            document = Json.Parser.GetLayoutDocument(jsonPath, encoding);
            foreach (Document.Layout.Element.BaseElement element in document.elements)
            {
                layoutDocument.elements.Add(element);
            }
        }
    }
}
