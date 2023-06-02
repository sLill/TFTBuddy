using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TFTBuddy.Data
{
    public class AugmentCollectionJsonConverter : JsonConverter
    {
        #region Methods..
        public override bool CanConvert(Type objectType)
            => objectType == typeof(AugmentCollection);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var result = new AugmentCollection();
            result.Augments = new List<Augment>();

            // Extracts all string properties into the list
            foreach (var prop in jObject.Properties())
            {
                if (prop.Value.Type == JTokenType.Object)
                {
                    var augment = JsonConvert.DeserializeObject<Augment>(prop.Value.ToString());
                    result.Augments.Add(augment);
                }
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => throw new NotImplementedException();
        #endregion Methods..
    }
}
