using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Layout
{
     class LayoutGenerator
    {
        private Document.DocumentLayout document;
        private Document.DocumentTemplate documentTemplate;
        private Random rand;
        private Encoding encoding;
        private CultureInfo dateCulture;

        public LayoutGenerator(string jsonPath, Encoding encoding)
        {
            this.encoding = encoding;
            document = InitDocumentLayout();
            rand = new Random();
            dateCulture = CultureInfo.InvariantCulture;
            JsonParser.GetObject<Document.DocumentTemplate>(jsonPath, encoding, out documentTemplate);
        }

        public void GenerateLayout()
        {
            document.size = documentTemplate.size;
            document.background = documentTemplate.background;
            document.rotation = documentTemplate.rotation != null ? GetRandomIntInRange(documentTemplate.rotation) : 0;
            document.shiftX = GetRandomIntInRange(documentTemplate.shiftX);
            document.shiftY = GetRandomIntInRange(documentTemplate.shiftY);

            foreach (Document.DocumentTemplate.LayoutTemplate layoutTemplate in documentTemplate.layout)
            {
                Document.DocumentLayout.Layout layout = new Document.DocumentLayout.Layout();
                layout.type = layoutTemplate.type;
                layout.location = layoutTemplate.location;

                if (layout.type != Document.Type.image.ToString())
                {
                    if (layout.type == Document.Type.text.ToString())
                    {
                        string format = layoutTemplate.format ?? documentTemplate.textDefault.format;
                        string value =
                            (layoutTemplate.value ?? GetRandomValueFromFile(layoutTemplate.valuePath)) ??
                            (documentTemplate.textDefault.value ?? GetRandomValueFromFile(documentTemplate.textDefault.valuePath));
                        layout.value = String.Format(format, value);
                    }
                    else
                    {
                        string format = layoutTemplate.format ?? documentTemplate.dateDefault.format;
                        string value = 
                            (
                                layoutTemplate.value ??
                                GetRandomDateInRange(layoutTemplate.dateRange)
                            ) ??
                            (
                                documentTemplate.dateDefault.value ??
                                GetRandomDateInRange(documentTemplate.dateDefault.dateRange)
                            );
                        layout.value = String.Format(format, GetDateFromString(value));
                    }

                    layout.alignmentX = GetRandomStringFromArray(
                        layoutTemplate.alignmentX ??
                        (layout.type == Document.Type.text.ToString() ? 
                            documentTemplate.textDefault.alignmentX : documentTemplate.dateDefault.alignmentY)
                    );
                    layout.alignmentY = GetRandomStringFromArray(
                        layoutTemplate.alignmentY ??
                        (layout.type == Document.Type.text.ToString() ? 
                            documentTemplate.textDefault.alignmentY : documentTemplate.dateDefault.alignmentY)
                    );
                    layout.font = GetRandomFontFromResources(
                        layoutTemplate.font ?? 
                        (layout.type == Document.Type.text.ToString() ? 
                            documentTemplate.textDefault.font : documentTemplate.dateDefault.font)
                    );
                    layout.rotation = GetRandomTextRotationInRange(
                        layoutTemplate.rotation ??
                        (layout.type == Document.Type.text.ToString() ? 
                            documentTemplate.textDefault.rotation : documentTemplate.dateDefault.rotation)
                    );
                    layout.shiftX = GetRandomIntInRange(
                        layoutTemplate.shiftX ??
                        (layout.type == Document.Type.text.ToString() ? 
                            documentTemplate.textDefault.shiftX : documentTemplate.dateDefault.shiftX)
                    );
                    layout.shiftY = GetRandomIntInRange(
                        layoutTemplate.shiftY ??
                        (layout.type == Document.Type.text.ToString() ? 
                            documentTemplate.textDefault.shiftY : documentTemplate.dateDefault.shiftY)
                    );
                }
                else
                {
                    layout.alignmentX = GetRandomStringFromArray(layoutTemplate.alignmentX ?? documentTemplate.imageDefault.alignmentX);
                    layout.alignmentY = GetRandomStringFromArray(layoutTemplate.alignmentY ?? documentTemplate.imageDefault.alignmentY);
                    layout.rotation = GetRandomIntInRange(layoutTemplate.rotation ?? documentTemplate.imageDefault.rotation);
                    layout.value =
                        (layoutTemplate.value ?? GetRandomValueFromFile(layoutTemplate.valuePath)) ??
                        (documentTemplate.imageDefault.value ?? GetRandomValueFromFile(documentTemplate.imageDefault.valuePath));
                    layout.shiftX = GetRandomIntInRange(layoutTemplate.shiftX ?? documentTemplate.imageDefault.shiftX);
                    layout.shiftY = GetRandomIntInRange(layoutTemplate.shiftY ?? documentTemplate.imageDefault.shiftY);
                }
                document.layout.Add(layout);
            }
        }

        public void AddElementToLayout(string jsonPath, Encoding encoding)
        {
            Document.DocumentLayout documentElements = InitDocumentLayout();
            JsonParser.GetObject<Document.DocumentLayout>(jsonPath, encoding, out documentElements);
            List<Document.DocumentLayout.Layout> elements = new List<Document.DocumentLayout.Layout>();
            foreach (Document.DocumentLayout.Layout element in elements)
            {
                document.layout.Add(element);
            }
        }

        public void SaveLayout(string savePath, Encoding encoding)
        {
            JsonParser.SaveObject(document, savePath, encoding);
        }

        private Document.DocumentLayout InitDocumentLayout()
        {
            Document.DocumentLayout documentLayout = new Document.DocumentLayout();
            documentLayout.layout = new List<Document.DocumentLayout.Layout>();
            documentLayout.size = new Document.Size();
            return documentLayout;
        }

        private Document.DocumentLayout.FontLayout GetRandomFontFromResources(Document.DocumentTemplate.FontTemplate fontRange)
        {
            Document.DocumentLayout.FontLayout font = new Document.DocumentLayout.FontLayout();
            font.fontFamily = GetRandomStringFromArray(fontRange.fontFamily);
            font.fontColor = GetRandomStringFromArray(fontRange.fontColor);
            font.fontSize = GetRandomIntInRange(fontRange.fontSize);
            return font;
        }

        private string GetRandomDateInRange(string[] range)
        {
            DateTime startDate = GetDateFromString(range[0]);
            DateTime endDate = GetDateFromString(range[1]);
            int daysRange = (endDate - startDate).Days;
            return startDate.AddDays(rand.NextDouble() * daysRange).ToString(documentTemplate.layoutDateFormat);
        }

        private DateTime GetDateFromString(string date)
        {
            return DateTime.ParseExact(date, documentTemplate.layoutDateFormat, dateCulture);
        }

        private string GetRandomStringFromArray(string[] arr)
        {
            return arr[rand.Next(0, arr.Length)];
        }

        private string GetRandomValueFromFile(string path)
        {
            return GetRandomStringFromArray(JsonParser.GetArrayFromJson(path, encoding));
        }

        private int GetRandomIntInRange(int[] range)
        {
            return rand.Next(range[0], range[1] + 1);
        }

        private int GetRandomTextRotationInRange(int[] range)
        {
            return GetRandomIntInRange(new int[] { range[0] / 90, range[1] / 90 }) * 90;
        }
    }
}
