namespace TFTBuddy.Core
{
    public class PointerTraceLevel
    {
        #region Properties..
        public ulong Address { get; private set; }
        public ulong ReadResult { get; private set; }
        public long Offset { get; private set; }
        public ulong Result { get; private set; }
        #endregion Properties..

        #region Constructors..
        public PointerTraceLevel(ulong address, ulong readResult, long offset, ulong result)
        {
            Address = address;
            ReadResult = readResult;
            Offset = offset;
            Result = result;
        }
        #endregion Constructors..

        #region Methods..
        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;
            else
            {
                var pointerTraceLevelObj = obj as PointerTraceLevel;
                return Address == pointerTraceLevelObj.Address
                    && ReadResult == pointerTraceLevelObj.ReadResult
                    && Offset == pointerTraceLevelObj.Offset
                    && Result == pointerTraceLevelObj.Result;
            }
        }

        public override int GetHashCode()
        {
            var hashCode = 1237761341;
            hashCode = hashCode * -1521134295 + Address.GetHashCode();
            hashCode = hashCode * -1521134295 + ReadResult.GetHashCode();
            hashCode = hashCode * -1521134295 + Offset.GetHashCode();
            hashCode = hashCode * -1521134295 + Result.GetHashCode();
            return hashCode;
        } 
        #endregion Methods..
    }
}
