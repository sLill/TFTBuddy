using TFTBuddy.Common;

namespace TFTBuddy.Core
{
    public class ConfigField : Enumeration
    {
        #region Properties..
        public object Value { get; set; }

        public static ConfigField RiotApiKey { get; } = new ConfigField(0, nameof(RiotApiKey), "[API KEY]");
        #endregion Properties..

        #region Constructors..
        public ConfigField(int id, string name, object value)
            : base(id, name) 
        {
            Value = value;
        }
        #endregion Constructors..
    }
}
