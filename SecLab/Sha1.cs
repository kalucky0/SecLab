using System;
using System.Security.Cryptography;
using System.Text;

namespace SecLab;

internal static class Sha1
{
    internal static void Run()
    {
        string input = Console.ReadLine() ?? string.Empty;
        using SHA1 sha1Hash = SHA1.Create();
        byte[] textBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = sha1Hash.ComputeHash(textBytes);
        string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

        Console.WriteLine("The SHA1 hash of " + input + " is: " + hash);
        Console.In.ReadLine();
    }
}