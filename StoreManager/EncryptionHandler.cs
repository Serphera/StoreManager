using System;
using System.IO;
using System.Security.Cryptography;

public static class EncryptionHandler {

    private const int AES256KeySize = 256;
    private const int SaltSize = 16;


    public static bool AESEncryptFile(string path, byte[] password, byte[] salt) {

        using (var fileStream = new FileStream(path + ".encrypted", FileMode.Create)) {

            var key = GenerateKey(password, salt);

            using (var aes = new AesManaged()) {

                aes.KeySize = AES256KeySize;
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);
                aes.Padding = PaddingMode.PKCS7;
                aes.Mode = CipherMode.CBC;

                using (var cryptStream = new CryptoStream(fileStream, aes.CreateEncryptor(), CryptoStreamMode.Write)) {

                    using (var fileStreamIn = new FileStream(path, FileMode.Open)) {

                        var buffer = new byte[1];
                        int read;

                        try {

                            while ((read = fileStreamIn.Read(buffer, 0, buffer.Length)) > 0) {

                                cryptStream.Write(buffer, 0, read);
                            }

                            cryptStream.FlushFinalBlock();
                            return true;
                        }
                        catch (Exception) {

                            return false;
                        }
                    }
                }

            }
        }
    }


    public static byte[] AesDecryptFile(byte[] encrypted, byte[] password, byte[] salt) {


        byte[] decrypted = null;

        var key = GenerateKey(password, salt);


        Array.Clear(password, 0, password.Length);
        Array.Resize(ref password, 1);
        password = null;


        Array.Clear(salt, 0, salt.Length);
        Array.Resize(ref salt, 1);
        salt = null;

        using (var aes = new AesManaged()) {

            aes.KeySize = AES256KeySize;
            aes.Key = key.GetBytes(aes.KeySize / 8);
            aes.IV = key.GetBytes(aes.BlockSize / 8);
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;

            using (var memStream = new MemoryStream()) {

                using (var cryptStream = new CryptoStream(memStream, aes.CreateDecryptor(), CryptoStreamMode.Write)) {

                    cryptStream.Write(encrypted, 0, encrypted.Length);
                }

                decrypted = memStream.ToArray();
            }

            encrypted = null;

            return decrypted;
        }
    }


    public static Rfc2898DeriveBytes GenerateKey(byte[] password, byte[] salt) {

        return new Rfc2898DeriveBytes(password, salt, 52768);

    }

}