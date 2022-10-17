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
            // Rc2Main();
            RsaMain();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
        Console.In.ReadLine();
    }

    private static void RsaMain()
    {
        var input = Console.ReadLine();

        RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider(2048); 
        RSAParameters paramsWithPrivateKey = cryptoServiceProvider.ExportParameters(true); 
        RSAParameters paramsWithPublicKey = cryptoServiceProvider.ExportParameters(false); 
        
        string publicKey = Rsa.GetKeyString(paramsWithPublicKey);
        string privateKey = Rsa.GetKeyString(paramsWithPrivateKey);

        Console.WriteLine();
        Console.WriteLine("Public key:");
        Console.WriteLine(publicKey);
        
        Console.WriteLine();
        Console.WriteLine("Private key:");
        Console.WriteLine(privateKey);

        string encryptedText = Rsa.Encrypt(input, publicKey);
        string decryptedText = Rsa.Decrypt(encryptedText, privateKey);

        Console.WriteLine();
        Console.WriteLine("Encrypted:");
        Console.WriteLine(encryptedText);
        Console.WriteLine();
        Console.WriteLine("Decrypted:");
        Console.WriteLine(decryptedText);
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