using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TFTBuddy.Data.Converters
{
    public class ChampionCollectionJsonConverter : JsonConverter
    {
        #region Methods..
        public override bool CanConvert(Type objectType)
            => objectType == typeof(ChampionCollection);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);
            var result = new ChampionCollection();
            result.Champions = new List<Champion>();

            // Extracts all string properties into the list
            foreach (var prop in jObject.Properties())
            {
                if (prop.Value.Type == JTokenType.Object)
                {
                    var champion = JsonConvert.DeserializeObject<Champion>(prop.Value.ToString());
                    result.Champions.Add(champion);
                }
            }

            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            => throw new NotImplementedException();
        #endregion Methods..
    }
}
