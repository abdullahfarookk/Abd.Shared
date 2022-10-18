namespace Abd.Shared.Utils.GuidUtils;

public static class GuidExtension
{
    public static Guid ToGuid(this int value)
    {
        var bytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(bytes, 0);
        return new Guid(bytes);
    }

    public static int ToInt(this Guid value)
    {
        var b = value.ToByteArray();
        return BitConverter.ToInt32(b, 0);
    }
    public static bool IsNoE(this Guid? guid)
    {
        return !guid.HasValue || guid == Guid.Empty;
    }

    public static bool IsNoE(this Guid guid)
    {
        return guid == Guid.Empty;
    }  
}