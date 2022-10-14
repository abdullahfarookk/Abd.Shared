using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Abd.Shared.Core;

public class Encryption : IEncryption
{
    private readonly string _key;

    public Encryption(string key)
    {
        _key = key;
    }
    public string Encrypt(string toEncrypt, bool useHashing = true)
    {
        var securityKey = _key;
        byte[] keyArray;
        var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
        var key = securityKey;
        if (useHashing)
        {
            using var hashMd5 = SHA512.Create();
            keyArray = hashMd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            hashMd5.Clear();
        }
        else
            keyArray = Encoding.UTF8.GetBytes(key);

        using var tripleDes = TripleDES.Create();

        tripleDes.Key = keyArray;
        tripleDes.Mode = CipherMode.ECB;
        tripleDes.Padding = PaddingMode.PKCS7;
        
        var cTransform = tripleDes.CreateEncryptor();
        var resultArray =
            cTransform.TransformFinalBlock(toEncryptArray, 0,
                toEncryptArray.Length);
        tripleDes.Clear();
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    public string Decrypt(string cipherString, bool useHashing = true)
    {
        var securityKey = _key;
        byte[] keyArray;
        var toEncryptArray = Convert.FromBase64String(cipherString.Replace(" ", "+")); //NEW ONE
        var key = securityKey;
        if (useHashing)
        {
            using var hashMd5 = MD5.Create();
            keyArray = hashMd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            hashMd5.Clear();
        }
        else
        {
            keyArray = Encoding.UTF8.GetBytes(key);
        }

        using var tripleDes = TripleDES.Create();

        tripleDes.Key = keyArray;
        tripleDes.Mode = CipherMode.ECB;
        tripleDes.Padding = PaddingMode.PKCS7;
        
        var cTransform = tripleDes.CreateDecryptor();
        var resultArray = cTransform.TransformFinalBlock(
            toEncryptArray, 0, toEncryptArray.Length);
        tripleDes.Clear();
        return Encoding.UTF8.GetString(resultArray);
    }
}
public static class Base64Utils
{
    public static string ToBase64Key(long number) =>
        Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:D4}", number)));
}