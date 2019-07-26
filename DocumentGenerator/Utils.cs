using DocumentGenerator.Json;
using System;
using System.Drawing.Text;
using System.Globalization;
using System.Text;

namespace DocumentGenerator
{
    public static class Utils
    {
        private static Random rand = new Random();
        private static CultureInfo dateCulture = CultureInfo.InvariantCulture;
        private static Encoding encoding = Encoding.Default;

        public static Document.Layout.Font GetRandomFontFromResources(Document.Template.Font fontRange)
        {
            Document.Layout.Font font = new Document.Layout.Font();
            font.fontFamily = GetRandomStringFromArray(fontRange.fontFamily);
            font.fontColor = GetRandomStringFromArray(fontRange.fontColor);
            font.fontSize = Utils.GetRandomIntInRange(fontRange.fontSize);
            return font;
        }

        public static DateTime GetRandomDateInRange(string[] range, string format)
        {
            DateTime startDate = GetDateFromString(range[0], format);
            DateTime endDate = GetDateFromString(range[1], format);
            int daysRange = (endDate - startDate).Days;
            return startDate.AddDays(rand.NextDouble() * daysRange);
        }

        public static DateTime GetDateFromString(string date, string format)
        {
            return DateTime.ParseExact(date, format, dateCulture);
        }

        public static string GetRandomStringFromArray(string[] arr)
        {
            return arr[rand.Next(0, arr.Length)] ;
        }

        public static int GetRandomIntInRange(int[] range)
        {
            return range != null ? rand.Next(range[0], range[1] + 1) : 0;
        }

        public static string GetRandomValueFromFile(string path)
        {
            return GetRandomStringFromArray(Parser.GetArrayFromJson(path, encoding));
        }

        public static int GetRandomTextRotationInRange(int[] range)
        {
            return GetRandomIntInRange(new int[] { range[0] / 90, range[1] / 90 }) * 90;
        }

        public static bool FontExists(string fontFamily)
        {
            var fontsCollection = new InstalledFontCollection();
            foreach (var fontFamiliy in fontsCollection.Families)
            {
                if (fontFamiliy.Name == fontFamily)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
