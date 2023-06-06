using System.Globalization;

namespace TFTBuddy.Common
{
    public static class StringExtensions
    {
        #region Methods.. 
        /// <summary>
        /// Converts a string of bytes (ex "48 ?? ?? 75 11") to an array of bytes
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte?[] ToByteArray(this string value)
        {
            List<byte?> byteList = new List<byte?>();
            var singleByteStrings = value.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var singleByteString in singleByteStrings)
            {
                byte parsedByte = 0;
                if (byte.TryParse(singleByteString, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out parsedByte))
                    byteList.Add(parsedByte);
                else
                    byteList.Add(null);
            }

            return byteList.ToArray();
        }

        public static long TryParseHex(this string value)
        {
            long result = 0;
            bool parseResult = false;

            if (value.StartsWith("-"))
            {
                parseResult = long.TryParse(value.Substring(1), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result);
                result = (-1) * result;
            }
            else
                parseResult = long.TryParse(value, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out result);

            if (!parseResult)
                throw new Exception($"Could not parse hex string");

            return result;
        }
        #endregion Methods..
    }
}
