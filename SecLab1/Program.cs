using System;
using System.Linq;

namespace SecLab1;

internal static class Program
{
    private static void Main()
    {
        try
        {
            // Des.Run();
            // Aes.Run();
            // Rc2.Run();
            // Rsa.Run();
            // Md5.Run();
            // Sha1.Run();
            // Sha256.Run();
            Sha512.Run();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        Console.In.ReadLine();
    }

    public static void PrintResults(byte[] buffer, string plaintext)
    {
        Console.WriteLine("\nEncrypted:");
        Console.WriteLine(buffer.Aggregate("", (s, b) => s + b.ToString("X2") + " "));
        Console.WriteLine("\nDecrypted:");
        Console.WriteLine(plaintext);
    }
}