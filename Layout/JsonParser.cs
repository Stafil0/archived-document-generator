using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;

namespace Layout
{
    public static class JsonParser
    {
        private class Values
        {
            public string[] values;
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

        public static string[] GetArrayFromJson(string jsonPath, Encoding encoding)
        {
            Values values = new Values();
            JsonParser.GetObject<Values>(jsonPath, encoding, out values);
            return values.values;
        }
    }
}
