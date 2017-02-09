using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace clby.Core.Json.Converters
{
    public class DictionaryStringBsonValue : Newtonsoft.Json.JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.GetType() == typeof(Dictionary<string, BsonValue>))
            {
                writer.WriteStartObject();
                var dict = value as Dictionary<string, BsonValue>;
                foreach (var item in dict)
                {
                    writer.WritePropertyName(item.Key);
                    WriteJson(writer, item.Value, serializer);
                }
                writer.WriteEndObject();
            }
            else if (value.GetType() == typeof(BsonDocument))
            {
                writer.WriteStartObject();
                var bsonDocument = BsonValue.Create(value).AsBsonDocument;
                foreach (var bd in bsonDocument)
                {
                    WriteJson(writer, bd, serializer);
                }
                writer.WriteEndObject();
            }
            else if (value.GetType() == typeof(BsonArray))
            {
                writer.WriteStartArray();
                var bsonArray = BsonValue.Create(value).AsBsonArray;
                foreach (var bItem in bsonArray)
                {
                    WriteJson(writer, bItem, serializer);
                }
                writer.WriteEndArray();
            }
            else
            {
                var bsonValue = BsonValue.Create(value);
                if (bsonValue.IsBsonDateTime)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("$date");
                    writer.WriteValue((bsonValue.AsBsonDateTime.ToUniversalTime().Ticks - 621355968000000000) / 10000);
                    writer.WriteEndObject();
                }
                else if (bsonValue.IsObjectId)
                {
                    writer.WriteStartObject();
                    writer.WritePropertyName("$oid");
                    writer.WriteValue(bsonValue.AsObjectId.ToString());
                    writer.WriteEndObject();
                }
                else if (bsonValue.IsBoolean)
                {
                    writer.WriteValue(bsonValue.AsBoolean);
                }
                else if (bsonValue.IsInt32)
                {
                    writer.WriteValue(bsonValue.AsInt32);
                }
                else if (bsonValue.IsInt64)
                {
                    writer.WriteValue(bsonValue.AsInt64);
                }
                else if (bsonValue.IsDouble)
                {
                    writer.WriteValue(bsonValue.AsDouble);
                }
                else if (bsonValue.IsBsonNull || bsonValue.IsBsonUndefined)
                {
                    writer.WriteValue("");
                }
                else
                {
                    writer.WriteValue(bsonValue.ToString());
                }
            }
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var target = reader.Value.ToString();
            return BsonSerializer.Deserialize<Dictionary<string, BsonValue>>(target);
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Dictionary<string, BsonValue>);
        }
    }
}
