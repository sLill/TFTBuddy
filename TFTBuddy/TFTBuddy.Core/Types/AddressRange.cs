namespace TFTBuddy.Core
{
    public class AddressRange
    {
        #region Properties..
        public ulong Start { get; private set; }
        public ulong End { get; private set; }
        public ulong Size { get; private set; }
        #endregion Properties..

        #region Constructors..
        public AddressRange(ulong start, ulong size)
        {
            Start = start;
            End = start + size;
            Size = size;
        }
        #endregion Constructors..
    }
}
