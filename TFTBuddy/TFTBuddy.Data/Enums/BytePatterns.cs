using TFTBuddy.Common;

namespace TFTBuddy.Data
{
    public enum BytePatterns
    {
        [Value("48 3B 0D ?? ?? ?? ?? 75 11")]
        LocalPlayer,

        [Value("F3 0F 5C 35 ?? ?? ?? ?? 0F 28 F8")]
        GameTime,

        [Value("89 57 10 48 8B 0D ? ? ? ?")]
        ObjectManager,
    }
}
