using System;
using System.IO;
using System.Security.Cryptography;

namespace SecLab;

internal static class Aes
{
    internal static void Run()
    {
        var key = new AesCryptoServiceProvider();

        var input = Console.ReadLine();

        byte[] encrypted = Aes.EncryptStringToBytes(input, key.Key, key.IV);
        string plaintext = Aes.DecryptStringFromBytes(encrypted, key.Key, key.IV);

        Program.PrintResults(encrypted, plaintext);
    }
    
    private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
    {
        if (plainText is not { Length: > 0 })
            throw new ArgumentNullException(nameof(plainText));
        if (key is not { Length: > 0 })
            throw new ArgumentNullException(nameof(key));
        if (iv is not { Length: > 0 })
            throw new ArgumentNullException(nameof(iv));

        using var aesAlg = new AesCryptoServiceProvider();
        aesAlg.Key = key;
        aesAlg.IV = iv;

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }

        var encrypted = msEncrypt.ToArray();

        return encrypted;
    }

    private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
    {
        if (cipherText is not { Length: > 0 })
            throw new ArgumentNullException(nameof(cipherText));
        if (key is not { Length: > 0 })
            throw new ArgumentNullException(nameof(key));
        if (iv is not { Length: > 0 })
            throw new ArgumentNullException(nameof(iv));

        using var aesAlg = new AesCryptoServiceProvider();
        aesAlg.Key = key;
        aesAlg.IV = iv;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var msDecrypt = new MemoryStream(cipherText);
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);

        var plaintext = srDecrypt.ReadToEnd();

        return plaintext;
    }
}