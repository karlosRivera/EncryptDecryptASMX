using System;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace EncryptClass
{

    /// <summary>
    /// Summary description for EncryptClass.
    /// </summary>
    public class EncryptClass
    {
        DESCryptoServiceProvider rj;
        byte[] key = new byte[] { 11, 9, 3, 4, 1, 8, 12, 7 };
        byte[] IV = new byte[] { 1, 8, 7, 16, 1, 9, 0, 3 };
        public EncryptClass()
        {
            //
            // TODO: Add constructor logic here
            //
            rj = new DESCryptoServiceProvider();
        }

        // for encryption
        public string custEncrypt(string message)
        {
            //create a memory stream
            MemoryStream ciphertextmem = new MemoryStream();
            //create a crypto stream in write mode
            CryptoStream crystm = new CryptoStream(ciphertextmem, rj.CreateEncryptor(key, IV), CryptoStreamMode.Write);
            //Encode the passed plain text string into Unicode byte stream
            Byte[] plaintextbyte = new UnicodeEncoding().GetBytes(message);
            //Write the plaintext byte stream to CryptoStream
            crystm.Write(plaintextbyte, 0, plaintextbyte.Length);
            //don't forget to close the stream
            crystm.Close();
            //Extract the ciphertext byte stream and close the MemoryStream
            Byte[] ciphertextbyte = ciphertextmem.ToArray();
            ciphertextmem.Close();
            //Encode the ciphertext byte into Unicode string
            string ciphertext = new UnicodeEncoding().GetString(ciphertextbyte);
            return ciphertext;
            //return "encry " + message;


        }

        // for decryption
        public string custDecrypt(string message)
        {
            //Create a memory stream from which CryptoStream will read the cipher text
            MemoryStream ciphertextmem = new MemoryStream(new UnicodeEncoding().GetBytes(message));

            //Create a CryptoStream in Read Mode; initialise with the Rijndael's Decryptor ICryptoTransform
            CryptoStream crystm = new CryptoStream(ciphertextmem, rj.CreateDecryptor(key, IV), CryptoStreamMode.Read);

            //Create a temporary memory stream to which we will copy the 
            //plaintext byte array from CryptoStream

            MemoryStream plaintextmem = new MemoryStream();
            do
            {
                //Create a byte array into which we will read the plaintext 
                //from CryptoStream
                Byte[] buf = new Byte[100];

                //read the plaintext from CryptoStream
                int actualbytesread = crystm.Read(buf, 0, 100);

                //if we have reached the end of stream quit the loop
                if (0 == actualbytesread)
                    break;

                //copy the plaintext byte array to MemoryStream
                plaintextmem.Write(buf, 0, actualbytesread);

            } while (true);

            //don't forget to close the streams
            crystm.Close();
            ciphertextmem.Close();

            //Extract the plaintext byte stream and close the MemoryStream
            Byte[] plaintextbyte = plaintextmem.ToArray();
            plaintextmem.Close();

            //Encode the plaintext byte into Unicode string
            string plaintext = new UnicodeEncoding().GetString(plaintextbyte);

            return plaintext;

            //return "decry "+ message;
        }
    }

}
