using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
using System.Net.Mime;

namespace _3DES_Files_Encryption_Decryption
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "C:\\Users\\edove\\Desktop\\Fakultet\\sampleText.txt";
                string input;
                string key = "eldin";
            while(true)
            {
                Console.WriteLine("a) Enkripto");
                Console.WriteLine("b) Dekripto");
                Console.WriteLine("c) Shkyqu");

                input = Console.ReadLine();
                if (input == "c")
                    break;
                else
                {
                    if (input == "a")
                        EncryptFile(filePath, key);
                    else if (input == "b")
                        DecryptFile(filePath, key);

                }
            }

        }

        static void EncryptFile(string filePath, string key)
        {
            byte[] plainContent = File.ReadAllBytes(filePath);
            using (var tDES = new TripleDESCryptoServiceProvider())
            {
                tDES.IV = Encoding.UTF8.GetBytes(key);
                tDES.Key = Encoding.UTF8.GetBytes(key);
                tDES.Mode = CipherMode.ECB;
                tDES.Padding = PaddingMode.PKCS7;

                using (var memoryStream = new MemoryStream())
                {
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, tDES.CreateEncryptor(), CryptoStreamMode.Write);

                    cryptoStream.Write(plainContent, 0, plainContent.Length);
                    cryptoStream.FlushFinalBlock();
                    File.WriteAllBytes(filePath, memoryStream.ToArray());
                    Console.WriteLine("Enkriptuar me sukses " + filePath);

                }
            }
        }

        private static void DecryptFile(string filePath, string key)
        {
            byte[] encrypted = File.ReadAllBytes(filePath);
            using (var tDES = new TripleDESCryptoServiceProvider())
            {
                tDES.IV = Encoding.UTF8.GetBytes(key);
                tDES.Key = Encoding.UTF8.GetBytes(key);
                tDES.Mode = CipherMode.ECB;
                tDES.Padding = PaddingMode.PKCS7;

                using (var memoryStream = new MemoryStream())
                {
                    CryptoStream cryptoStream = new CryptoStream(memoryStream, tDES.CreateDecryptor(), CryptoStreamMode.Write);

                    cryptoStream.Write(encrypted, 0, encrypted.Length);
                    cryptoStream.FlushFinalBlock();
                    File.WriteAllBytes(filePath, memoryStream.ToArray());
                    Console.WriteLine("Dekriptuar me sukses" + filePath);
                }
            }
        }
    }
}
