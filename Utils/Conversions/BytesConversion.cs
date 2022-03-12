namespace Abd.Shared.Utils.Conversions;

public static class BytesConversion
{
    public static long ToKilobytes(this long value) => value / 1024;
    public static long ToMegabytes(this long value) => value / (1024 * 1024);
    public static long ToGigabytes(this long value) => value / (1024 * 1024 * 1024);

}