using System;
using System.Linq;
using System.Security.Cryptography;

namespace SecLab1;

public class Program
{
    private static void Main()
    {
        AesMain();
    }

    private static void AesMain()
    {
        var key = new AesCryptoServiceProvider();

        var input = Console.ReadLine();
        
        byte[] encrypted = Aes.EncryptStringToBytes(input, key.Key, key.IV);
        string plaintext = Aes.DecryptStringFromBytes(encrypted, key.Key, key.IV);

        Console.WriteLine(plaintext);
        Console.WriteLine(encrypted.Aggregate("", (s, b) => s + b.ToString("X2") + " "));
    }

    private static void DesMain()
    {
        DESCryptoServiceProvider key = new DESCryptoServiceProvider();
        
        var input = Console.ReadLine();
        
        byte[] buffer = CryptoMemoryStream.Encrypt(input, key);
        string plaintext = CryptoMemoryStream.Decrypt(buffer, key);
        
        Console.WriteLine(plaintext);
        Console.WriteLine(buffer.Aggregate("", (s, b) => s + b.ToString("X2") + " "));
    }
}