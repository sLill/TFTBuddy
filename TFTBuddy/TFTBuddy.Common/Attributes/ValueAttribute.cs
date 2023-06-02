namespace TFTBuddy.Common
{
    public class ValueAttribute : Attribute
    {
        #region Properties..
        public object Value { get; private set; }
        #endregion Properties..

        #region Constructors..
        public ValueAttribute(object value)
        {
            Value = value;
        } 
        #endregion Constructors..
    }
}
