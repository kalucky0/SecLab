using System;
using System.Security.Cryptography;
using System.Text;

namespace SecLab;

internal static class Rsa
{
    public static void Run()
    {
        DESCryptoServiceProvider key = new DESCryptoServiceProvider();

        var input = Console.ReadLine();

        byte[] buffer = CryptoMemoryStream.Encrypt(input, key);
        string plaintext = CryptoMemoryStream.Decrypt(buffer, key);

        Program.PrintResults(buffer, plaintext);
    }
    
    public static string GetKeyString(RSAParameters publicKey)
    {
        var stringWriter = new System.IO.StringWriter();
        var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
        xmlSerializer.Serialize(stringWriter, publicKey);
        return stringWriter.ToString();
    }

    public static string Encrypt(string textToEncrypt, string publicKeyString)
    {
        var bytesToEncrypt = Encoding.UTF8.GetBytes(textToEncrypt);
        using var rsa = new RSACryptoServiceProvider(2048);
        try
        {
            rsa.FromXmlString(publicKeyString.ToString());
            var encryptedData = rsa.Encrypt(bytesToEncrypt, true);
            var base64Encrypted = Convert.ToBase64String(encryptedData);
            return base64Encrypted;
        }
        finally
        {
            rsa.PersistKeyInCsp = false;
        }
    }

    public static string Decrypt(string textToDecrypt, string privateKeyString)
    {
        using var rsa = new RSACryptoServiceProvider(2048);
        try
        {
            rsa.FromXmlString(privateKeyString);
            var resultBytes = Convert.FromBase64String(textToDecrypt);
            var decryptedBytes = rsa.Decrypt(resultBytes, true);
            var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
            return decryptedData.ToString();
        }
        finally
        {
            rsa.PersistKeyInCsp = false;
        }
    }
}