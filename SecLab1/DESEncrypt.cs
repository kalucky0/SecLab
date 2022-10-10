using System;
using System.Security.Cryptography;
using System.IO;

internal sealed class DESEncrypt
{
    internal static void EncryptData(string inName, string outName, byte[] desKey, byte[] desIV)
    {
        FileStream fin = new FileStream(inName, FileMode.Open, FileAccess.Read);
        FileStream fout = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
        fout.SetLength(0);

        byte[] bin = new byte[100];
        long rdlen = 0;
        long totlen = fin.Length;
        int len;

        DES des = new DESCryptoServiceProvider();
        CryptoStream encStream = new CryptoStream(fout, des.CreateEncryptor(desKey, desIV), CryptoStreamMode.Write);

        while (rdlen < totlen)
        {
            len = fin.Read(bin, 0, 100);
            encStream.Write(bin, 0, len);
            rdlen = rdlen + len;
        }

        encStream.Close();
        fout.Close();
        fin.Close();
    }
    
    internal static void Run()
    {
        byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        byte[] key2 = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        EncryptData("in", "out", key, key2);
        Console.WriteLine("OK.");
    }
}