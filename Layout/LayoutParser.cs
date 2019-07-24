using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

namespace Layout
{
    public  class LayoutParser
    {
        private Random rand = new Random();
        private Document.DocumentLayout documentLayout;
        private Bitmap targetDocument;
        private Graphics g;

        public LayoutParser(string jsonPath, Encoding encoding)
        {
            JsonParser.GetObject<Document.DocumentLayout>(jsonPath, encoding, out documentLayout);
            targetDocument = new Bitmap(documentLayout.size.width, documentLayout.size.height, PixelFormat.Format32bppArgb);
            g = Graphics.FromImage(targetDocument);
            if (documentLayout.background != null)
            {
                Image image = Image.FromFile(documentLayout.background);
                g.DrawImage(image, 0, 0, image.Width, image.Height);
            }
        }

        public void PrintDocument(string savePath)
        {
            foreach (Document.DocumentLayout.Layout layout in documentLayout.layout)
            {
                g = Graphics.FromImage(targetDocument);
                layout.location.left += layout.shiftX;
                layout.location.top += layout.shiftY;

                if (layout.type != Document.Type.image.ToString())
                {
                    DrawText(layout);
                }
                else
                {
                    DrawScaledImage(layout);
                }
            }
            targetDocument = RotateDocument(targetDocument);
            targetDocument.Save(savePath, ImageFormat.Png);
        }

        private void DrawText(Document.DocumentLayout.Layout layout)
        {
            StringFormat drawFormat = new StringFormat();
            Rectangle location = new Rectangle(layout.location.left, layout.location.top, layout.location.width, layout.location.height);
            Font font = new Font(layout.font.fontFamily, layout.font.fontSize);
            SolidBrush drawBrush = new SolidBrush(ColorTranslator.FromHtml(layout.font.fontColor));
            layout.location.left += layout.shiftX;
            layout.location.top += layout.shiftY;
            drawFormat.Alignment = GetStringAlignment((int)Enum.Parse(typeof(Document.HorizontalAlignment), layout.alignmentX, true));
            drawFormat.LineAlignment = GetStringAlignment((int)Enum.Parse(typeof(Document.VerticalAlignment), layout.alignmentY, true));
            switch (layout.rotation)
            {
                case 90:
                    g.RotateTransform(90);
                    g.TranslateTransform(
                        -(layout.location.left - layout.location.top), 
                        -(layout.location.left + layout.location.top + layout.location.width)
                    );
                    location = new Rectangle(location.Left, location.Top, location.Height, location.Width);
                    break;
                case 180:
                    g.RotateTransform(180);
                    g.TranslateTransform(
                        -(layout.location.left * 2 + layout.location.width), 
                        -(layout.location.height + layout.location.top * 2)
                    );
                    break;
                case 270:
                    g.RotateTransform(270);
                    g.TranslateTransform(
                        -(layout.location.left + layout.location.top + layout.location.height), 
                        (layout.location.left - layout.location.top)
                    );
                    location = new Rectangle(location.Left, location.Top, location.Height, location.Width);
                    break;
            }

            g.DrawString(layout.value, font, drawBrush, location, drawFormat);
        }

        private void DrawScaledImage(Document.DocumentLayout.Layout layout)
        {
            Document.HorizontalAlignment alignmentX = 
                (Document.HorizontalAlignment)Enum.Parse(typeof(Document.HorizontalAlignment), layout.alignmentX, true);
            Document.VerticalAlignment alignmentY = 
                (Document.VerticalAlignment)Enum.Parse(typeof(Document.VerticalAlignment), layout.alignmentY, true);
            Image image = Image.FromFile(layout.value);
            Document.Location scaledLocation = new Document.Location();
            scaledLocation.left = layout.location.left;
            scaledLocation.top = layout.location.top;
            if (layout.location.width > layout.location.height)
            {
                scaledLocation.width = (int)(image.Width * ((double)layout.location.height / image.Height));
                scaledLocation.height = layout.location.height;
            }
            else
            {
                scaledLocation.height = (int)(image.Height * ((double)layout.location.width / image.Width));
                scaledLocation.width = layout.location.width;
            }
            if ((alignmentX == Document.HorizontalAlignment.middle) && (scaledLocation.width < layout.location.width))
            {
                scaledLocation.left += (layout.location.width - scaledLocation.width) / 2;
            }
            else if ((alignmentX == Document.HorizontalAlignment.right) && (scaledLocation.width < layout.location.width))
            {
                scaledLocation.left += (layout.location.width - scaledLocation.width);
            }
            if ((alignmentY == Document.VerticalAlignment.middle) && (scaledLocation.height < layout.location.height))
            {
                scaledLocation.top += (layout.location.height - scaledLocation.height) / 2;
            }
            else if ((alignmentY == Document.VerticalAlignment.bottom) && (scaledLocation.height < layout.location.height))
            {
                scaledLocation.top += (layout.location.height - scaledLocation.height);
            }
            RotateObject(scaledLocation, layout.rotation);
            g.DrawImage(image, scaledLocation.left, scaledLocation.top, scaledLocation.width, scaledLocation.height);
        }

        private void RotateObject(Document.Location location, float angle)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Point center = new Point((location.left + location.width / 2), (location.top + location.height / 2));
            g.Transform = RotateAroundPoint(angle, center);
        }
        
        public StringAlignment GetStringAlignment(int alignment)
        {
            switch (alignment)
            {
                case 0:
                    return StringAlignment.Near;
                case 1:
                    return StringAlignment.Center;
                case 2:
                    return StringAlignment.Far;
                default:
                    return StringAlignment.Center;

            }
        }

        private Matrix RotateAroundPoint(float angle, Point center)
        {
            Matrix result = new Matrix();
            result.RotateAt(angle, center);
            return result;
        }

        private Bitmap RotateDocument(Bitmap currentDocument)
        {
            Bitmap document = new Bitmap(documentLayout.size.width, documentLayout.size.height, PixelFormat.Format32bppArgb);
            g = Graphics.FromImage(document);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Point center = new Point(documentLayout.size.width / 2, documentLayout.size.height / 2);
            g.Transform = RotateAroundPoint(documentLayout.rotation, center);
            g.DrawImage(currentDocument, documentLayout.shiftX, documentLayout.shiftY, documentLayout.size.width, documentLayout.size.height);
            return document;
        }
    }
}
