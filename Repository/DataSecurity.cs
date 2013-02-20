using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Pogs.DataModel
{
    /// <summary>
    /// A utility class for encrypting and decrypting strings.
    /// </summary>
    /// <remarks>
    ///     Based on sample code found at: http://stackoverflow.com/questions/202011/encrypt-decrypt-string-in-net
    /// </remarks>
    internal static class DataSecurity
    {
        private static byte[] _saltX = Encoding.ASCII.GetBytes("CHANGE_BEFORE_COMPILING_A");

        /// <summary> 
        /// Encrypt the given string using AES.  The string can be decrypted using  
        /// DecryptStringAES().  The sharedSecret parameters must match. 
        /// </summary> 
        /// <param name="plainText">The text to encrypt.</param> 
        public static string EncryptStringAES(string plainText)
        {
            if (String.IsNullOrEmpty(plainText))
                plainText = String.Empty;

            string outStr = null;                       // Encrypted string to return 
            RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data. 

            try
            {
                byte[] salt = CreateSalt();

                // generate the key from the shared secret and the salt 
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(GetParameters(5), salt);

                // Create a RijndaelManaged object 
                // with the specified key and IV. 
                aesAlg = new RijndaelManaged();
                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

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
                    }

                    byte[] cipherArray = msEncrypt.ToArray();
                    byte[] result = new byte[salt.Length + cipherArray.Length];
                    
                    Buffer.BlockCopy(salt, 0, result, 0, salt.Length);
                    Buffer.BlockCopy(cipherArray, 0, result, salt.Length, cipherArray.Length);

                    outStr = Convert.ToBase64String(result);
                }
            }
            finally
            {
                // Clear the RijndaelManaged object. 
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            // Return the encrypted bytes from the memory stream. 
            return outStr;
        }

        private static byte[] CreateSalt()
        {
            Random r = new Random();

            byte[] result = new byte[8];

            r.NextBytes(result);

            return result;
        }

        /// <summary> 
        /// Decrypt the given string.  Assumes the string was encrypted using  
        /// EncryptStringAES(), using an identical sharedSecret. 
        /// </summary> 
        /// <param name="cipherText">The text to decrypt.</param> 
        public static string DecryptStringAES(string cipherText)
        {
            if (String.IsNullOrEmpty(cipherText))
                return cipherText;

            // Declare the RijndaelManaged object 
            // used to decrypt the data. 
            RijndaelManaged aesAlg = null;

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            try
            {
                byte[] allBytes = Convert.FromBase64String(cipherText);
                if (allBytes.Length < 8)
                    return cipherText;

                byte[] salt = new byte[8];
                byte[] cipherBytes = new byte[allBytes.Length - 8];

                Buffer.BlockCopy(allBytes, 0, salt, 0, 8);
                Buffer.BlockCopy(allBytes, 8, cipherBytes, 0, allBytes.Length - 8);

                // generate the key from the shared secret and the salt 
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(GetParameters(5), salt);

                // Create a RijndaelManaged object 
                // with the specified key and IV. 
                aesAlg = new RijndaelManaged();

                aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);
                
                // Create a decrytor to perform the stream transform. 
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                // Create the streams used for decryption.          
                                
                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string. 
                            plaintext = srDecrypt.ReadToEnd();
                    }
                }
            }
            catch (FormatException)
            {
                return cipherText;
            }
            catch (CryptographicException)
            {
                return cipherText;
            }
            finally
            {
                // Clear the RijndaelManaged object. 
                if (aesAlg != null)
                    aesAlg.Clear();
            }

            return plaintext;
        }

        private static string GetParameters(int p)
        {
            char[] result = "CHANGE_BEFORE_COMPILING_B".ToCharArray();

            for (int i = p; i >= 0; i--)
            {
                char x = result[i];
                result[i] = result[p - i];
                result[p - i] = x;

                char y = result[0];
                result[0] = result[8];
                result[8] = (char)((int)y + 1);
            }

            string nextResult = (new String(result) + new String(result)).Substring(2, 10);

            return Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(nextResult));
        } 
    }
}
