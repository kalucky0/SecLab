using System;
using System.IO;
using System.Security.Cryptography;

namespace SecLab;

internal static class Des
{
    public static void Run()
    {
        DESCryptoServiceProvider key = new DESCryptoServiceProvider();

        var input = Console.ReadLine();

        byte[] buffer = CryptoMemoryStream.Encrypt(input, key);
        string plaintext = CryptoMemoryStream.Decrypt(buffer, key);

        Program.PrintResults(buffer, plaintext);
    }
    
    /// <example>
    /// <code>
    /// byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
    /// byte[] key2 = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
    /// EncryptData("in", "out", key, key2);
    /// </code>
    /// </example>
    /// <param name="inName">Name of input file</param>
    /// <param name="outName">Name of output file</param>
    /// <param name="desKey">The secret key to use for the symmetric algorithm</param>
    /// <param name="desIv">The initialization vector to use for the symmetric algorithm</param>
    internal static void EncryptData(string inName, string outName, byte[] desKey, byte[] desIv)
    {
        var inputFile = new FileStream(inName, FileMode.Open, FileAccess.Read);
        var outputFile = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
        outputFile.SetLength(0);

        var buffer = new byte[100];
        var length = inputFile.Length;

        DES des = new DESCryptoServiceProvider();
        var encStream = new CryptoStream(outputFile, des.CreateEncryptor(desKey, desIv), CryptoStreamMode.Write);

        long readLen = 0;
        while (readLen < length)
        {
            var len = inputFile.Read(buffer, 0, 100);
            encStream.Write(buffer, 0, len);
            readLen += len;
        }

        encStream.Close();
        outputFile.Close();
        inputFile.Close();
    }
    
    /// <example>
    /// <code>
    /// byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
    /// byte[] key2 = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
    /// DecryptData("in", "out", key, key2);
    /// </code>
    /// </example>
    /// <param name="inName">Name of input file</param>
    /// <param name="outName">Name of output file</param>
    /// <param name="desKey">The secret key to use for the symmetric algorithm</param>
    /// <param name="desIv">The initialization vector to use for the symmetric algorithm</param>
    private static void DecryptData(string inName, string outName, byte[] desKey, byte[] desIv)
    {
        var fileInput = new FileStream(inName, FileMode.Open, FileAccess.Read);
        var fileOutput = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
        fileOutput.SetLength(0);

        var buffer = new byte[100];

        DES des = new DESCryptoServiceProvider();
        var encStream = new CryptoStream(fileInput, des.CreateDecryptor(desKey, desIv), CryptoStreamMode.Read);

        int len;
        do
        {
            len = encStream.Read(buffer, 0, 100);
            fileOutput.Write(buffer, 0, len);
        } while (len > 0);

        encStream.Close();
        fileOutput.Close();
        fileInput.Close();
    }
}