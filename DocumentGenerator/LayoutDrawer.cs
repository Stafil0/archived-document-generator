using DocumentGenerator.Document.Element;
using DocumentGenerator.Document.Layout;
using DocumentGenerator.Document.Layout.Element;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

namespace DocumentGenerator
{
    class LayoutDrawer
    {
        private Random rand = new Random();
        private LayoutDocument document;
        private Bitmap targetDocument;
        private Graphics g;

        public LayoutDrawer(string jsonPath, Encoding encoding)
        {
            document = Json.Parser.GetLayoutDocument(jsonPath, encoding);
            targetDocument = new Bitmap(document.size.width, document.size.height, PixelFormat.Format32bppArgb);
            g = Graphics.FromImage(targetDocument);
            if (document.background != "")
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(document.background);
                g.DrawImage(image, 0, 0, image.Width, image.Height);
            }
        }

        public void PrintDocument(string savePath)
        {
            foreach (BaseElement element in document.elements)
            {
                if (element.GetType() != typeof(Document.Layout.Element.Image))
                {
                    DrawText(element);
                }
                else
                {
                    DrawScaledImage(element);
                }
            }
            targetDocument = RotateDocument(targetDocument);
            targetDocument.Save(savePath, ImageFormat.Png);
        }

        private void DrawText(BaseElement element)
        {
            g = Graphics.FromImage(targetDocument);
            BaseTextElement textElement = (BaseTextElement)element;
            StringFormat drawFormat = new StringFormat();
            Rectangle shiftLocation = new Rectangle(
                textElement.location.left + textElement.shiftX,
                textElement.location.top + textElement.shiftY,
                textElement.location.width,
                textElement.location.height
            );
            System.Drawing.Font font = new System.Drawing.Font(textElement.font.fontFamily, textElement.font.fontSize);
            SolidBrush drawBrush = new SolidBrush(ColorTranslator.FromHtml(textElement.font.fontColor));
            drawFormat.Alignment = GetStringAlignment((int)Enum.Parse(typeof(Alignment.Horizontal), element.alignmentX, true));
            drawFormat.LineAlignment = GetStringAlignment((int)Enum.Parse(typeof(Alignment.Vertical), element.alignmentY, true));
            switch (element.rotation)
            {
                case 90:
                    g.RotateTransform(90);
                    g.TranslateTransform(
                        -(shiftLocation.Left - shiftLocation.Top),
                        -(shiftLocation.Left + shiftLocation.Top + shiftLocation.Width)
                    );
                    shiftLocation = new Rectangle(shiftLocation.Left, shiftLocation.Top, shiftLocation.Height, shiftLocation.Width);
                    break;
                case 180:
                    g.RotateTransform(180);
                    g.TranslateTransform(
                        -(shiftLocation.Left * 2 + shiftLocation.Width),
                        -(shiftLocation.Height + shiftLocation.Top * 2)
                    );
                    break;
                case 270:
                    g.RotateTransform(270);
                    g.TranslateTransform(
                        -(shiftLocation.Left + shiftLocation.Top + shiftLocation.Height),
                        (shiftLocation.Left - shiftLocation.Top)
                    );
                    shiftLocation = new Rectangle(shiftLocation.Left, shiftLocation.Top, shiftLocation.Height, shiftLocation.Width);
                    break;
            }
            g.DrawString(textElement.GetValue(), font, drawBrush, shiftLocation, drawFormat);
        }

        private void DrawScaledImage(BaseElement element)
        {
            Alignment.Horizontal alignmentX =
                (Alignment.Horizontal)Enum.Parse(typeof(Alignment.Horizontal), element.alignmentX, true);
            Alignment.Vertical alignmentY =
                (Alignment.Vertical)Enum.Parse(typeof(Alignment.Vertical), element.alignmentY, true);
            System.Drawing.Image image = System.Drawing.Image.FromFile(element.GetValue());
            Location scaledLocation = new Location();
            scaledLocation.left = element.location.left;
            scaledLocation.top = element.location.top;
            if (element.location.width > element.location.height)
            {
                scaledLocation.width = (int)(image.Width * ((double)element.location.height / image.Height));
                scaledLocation.height = element.location.height;
            }
            else
            {
                scaledLocation.height = (int)(image.Height * ((double)element.location.width / image.Width));
                scaledLocation.width = element.location.width;
            }
            if ((alignmentX == Alignment.Horizontal.middle) && (scaledLocation.width < element.location.width))
            {
                scaledLocation.left += (element.location.width - scaledLocation.width) / 2;
            }
            else if ((alignmentX == Alignment.Horizontal.right) && (scaledLocation.width < element.location.width))
            {
                scaledLocation.left += (element.location.width - scaledLocation.width);
            }
            if ((alignmentY == Alignment.Vertical.middle) && (scaledLocation.height < element.location.height))
            {
                scaledLocation.top += (element.location.height - scaledLocation.height) / 2;
            }
            else if ((alignmentY == Alignment.Vertical.bottom) && (scaledLocation.height < element.location.height))
            {
                scaledLocation.top += (element.location.height - scaledLocation.height);
            }
            RotateObject(scaledLocation, element.rotation);
            g.DrawImage(image, scaledLocation.left, scaledLocation.top, scaledLocation.width, scaledLocation.height);
        }

        private void RotateObject(Location location, float angle)
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
            Bitmap newDocument = new Bitmap(document.size.width, document.size.height, PixelFormat.Format32bppArgb);
            g = Graphics.FromImage(newDocument);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            Point center = new Point(document.size.width / 2, document.size.height / 2);
            g.Transform = RotateAroundPoint(document.rotation, center);
            g.DrawImage(currentDocument, document.shiftX, document.shiftY, document.size.width, document.size.height);
            return newDocument;
        }
    }
}
