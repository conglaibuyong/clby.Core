using MongoDB.Bson;
using Newtonsoft.Json;
using System;

namespace clby.Core.Json.Converters
{
    public class ObjectIdConverter : Newtonsoft.Json.JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var target = reader.Value.ToString();
            return ObjectId.Parse(target);
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ObjectId);
        }
    }
}
