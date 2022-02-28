

namespace Abd.Shared.Core.BitUtils;

public static class BitConverter
{
    public static List<int> ToBits(this int value)
    {
        var result =  new List<int>();
        var bitArray = new BitArray(new[] { value });
        for (var i = 0; i < (bitArray.Length) - 1; i++)
        {
            if (bitArray[i])
            {
                result.Add((int)Math.Pow(2, i));
            }
        }

        return result;
    }
    public static List<long> ToBits(this long value)
    {
        var result = new List<long>();
        if (value <= 0) return result;
        var bitArray = new BitArray(System.BitConverter.GetBytes(value));
        for (var i = 0; i < (bitArray.Length) - 1; i++)
        {
            if (bitArray[i])
            {
                result.Add((long)Math.Pow(2, i));
            }
        }

        return result;
    }
}