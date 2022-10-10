using System;
using System.IO;
using System.Security.Cryptography;

internal sealed class CryptoMemoryStream
{
    internal static void Run()
    {
        DESCryptoServiceProvider key = new DESCryptoServiceProvider();
        byte[] buffer = Encrypt("Tekst", key);
        string plaintext = Decrypt(buffer, key);
        Console.WriteLine(plaintext);
    }

    public static byte[] Encrypt(string PlainText, SymmetricAlgorithm key)
    {
        MemoryStream ms = new MemoryStream();
        CryptoStream encStream = new CryptoStream(ms, key.CreateEncryptor(), CryptoStreamMode.Write);
        StreamWriter sw = new StreamWriter(encStream);
        sw.WriteLine(PlainText);
        sw.Close();
        encStream.Close();
        byte[] buffer = ms.ToArray();
        ms.Close();
        return buffer;
    }

    public static string Decrypt(byte[] CypherText, SymmetricAlgorithm key)
    {
        MemoryStream ms = new MemoryStream(CypherText);
        CryptoStream encStream = new CryptoStream(ms, key.CreateDecryptor(), CryptoStreamMode.Read);
        StreamReader sr = new StreamReader(encStream);
        string val = sr.ReadLine();
        sr.Close();
        encStream.Close();
        ms.Close();
        return val;
    }
}