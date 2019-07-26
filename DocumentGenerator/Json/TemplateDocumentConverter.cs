using DocumentGenerator.Document.Template.Element;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace DocumentGenerator.Json
{
    class TemplateDocumentConverter : Newtonsoft.Json.Converters.CustomCreationConverter<BaseElement>
    {
        public override BaseElement Create(Type objectType)
        {
            throw new NotImplementedException();
        }

        public BaseElement Create(Type objectType, JObject jObject)
        {
            var type = (string)jObject.Property("type");

            switch (type)
            {
                case "text":
                    return new Text();
                case "date":
                    return new Date();
                case "number":
                    return new Number();
                case "image":
                    return new Image();
            }

            throw new ApplicationException(String.Format("The element type {0} is not supported!", type));
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            var target = Create(objectType, jObject);

            serializer.Populate(jObject.CreateReader(), target);

            return target;
        }
    }
}
