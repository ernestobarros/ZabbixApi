using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ZabbixApi.Entities;

namespace ZabbixApi.CustomJsonConverters
{
    public class MaintenanceJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType == typeof(Maintenance))
            {
                return true;
            }
            return false;
        }

        public override bool CanRead => false;

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            JToken t = JToken.FromObject(value);
            if (t.Type != JTokenType.Object)
            {
                t.WriteTo(writer);
            }
            else
            {
                JObject o = (JObject)t;
                var entity = value as Maintenance;
                if (entity?.groups != null)
                {
                    o.AddFirst(new JProperty("groupids", new JArray(entity.groups.Select(g => g.Id))));
                }
                o.WriteTo(writer);
            }
        }
    }
}