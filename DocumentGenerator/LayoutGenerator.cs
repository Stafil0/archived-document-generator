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
                Document.Layout.Element.BaseElement layoutElement;

                if (templateElement.GetType() != typeof(Document.Template.Element.Image))
                {
                    layoutElement = templateElement.Generate(templateDocument.defaultValues);
                    string fontName = ((Document.Template.Element.BaseTextElement)templateElement).font != null ?
                        Utils.GetRandomStringFromArray(((Document.Template.Element.BaseTextElement)templateElement).font.fontFamily) : null;
                    ((Document.Layout.Element.BaseTextElement)layoutElement).font =
                        Utils.GetRandomFontFromResources(
                            (fontName != null && Utils.FontExists(fontName)) ?
                                ((Document.Template.Element.BaseTextElement)templateElement).font :
                                templateDocument.defaultValues.font
                        );
                    layoutElement.rotation = Utils.GetRandomTextRotationInRange(templateElement.rotation);
                }
                else
                {
                    layoutElement = templateElement.Generate(templateDocument.defaultValues);
                    layoutElement.rotation = Utils.GetRandomIntInRange(templateElement.rotation);
                }

                layoutElement.type = templateElement.type;
                layoutElement.location = templateElement.location;
                layoutElement.alignmentX = Utils.GetRandomStringFromArray(templateElement.alignmentX ?? 
                    templateDocument.defaultValues.alignmentX);
                layoutElement.alignmentY = Utils.GetRandomStringFromArray(templateElement.alignmentY ??
                    templateDocument.defaultValues.alignmentY);
                layoutElement.shiftX = Utils.GetRandomIntInRange(templateElement.shiftX);
                layoutElement.shiftY = Utils.GetRandomIntInRange(templateElement.shiftY);
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
