using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TFTBuddy.Data.Converters
{
    public class TacticianCollectionJsonConverter : JsonConverter
    {
        #region Methods..
        public override bool CanConvert(Type objectType)
            => objectType == typeof(TacticianCollection);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var result = new TacticianCollection();
            result.Tacticians = new List<Tactician>();

            // Extracts all string properties into the list
            foreach (var prop in jObject.Properties())
            {
                if (prop.Value.Type == JTokenType.Object)
                {
                    var champion = JsonConvert.DeserializeObject<Tactician>(prop.Value.ToString());
                    result.Tacticians.Add(champion);
                }
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => throw new NotImplementedException();
        #endregion Methods..
    }
}
