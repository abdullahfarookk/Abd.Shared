namespace Abd.Shared.Core.GuidUtils;

public static class GuidExtension
{
    public static Guid ToGuid(this int value)
    {
        byte[] bytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(bytes, 0);
        return new Guid(bytes);
    }

    public static int ToInt(this Guid value)
    {
        byte[] b = value.ToByteArray();
        int bint = BitConverter.ToInt32(b, 0);
        return bint;
    }

    public static bool IsNull(this Guid? guid) =>
        !(guid.HasValue && guid.Value != Guid.Empty);

    public static bool IsNull(this Guid guid) =>
        guid == Guid.Empty;
}