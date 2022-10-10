using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecLab1;

internal static class Rc2
{
    public static byte[] EncryptText(string plainText, byte[] key, byte[] iv)
    {
        using var rc2 = RC2.Create();
        using var encryptor = rc2.CreateEncryptor(key, iv);
        using var mStream = new MemoryStream();
        using var cStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write);

        byte[] toEncrypt = Encoding.UTF8.GetBytes(plainText);

        cStream.Write(toEncrypt, 0, toEncrypt.Length);
        cStream.FlushFinalBlock();

        return mStream.ToArray();
    }

    public static string DecryptText(byte[] cipherText, byte[] key, byte[] iv)
    {
        using var rc2 = RC2.Create();
        using var decryptor = rc2.CreateDecryptor(key, iv);
        using var mStream = new MemoryStream(cipherText);
        using var cStream = new CryptoStream(mStream, decryptor, CryptoStreamMode.Read);
        using var reader = new StreamReader(cStream, Encoding.UTF8);

        return reader.ReadToEnd();
    }
}