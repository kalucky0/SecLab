using System;
using System.Security.Cryptography;
using System.Text;

namespace SecLab1;

internal static class Sha512
{
    internal static void Run()
    {
        string input = Console.ReadLine() ?? string.Empty;
        using SHA512 shaHash = SHA512.Create();
        byte[] textBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = shaHash.ComputeHash(textBytes);
        string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

        Console.WriteLine("The SHA512 hash of " + input + " is: " + hash);
        Console.In.ReadLine();
    }
}