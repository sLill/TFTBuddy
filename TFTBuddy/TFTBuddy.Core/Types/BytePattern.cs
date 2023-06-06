using TFTBuddy.Common;

namespace TFTBuddy.Core
{
    public class BytePattern
    {
        #region Properties..
        public string Name { get; }
        public string PatternString { get; }
        public byte?[] Bytes { get; private set; }
        public List<ulong> MatchedAddresses { get; private set; }
        public string LastResultAddress { get; set; }
        #endregion Properties..

        #region Constructors..
        public BytePattern(string name, string patternString)
        {
            Name = name;
            PatternString = patternString;
            Bytes = patternString.ToByteArray();
            MatchedAddresses = new List<ulong>();
        } 
        #endregion Constructors..
    }
}
