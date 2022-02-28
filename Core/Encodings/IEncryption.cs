namespace Abd.Shared.Core.Encodings;

public interface IEncryption: ICommonManager
{
    string Encrypt(string toEncrypt, bool useHashing = true);
    string Decrypt(string cipherString, bool useHashing = true);
}