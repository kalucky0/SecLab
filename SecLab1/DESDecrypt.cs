using System;
using System.Security.Cryptography;
using System.IO;

internal sealed class DESDecrypt
{
    private static void DecryptData(string inName, string outName, byte[] desKey, byte[] desIV)
    {
        FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
        FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
        fout.SetLength(0);

        byte[] bin = new byte[100];
        long totlen = fin.Length;
        int len;

        DES des = new DESCryptoServiceProvider();
        CryptoStream encStream = new CryptoStream(fin, des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Read);
        
        do
        {
            len = encStream.Read(bin, 0, 100);
            fout.Write(bin, 0, len);
        } while (len > 0);

        encStream.Close();
        fout.Close();
        fin.Close();
    }

    internal static void Run()
    {
        byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        byte[] key2 = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        DecryptData("in", "out", key, key2);
        Console.WriteLine("OK.");
    }
}