using System;
using System.IO;
using System.Security.Cryptography;
using iSabaya;
using System.Configuration;

namespace TestORM
{
    public class MapCipher
    {
        private static string KeyBase64 = "eoNRr2w9PhU7IrgQtlco2nFZQx2K42llF2n/ndpjpBw=";
        private static string InitialVectorBase64 = "0R5fFIyRkX4ZsOFoLFtyVA==";
        private static readonly byte[] KeyByte = Convert.FromBase64String(MapCipher.KeyBase64);
        private static readonly byte[] IVByte = Convert.FromBase64String(MapCipher.InitialVectorBase64);

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

        // Sawangchai
        public static void EncryptFile(string inputFile, string outputFile)
        {
            try
            {
                byte[] key = KeyByte;
                byte[] keyIV = IVByte;

                var cryptFile = outputFile;
                var fsCrypt = new FileStream(cryptFile, FileMode.Create);
                var rmCrypto = new RijndaelManaged();
                var cs = new CryptoStream(fsCrypt,
                                                   rmCrypto.CreateEncryptor(key, keyIV),
                                                   CryptoStreamMode.Write);
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

        // Sawangchai
        public static void DecryptFile(string inputFile, string outputFile)
        {
            try
            {
                byte[] key = KeyByte;
                byte[] keyIV = IVByte;

                var fsCrypt = new FileStream(inputFile, FileMode.Open);
                var rmCrypto = new RijndaelManaged();
                var cs = new CryptoStream(fsCrypt,
                                                   rmCrypto.CreateDecryptor(key, keyIV),
                                                   CryptoStreamMode.Read);
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
    }
}
