using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace TermConfig_NewMask.Controllers
{
    public class EncryptionCtl
    {
        //http://dotnet-snippets.com/snippet/encrypt-and-decrypt-strings/205
        //http://dotnet-snippets.com/snippet/string-encrypt-decrypt/1704

        string Password = "krutecKR02";

        private static byte[] Encrypt(byte[] clearText, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearText, 0, clearText.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }

        public string Encrypt(string clearText) //, string Password)
        {
            byte[] clearData = Encoding.Unicode.GetBytes(clearText);
            PasswordDeriveBytes bytes = new PasswordDeriveBytes(Password, new byte[] { 50, 21, 32, 119, 19, 4, 56, 76, 23, 65, 58, 23, 43 });
            return Convert.ToBase64String(Encrypt(clearData, bytes.GetBytes(0x20), bytes.GetBytes(0x10)));
        }

        private static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }

        public string Decrypt(string cipherText) //, string Password)
        {
            try
            {
                byte[] cipherData = Convert.FromBase64String(cipherText);
                PasswordDeriveBytes bytes = new PasswordDeriveBytes(Password, new byte[] { 50, 21, 32, 119, 19, 4, 56, 76, 23, 65, 58, 23, 43 });
                //byte[] buffer2 = Decrypt(cipherData, bytes.GetBytes(0x20), bytes.GetBytes(0x10));
                //return Encoding.Unicode.GetString(buffer2);

                byte[] buffer2 = Decrypt(cipherData, bytes.GetBytes(0x20), bytes.GetBytes(0x10));
                return Encoding.Unicode.GetString(buffer2);
            }
            catch (Exception e) { return cipherText; }

            ////To implement encryption
            //string str2 = cs.Encrypt("YourPasswordKey", txtPassword.Text);


            ////To implement decryption
            //string text = this.txtPassword.Text;
            //str3 = cs.Encrypt("YourPasswordKey", text);
        }
    }
}
