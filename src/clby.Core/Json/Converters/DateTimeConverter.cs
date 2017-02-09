using Newtonsoft.Json;
using System;

namespace clby.Core.Json.Converters
{
    public class DateTimeConverter : Newtonsoft.Json.JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var dt = Convert.ToDateTime(value);
            if (dt == DateTime.MaxValue || dt == DateTime.MinValue)
            {
                writer.WriteValue("");
            }
            else
            {
                writer.WriteValue(dt.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var target = reader.Value.ToString();
            return DateTime.Parse(target);
        }
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }
    }
}
