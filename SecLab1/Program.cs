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
        using var myAes = new AesCryptoServiceProvider();

        var input = Console.ReadLine();

        byte[] encrypted = Aes.EncryptStringToBytes(input, myAes.Key, myAes.IV);
        string plaintext = Aes.DecryptStringFromBytes(encrypted, myAes.Key, myAes.IV);

        Console.WriteLine(input);
        Console.WriteLine(plaintext);
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