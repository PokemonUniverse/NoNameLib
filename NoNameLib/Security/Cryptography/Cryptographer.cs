using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NoNameLib.Security.Cryptography
{
    /// <summary>
    /// Static class used for encryption and decryption of instance
    /// </summary>
    public class Cryptographer
    {
        static readonly Cryptographer instance;

        const string PASS_PHRASE = "Pas5pr@se"; // can be any string
        const string SALT_VALUE = "s41tValue"; // can be any string
        const string HASH_ALGORITHM = "SHA1"; // can be "MD5"
        const int PASSWORD_ITERATIONS = 2; // can be any number
        const string INIT_VECTOR = "@1B2c3D4e5F6g7H8"; // must be 16 bytes
        const int KEY_SIZE = 256; // can be 192 or 128

        static Cryptographer()
        {
            if (instance == null)
            {
                // first create the instance
                instance = new Cryptographer();
                // Then init the instance (the init needs the instance to fill it)
                instance.Init();
            }
        }

        private void Init()
        {
        }

        /// <summary>
        /// Encrypt a string using a MD5 hash
        /// </summary>
        /// <param name="input">The System.String instance to encrypt</param>
        /// <returns>An encrypted System.String instance</returns>
        public static string EncryptStringUsingMD5(string input)
        {
            string encryptedText = string.Empty;

            if (Instance.ArgumentIsEmpty(input, "input"))
            {
                // input is empty
            }
            else
            {
                // Convert the original string to array of Bytes
                byte[] valueToEncrypt = Encoding.Unicode.GetBytes(input);

                // This is one implementation of the abstract class MD5.
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] result = md5.ComputeHash(valueToEncrypt);

                // Convert the value so that it can be displayed
                encryptedText = Convert.ToBase64String(result);
            }

            return encryptedText;
        }

        /// <summary>
        /// Encrypts specified plaintext using Rijndael symmetric key algorithm
        /// and returns a base64-encoded result.
        /// </summary>
        /// <param name="plainText">
        /// Plaintext value to be encrypted.
        /// </param>
        /// <returns>
        /// Encrypted value formatted as a base64-encoded string.
        /// </returns>
        public static string EncryptStringUsingRijndael(string plainText)
        {
            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8 
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(INIT_VECTOR);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(SALT_VALUE);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            var password = new PasswordDeriveBytes(PASS_PHRASE,
                                                   saltValueBytes,
                                                   HASH_ALGORITHM,
                                                   PASSWORD_ITERATIONS);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(KEY_SIZE / 8);

            // Create uninitialized Rijndael encryption object.
            var symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes,
                                                                      initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            var memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            var cryptoStream = new CryptoStream(memoryStream,
                                                         encryptor,
                                                         CryptoStreamMode.Write);
            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            string cipherText = Convert.ToBase64String(cipherTextBytes);

            // Return encrypted string.
            return cipherText;
        }

        /// <summary>
        /// Decrypts specified ciphertext using Rijndael symmetric key algorithm.
        /// </summary>
        /// <returns>
        /// Decrypted string value.
        /// </returns>
        /// <remarks>
        /// Most of the logic in this function is similar to the Encrypt
        /// logic. In order for decryption to work, all parameters of this function
        /// - except cipherText value - must match the corresponding parameters of
        /// the Encrypt function which was called to generate the
        /// ciphertext.
        /// </remarks>
        public static string DecryptStringUsingRijndael(string cipherText)
        {
            // Convert strings defining encryption key characteristics into byte
            // arrays. Let us assume that strings only contain ASCII codes.
            // If strings include Unicode characters, use Unicode, UTF7, or UTF8
            // encoding.
            byte[] initVectorBytes = Encoding.ASCII.GetBytes(INIT_VECTOR);
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(SALT_VALUE);

            // Convert our ciphertext into a byte array.
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            // First, we must create a password, from which the key will be 
            // derived. This password will be generated from the specified 
            // passphrase and salt value. The password will be created using
            // the specified hash algorithm. Password creation can be done in
            // several iterations.
            var password = new PasswordDeriveBytes(PASS_PHRASE,
                                                   saltValueBytes,
                                                   HASH_ALGORITHM,
                                                   PASSWORD_ITERATIONS);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = password.GetBytes(KEY_SIZE / 8);

            // Create uninitialized Rijndael encryption object.
            var symmetricKey = new RijndaelManaged();

            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate decryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform decryptor = symmetricKey.CreateDecryptor(
                                                             keyBytes,
                                                             initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            var memoryStream = new MemoryStream(cipherTextBytes);

            // Define cryptographic stream (always use Read mode for encryption).
            var cryptoStream = new CryptoStream(memoryStream,
                                                          decryptor,
                                                          CryptoStreamMode.Read);

            // Since at this point we don't know what the size of decrypted data
            // will be, allocate the buffer long enough to hold ciphertext;
            // plaintext is never longer than ciphertext.
            var plainTextBytes = new byte[cipherTextBytes.Length];

            // Start decrypting.
            int decryptedByteCount = cryptoStream.Read(plainTextBytes,
                                                       0,
                                                       plainTextBytes.Length);

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert decrypted data into a string. 
            // Let us assume that the original plaintext string was UTF8-encoded.
            string plainText = Encoding.UTF8.GetString(plainTextBytes,
                                                       0,
                                                       decryptedByteCount);

            // Return decrypted string.   
            return plainText;
        }
    }
}
