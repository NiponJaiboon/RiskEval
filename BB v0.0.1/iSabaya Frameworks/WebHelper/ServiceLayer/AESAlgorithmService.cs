using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;

namespace WebHelper.ServiceLayer
{
    public class AESAlgorithmService
    {
        public static string InsideKey = "75C4782B51315EA46DAD86F3B543081755B69F4722EFA9399229885BD8A99641";
        public static string InsideIV = "39EAE0A8C074ADC4C30C216C03719EAB";

        public static byte[] ConvertStringHexToArrayByte(string hexString)
        {
            byte[] result = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i = i + 2)
            {
                result[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return result;
        }

        public static string EncryptString_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an AesCryptoServiceProvider object 
            // with the specified key and IV. 
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

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
            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptString_Aes(string cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold 
            // the decrypted text. 
            byte[] cipher = Convert.FromBase64String(cipherText);
            string plaintext = null;

            // Create an AesCryptoServiceProvider object 
            // with the specified key and IV. 
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipher))
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
    }
}