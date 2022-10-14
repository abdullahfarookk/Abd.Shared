namespace Abd.Shared.Abstraction;

public interface IEncryption
{
    string Encrypt(string toEncrypt, bool useHashing = true);
    string Decrypt(string cipherString, bool useHashing = true);
}