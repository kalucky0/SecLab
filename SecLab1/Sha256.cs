using System;
using System.Security.Cryptography;
using System.Text;

namespace SecLab1;

internal static class Sha256
{
    internal static void Run()
    {
        string input = Console.ReadLine() ?? string.Empty;
        using SHA256 shaHash = SHA256.Create();
        byte[] textBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = shaHash.ComputeHash(textBytes);
        string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

        Console.WriteLine("The SHA256 hash of " + input + " is: " + hash);
    }
}