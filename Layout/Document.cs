using System.Collections.Generic;

namespace Layout
{
    public class Document
    {
        public enum HorizontalAlignment
        {
            left,
            middle,
            right,
        }

        public enum VerticalAlignment
        {
            top,
            middle,
            bottom,
        }

        public enum Type
        {
            text,
            date,
            image
        }

        public class Location
        {
            public int left { get; set; }
            public int top { get; set; }
            public int width { get; set; }
            public int height { get; set; }
        }

        public class Size
        {
            public int width { get; set; }
            public int height { get; set; }
        }

        public class DocumentLayout
        {
            public List<Layout> layout { get; set; }
            public string background { get; set; }
            public int shiftX { get; set; }
            public int shiftY { get; set; }
            public int rotation { get; set; }
            public Size size { get; set; }

            public class Layout
            {
                public string type { get; set; } //
                public Location location { get; set; } //
                public string value { get; set; } //
                public string format { get; set; } //
                public FontLayout font { get; set; } //
                public string alignmentX { get; set; }
                public string alignmentY { get; set; }
                public int shiftX { get; set; }
                public int shiftY { get; set; }
                public int rotation { get; set; } //
            }

            public class FontLayout
            {
                public string fontFamily { get; set; }
                public string fontColor { get; set; }
                public int fontSize { get; set; }
            }
        }

        public class DocumentTemplate
        {
            public List<LayoutTemplate> layout { get; set; }
            public LayoutTemplate textDefault { get; set; }
            public LayoutTemplate dateDefault { get; set; }
            public LayoutTemplate imageDefault { get; set; }
            public string layoutDateFormat { get; set; }
            public string background { get; set; }
            public int[] shiftX { get; set; }
            public int[] shiftY { get; set; }
            public int[] rotation { get; set; }
            public Size size { get; set; }

            public class LayoutTemplate
            {
                public string type { get; set; }
                public Location location { get; set; }
                public string value { get; set; }
                public string[] dateRange { get; set; }
                public string valuePath { get; set; }
                public string format { get; set; }
                public FontTemplate font { get; set; }
                public string[] alignmentX { get; set; }
                public string[] alignmentY { get; set; }
                public int[] shiftX { get; set; }
                public int[] shiftY { get; set; }
                public int[] rotation { get; set; }
            }

            public class FontTemplate
            {
                public string[] fontFamily { get; set; }
                public string[] fontColor { get; set; }
                public int[] fontSize { get; set; }
            }
        }
    }
}
