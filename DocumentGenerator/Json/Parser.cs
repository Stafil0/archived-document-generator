using DocumentGenerator.Document.Layout;
using DocumentGenerator.Document.Template;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace DocumentGenerator.Json
{
    public class Parser
    {
        public static TemplateDocument GetTemplateDocument(string path, Encoding encoding)
        {
            try
            {
                string jsonString = File.ReadAllText(path, encoding);
                return JsonConvert.DeserializeObject<TemplateDocument>(jsonString, new TemplateDocumentConverter());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static LayoutDocument GetLayoutDocument(string path, Encoding encoding)
        {
            try
            {
                string jsonString = File.ReadAllText(path, encoding);
                return JsonConvert.DeserializeObject<LayoutDocument>(jsonString, new LayoutDocumentConverter());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void GetObject<T>(string jsonPath, Encoding encoding, out T obj)
        {
            try
            {
                string jsonString = File.ReadAllText(jsonPath, encoding);
                obj = JsonConvert.DeserializeObject<T>(jsonString);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void SaveObject<T>(T obj, string path, Encoding encoding)
        {
            try
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(obj, Formatting.Indented), encoding);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static string[] GetArrayFromJson(string jsonPath, Encoding encoding)
        {
            Values value = new Values();
            GetObject<Values>(jsonPath, encoding, out value);
            return value.values;
        }
    }
}
