using System;
using System.Security.Cryptography;
using System.Text;

namespace SecLab;

internal static class Md5
{
    internal static void Run()
    {
        string input = Console.ReadLine() ?? string.Empty;
        string hash = GetMd5Hash(input);
        Console.WriteLine("MD5 tekstu: " + input + " to: " + hash + ".");
        
        Console.WriteLine(VerifyMd5Hash(input, hash) ? "Weryfikacja OK." : "Weryfikacja niepoprawna.");
    }
    
    private static string GetMd5Hash(string input)
    {
        MD5 md5Hasher = MD5.Create();
        byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
        StringBuilder sBuilder = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
            sBuilder.Append(data[i].ToString("x2"));

        return sBuilder.ToString();
    }

    private static bool VerifyMd5Hash(string input, string hash)
    {
        string hashOfInput = GetMd5Hash(input);
        StringComparer comparer = StringComparer.OrdinalIgnoreCase;
        if (0 == comparer.Compare(hashOfInput, hash))
        {
            return true;
        }

        return false;
    }
}