using System;
using System.Linq;
using System.Security.Cryptography;

namespace SecLab1;

public class Program
{
    private static void Main()
    {
        DESCryptoServiceProvider key = new DESCryptoServiceProvider();
        
        var input = Console.ReadLine();
        
        byte[] buffer = CryptoMemoryStream.Encrypt(input, key);
        string plaintext = CryptoMemoryStream.Decrypt(buffer, key);
        
        Console.WriteLine(plaintext);
        Console.WriteLine(buffer.Aggregate("", (s, b) => s + b.ToString("X2") + " "));
    }
}