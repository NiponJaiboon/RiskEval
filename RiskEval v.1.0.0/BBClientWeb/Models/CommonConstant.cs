using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace BBClientWeb.Models
{
    public class CommonConstant
    {
        public static string ApplicationName(HttpRequestBase request)
        {
            if (request == null) return "";

            return request.ApplicationPath.Substring(1);
        }

        public static string GetApplicationUrl(HttpRequestBase request, string urlPath)
        {
            string configAppName = CommonConstant.ApplicationName(request);
            return (configAppName == "" ? "" : "/" + configAppName) + urlPath;
        }

        #region Encrypt & Decrypt

        private static readonly byte[] KeyByte = Convert.FromBase64String("eoNRr2w9PhU7IrgQtlco2nFZQx2K42llF2n/ndpjpBw=");
        private static readonly byte[] IVByte = Convert.FromBase64String("0R5fFIyRkX4ZsOFoLFtyVA==");

        public static string Encrypt(string plainText)
        {
            using (Rijndael myRijndael = Rijndael.Create())
            {
                myRijndael.Key = KeyByte;
                myRijndael.IV = IVByte;
                // Encrypt the string to an array of bytes.
                byte[] encrypted = EncryptStringToBytes(plainText, myRijndael.Key, myRijndael.IV);
                return Convert.ToBase64String(encrypted);
            }
        }

        public static string Decrypt(string cipheredText)
        {
            using (Rijndael myRijndael = Rijndael.Create())
            {
                myRijndael.Key = KeyByte;
                myRijndael.IV = IVByte;

                byte[] responseByte = Convert.FromBase64String(cipheredText);
                return DecryptStringFromBytes(responseByte, myRijndael.Key, myRijndael.IV);
            }
        }

        private static byte[] EncryptStringToBytes(string plainText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("key");
            byte[] encrypted;
            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encrypted;
        }

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] key, byte[] iv)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("key");
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException("key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext;

            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = key;
                rijAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }

            return plaintext;
        }

        public static string RemovePlusAndSpaceSymolFromBase64(string base64Text)
        {
            return base64Text.Replace('+', '-').Replace('/', '_');
        }

        public static string ResotrePlusAndSpaceSymolFromBase64(string base64Text)
        {
            return base64Text.Replace('-', '+').Replace('_', '/');
        }

        #endregion Encrypt & Decrypt
    }
}