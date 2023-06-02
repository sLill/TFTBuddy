using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TFTBuddy.Data.Converters
{
    public class ItemCollectionJsonConverter : JsonConverter
    {
        #region Methods..
        public override bool CanConvert(Type objectType)
            => objectType == typeof(ItemCollection);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var result = new ItemCollection();
            result.Items = new List<Item>();

            // Extracts all string properties into the list
            foreach (var prop in jObject.Properties())
            {
                if (prop.Value.Type == JTokenType.Object)
                {
                    var item = JsonConvert.DeserializeObject<Item>(prop.Value.ToString());
                    result.Items.Add(item);
                }
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => throw new NotImplementedException();
        #endregion Methods..
    }
}
