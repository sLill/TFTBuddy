using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TFTBuddy.Data.Converters
{
    public class TraitCollectionJsonConverter : JsonConverter
    {
        #region Methods..
        public override bool CanConvert(Type objectType)
            => objectType == typeof(TraitCollection);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var result = new TraitCollection();
            result.Traits = new List<Trait>();

            // Extracts all string properties into the list
            foreach (var prop in jObject.Properties())
            {
                if (prop.Value.Type == JTokenType.Object)
                {
                    var trait = JsonConvert.DeserializeObject<Trait>(prop.Value.ToString());
                    result.Traits.Add(trait);
                }
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => throw new NotImplementedException();
        #endregion Methods..
    }
}
