using System.IO;
using System.Security.Cryptography;

namespace SecLab1;

internal static class DesDecrypt
{
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
    /// <param name="desIV">The initialization vector to use for the symmetric algorithm</param>
    private static void DecryptData(string inName, string outName, byte[] desKey, byte[] desIV)
    {
        var fileInput = new FileStream(inName, FileMode.Open, FileAccess.Read);
        var fileOutput = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
        fileOutput.SetLength(0);

        var buffer = new byte[100];
        var length = fileInput.Length;

        DES des = new DESCryptoServiceProvider();
        var encStream = new CryptoStream(fileInput, des.CreateDecryptor(desKey, desIV), CryptoStreamMode.Read);

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