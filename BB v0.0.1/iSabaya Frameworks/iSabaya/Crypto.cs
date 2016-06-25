using System;
using System.IO;
using System.Security.Cryptography;
using iSabaya;
using System.Configuration;

namespace iSabaya
{
    public class Crypto
    {
        public static string EncryptRijndael(string plainText, byte[] keyBytes, byte[] ivBytes)
        {
            byte[] encrypted = EncryptStringToBytes(plainText, keyBytes, ivBytes);
            return Convert.ToBase64String(encrypted);
        }

        public static void EncryptFileRijndael(string inputFile, string outputFile, byte[] keyBytes, byte[] ivBytes)
        {
            try
            {
                var cryptFile = outputFile;
                var fsCrypt = new FileStream(cryptFile, FileMode.Create);
                var rmCrypto = new RijndaelManaged();
                var cs = new CryptoStream(fsCrypt, rmCrypto.CreateEncryptor(keyBytes, ivBytes), CryptoStreamMode.Write);
                var fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);

                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch (Exception exc)
            {
                throw new iSabayaException(exc.GetAllMessages());
            }
        }

        public static string DecryptRijndael(string cipheredText, byte[] keyBytes, byte[] ivBytes)
        {
                byte[] responseByte = Convert.FromBase64String(cipheredText);
                return DecryptStringFromBytes(responseByte, keyBytes, ivBytes);
        }

        public static void DecryptFileRijndael(string inputFile, string outputFile, byte[] keyBytes, byte[] ivBytes)
        {
            try
            {
                var fsCrypt = new FileStream(inputFile, FileMode.Open);
                var rmCrypto = new RijndaelManaged();
                var cs = new CryptoStream(fsCrypt, rmCrypto.CreateDecryptor(keyBytes, ivBytes), CryptoStreamMode.Read);
                var fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch (Exception exc)
            {
                throw new iSabayaException(exc.GetAllMessages());
            }
        }


        private static byte[] EncryptStringToBytes(string plainText, byte[] keyBytes, byte[] ivBytes)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (keyBytes == null || keyBytes.Length <= 0)
                throw new ArgumentNullException("key");
            if (ivBytes == null || ivBytes.Length <= 0)
                throw new ArgumentNullException("key");
            byte[] encrypted;
            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = keyBytes;
                rijAlg.IV = ivBytes;

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

        private static string DecryptStringFromBytes(byte[] cipherText, byte[] keyBytes, byte[] ivBytes)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (keyBytes == null || keyBytes.Length <= 0)
                throw new ArgumentNullException("key");
            if (ivBytes == null || ivBytes.Length <= 0)
                throw new ArgumentNullException("key");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext;

            // Create an Rijndael object
            // with the specified key and IV.
            using (Rijndael rijAlg = Rijndael.Create())
            {
                rijAlg.Key = keyBytes;
                rijAlg.IV = ivBytes;

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
    }
}
