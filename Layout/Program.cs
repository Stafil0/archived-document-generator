using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Layout
{
    class Program
    {
        private const string inputPath = ".\\resources\\input.json";
        private const string layoutSavePath = ".\\resources\\output.json";
        private const string documentSavePath = ".\\document.png";
        private static Encoding encoding = Encoding.Default;

        static void Main(string[] args)
        {
            LayoutGenerator generator = new LayoutGenerator(inputPath, encoding);

            generator.GenerateLayout();
            generator.SaveLayout(layoutSavePath, encoding);

            LayoutParser parser = new LayoutParser(layoutSavePath, encoding);

            parser.PrintDocument(documentSavePath);
            Process.Start(documentSavePath);
        }
    }
}
