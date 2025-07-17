namespace Domain.Shared.Utilities
{
    public static class StringUtilities
    {
        public static string FormatString(this string value, int splitSize, char splitChar)
        {
            splitSize++;
            for (int i = splitSize - 1; i < value.Length; i += splitSize)
                value = value.Insert(i, splitChar.ToString());
            return value;
        }
    }
}
