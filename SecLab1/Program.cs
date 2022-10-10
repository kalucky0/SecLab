using System;
using System.Linq;
using System.Security.Cryptography;

namespace SecLab1;

public static class Program
{
    private static void Main()
    {
        try
        {
            // DesMain();
            // AesMain();
            Rc2Main();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private static void Rc2Main()
    {
        byte[] key;
        byte[] iv;

        using (var rc2 = RC2.Create())
        {
            key = rc2.Key;
            iv = rc2.IV;
        }

        var input = Console.ReadLine();

        byte[] encrypted = Rc2.EncryptText(input, key, iv);
        string plaintext = Rc2.DecryptText(encrypted, key, iv);

        PrintResults(encrypted, plaintext);
    }

    private static void AesMain()
    {
        var key = new AesCryptoServiceProvider();

        var input = Console.ReadLine();

        byte[] encrypted = Aes.EncryptStringToBytes(input, key.Key, key.IV);
        string plaintext = Aes.DecryptStringFromBytes(encrypted, key.Key, key.IV);

        PrintResults(encrypted, plaintext);
    }

    private static void DesMain()
    {
        DESCryptoServiceProvider key = new DESCryptoServiceProvider();

        var input = Console.ReadLine();

        byte[] buffer = CryptoMemoryStream.Encrypt(input, key);
        string plaintext = CryptoMemoryStream.Decrypt(buffer, key);

        PrintResults(buffer, plaintext);
    }
    
    private static void PrintResults(byte[] buffer, string plaintext)
    {
        Console.WriteLine("\nEncrypted:");
        Console.WriteLine(buffer.Aggregate("", (s, b) => s + b.ToString("X2") + " "));
        Console.WriteLine("\nDecrypted:");
        Console.WriteLine(plaintext);
    }
}