using System.Security.Cryptography;
using System.Text;

namespace ShopMVC8.Helper
{
    public static class DataEncryptionExtensions
    {
        #region [Hashing Extension]
        public static string ToSHA256Hash(this string password, string? saltKey)
        {
            var sha256 = SHA256.Create();
            byte[] encryptedSHA256 = sha256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(password, saltKey)));
            sha256.Clear();

            return Convert.ToBase64String(encryptedSHA256);
        }

        public static string ToSHA512Hash(this string password, string? saltKey)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] encryptedSHA512 = sha512.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(password, saltKey)));
            sha512.Clear();

            return Convert.ToBase64String(encryptedSHA512);
        }
        
        public static string ToMd5Hash(this string password, string? saltKey)
        {
            byte[] data = MD5.HashData(Encoding.UTF8.GetBytes(string.Concat(password, saltKey)));
            StringBuilder sBuilder = new();
            for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
            return sBuilder.ToString(); 
        }

        #endregion

    }
}

