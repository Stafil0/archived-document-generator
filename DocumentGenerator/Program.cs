using System.Diagnostics;
using System.Text;

namespace DocumentGenerator
{

    class Program
    {
        private const string jsonPath = ".\\resources\\input.json";
        private const string layoutPath = ".\\resources\\output.json";
        private const string documentPath = ".\\resources\\document.png";
        private static Encoding encoding = Encoding.Default;

        static void Main(string[] args)
        {
            LayoutGenerator layoutGenerator = new LayoutGenerator(jsonPath, encoding);
            layoutGenerator.GenerateLayout();
            layoutGenerator.SaveLayout(layoutPath, encoding);

            LayoutDrawer layoutDrawer = new LayoutDrawer(layoutPath, encoding);
            layoutDrawer.PrintDocument(documentPath);

            Process.Start(documentPath);
        }
    }
}
