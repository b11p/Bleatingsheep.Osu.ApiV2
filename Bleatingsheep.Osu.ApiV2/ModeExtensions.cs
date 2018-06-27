namespace Bleatingsheep.Osu.ApiV2
{
    public static class ModeExtensions
    {
        public static string ModeString(this Mode mode)
        {
            switch (mode)
            {
                case Mode.Osu:
                    return "osu";
                case Mode.Taiko:
                    return "taiko";
                case Mode.Fruits:
                    return "fruits";
                case Mode.Mania:
                    return "mania";
                default:
                    return null;
            }
        }
    }
}
