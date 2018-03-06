using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reflection;

namespace ShipStation4Net.Converters
{
    public class DynamicPropertyNameConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Type type = value.GetType();
            JObject jo = new JObject();

            foreach (PropertyInfo prop in type.GetProperties().Where(p => p.CanRead && p.PropertyType.IsGenericType))
            {
                string propName = prop.Name;
                object propValue = prop.GetValue(value, null);
                JToken token = (propValue != null) ? JToken.FromObject(propValue, serializer) : JValue.CreateNull();

                if (propValue != null)
                {
                    JsonPropertyNameByTypeAttribute att = prop.GetCustomAttributes<JsonPropertyNameByTypeAttribute>()
                        .FirstOrDefault(a => a.ObjectType.IsAssignableFrom(propValue.GetType()));

                    if (att != null)
                        propName = att.PropertyName;
                }

                jo.Add(propName, token);
            }

            jo.WriteTo(writer);
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jObject = JObject.Load(reader);
            existingValue = Activator.CreateInstance(objectType);
            var properties = objectType.GetProperties();

            foreach (PropertyInfo prop in properties.Where(p => p.ReflectedType.IsConstructedGenericType))
            {
                foreach (var attr in prop.GetCustomAttributes().OfType<JsonPropertyNameByTypeAttribute>())
                {
                    if (jObject[attr.PropertyName] != null)
                    {
						//we should deserialize object with all conventions applied (e.g. time zone should be converted back as well)
						var objectToSet = serializer.Deserialize(jObject[attr.PropertyName].CreateReader(), attr.ObjectType);
                        existingValue.GetType().GetProperty(prop.Name).SetValue(existingValue, objectToSet);
                    }
                }
            }

            serializer.Populate(jObject.CreateReader(), existingValue);

            return existingValue;
        }

        public override bool CanConvert(Type objectType)
        {
            // CanConvert is not called if a [JsonConverter] attribute is used
            return false;
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    class JsonPropertyNameByTypeAttribute : Attribute
    {
        public string PropertyName { get; set; }
        public Type ObjectType { get; set; }

        public JsonPropertyNameByTypeAttribute(string propertyName, Type objectType)
        {
            PropertyName = propertyName;
            ObjectType = objectType;
        }
    }
}
