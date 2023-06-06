using System.Text;

namespace TFTBuddy.Core
{
    public class PointerTrace
    {
        #region Properties..
        public List<PointerTraceLevel> Levels { get; private set; } = new List<PointerTraceLevel>();
        #endregion Properties..

        #region Methods..
        public override string ToString()
        {
            var result = new StringBuilder();

            if (!Levels.Any())
                return result.ToString();

            // Apply padding to the output so it looks neat and tidy
            var largestAddressLength = Levels.Select(traceLevel => $"{traceLevel.Address:X}".Length).OrderByDescending(length => length).First();
            var largestReadResultLength = Levels.Select(traceLevel => $"{traceLevel.ReadResult:X}".Length).OrderByDescending(length => length).First();
            var largestOffsetLength = Levels.Select(traceLevel => $"{traceLevel.Offset:X}".Length).OrderByDescending(length => length).First();
            var largestResultLength = Levels.Select(traceLevel => $"{traceLevel.Result:X}".Length).OrderByDescending(length => length).First();

            var stringBuilder = new StringBuilder();
            foreach (var traceLevel in Levels)
            {
                string addressString = $"{traceLevel.Address:X}".PadLeft(largestAddressLength);
                string readResultString = $"{traceLevel.ReadResult:X}".PadLeft(largestReadResultLength);
                string offsetString = $"{traceLevel.Offset:X}".PadLeft(largestOffsetLength);
                string resultString = $"{traceLevel.Result:X}".PadLeft(largestResultLength);

                stringBuilder.Append($"{addressString} -> {readResultString} + {offsetString} = {resultString}");

                if (traceLevel != Levels.Last())
                    result.Append("\r\n");
            }

            return result.ToString();
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
                return false;
            else
            {
                var pointerTraceObj = obj as PointerTrace;
                
                if (Levels.Count != pointerTraceObj.Levels.Count)
                    return false;

                for (int index = 0; index < Levels.Count; ++index)
                {
                    if (!Levels[index].Equals(pointerTraceObj.Levels[index]))
                        return false;
                }

                return true;
            }
        }

        public override int GetHashCode()
            => -653240011 + EqualityComparer<List<PointerTraceLevel>>.Default.GetHashCode(Levels);
        #endregion Methods..
    }
}
