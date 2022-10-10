using System.IO;
using System.Security.Cryptography;

namespace SecLab1;

internal static class CryptoMemoryStream
{
    public static byte[] Encrypt(string plainText, SymmetricAlgorithm key)
    {
        var ms = new MemoryStream();
        var encStream = new CryptoStream(ms, key.CreateEncryptor(), CryptoStreamMode.Write);
        var sw = new StreamWriter(encStream);

        byte[] buffer = ms.ToArray();

        sw.Close();
        encStream.Close();
        ms.Close();

        return buffer;
    }

    public static string Decrypt(byte[] cypherText, SymmetricAlgorithm key)
    {
        var ms = new MemoryStream(cypherText);
        var encStream = new CryptoStream(ms, key.CreateDecryptor(), CryptoStreamMode.Read);
        var sr = new StreamReader(encStream);

        string value = sr.ReadLine();

        sr.Close();
        encStream.Close();
        ms.Close();

        return value;
    }
}