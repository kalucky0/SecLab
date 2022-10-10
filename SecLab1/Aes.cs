using System;
using System.IO;
using System.Security.Cryptography;

namespace SecLab1;

internal static class Aes
{
    internal static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] IV)
    {
        if (plainText is not { Length: > 0 })
            throw new ArgumentNullException(nameof(plainText));
        if (key is not { Length: > 0 })
            throw new ArgumentNullException(nameof(key));
        if (IV is not { Length: > 0 })
            throw new ArgumentNullException(nameof(IV));

        using var aesAlg = new AesCryptoServiceProvider();
        aesAlg.Key = key;
        aesAlg.IV = IV;

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using var swEncrypt = new StreamWriter(csEncrypt);

        swEncrypt.Write(plainText);

        var encrypted = msEncrypt.ToArray();

        return encrypted;
    }

    internal static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] IV)
    {
        if (cipherText is not { Length: > 0 })
            throw new ArgumentNullException(nameof(cipherText));
        if (key is not { Length: > 0 })
            throw new ArgumentNullException(nameof(key));
        if (IV is not { Length: > 0 })
            throw new ArgumentNullException(nameof(IV));

        string plaintext = null;

        using var aesAlg = new AesCryptoServiceProvider();
        aesAlg.Key = key;
        aesAlg.IV = IV;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var msDecrypt = new MemoryStream(cipherText);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);

        plaintext = srDecrypt.ReadToEnd();

        return plaintext;
    }
}