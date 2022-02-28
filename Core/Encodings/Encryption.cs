using System.Security.Cryptography;

namespace Abd.Shared.Core.Encodings;

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
            var hashMd5 = new MD5CryptoServiceProvider();
            keyArray = hashMd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            hashMd5.Clear();
        }
        else
            keyArray = Encoding.UTF8.GetBytes(key);

        var tripleDes = new TripleDESCryptoServiceProvider
        {
            Key = keyArray,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };
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
            var hashMd5 = new MD5CryptoServiceProvider();
            keyArray = hashMd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            hashMd5.Clear();
        }
        else
        {
            keyArray = Encoding.UTF8.GetBytes(key);
        }

        var tripleDes = new TripleDESCryptoServiceProvider
        {
            Key = keyArray,
            Mode = CipherMode.ECB,
            Padding = PaddingMode.PKCS7
        };
        var cTransform = tripleDes.CreateDecryptor();
        var resultArray = cTransform.TransformFinalBlock(
            toEncryptArray, 0, toEncryptArray.Length);
        tripleDes.Clear();
        return Encoding.UTF8.GetString(resultArray);
    }
}