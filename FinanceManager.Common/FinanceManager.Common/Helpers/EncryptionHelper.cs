using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;// 需要安装BCrypt.Net-Next NuGet包；不能直接使用BCrypt.Net命名空间，否则会无法识别

namespace FinanceManager.Common.Helpers
{
    public static class EncryptionHelper
    {
        // AES加密密钥和IV（实际生产环境建议从配置读取，这里做示例）
        private static readonly byte[] AesKey = Encoding.UTF8.GetBytes("FinanceManagerAesKey32ByteLong12"); // 32位密钥
        private static readonly byte[] AesIv = Encoding.UTF8.GetBytes("FinanceManager16"); // 16位IV

        /// <summary>
        /// 密码哈希（BCrypt）
        /// </summary>
        public static string HashPassword(string password)
        {
            return BC.HashPassword(password, 10);
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        public static bool VerifyPassword(string password, string hash)
        {
            return BC.Verify(password, hash);
        }

        /// <summary>
        /// AES对称加密
        /// </summary>
        public static string Encrypt(string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) return "";

            using (var aes = Aes.Create())
            {
                aes.Key = AesKey;
                aes.IV = AesIv;
                var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        /// <summary>
        /// AES对称解密
        /// </summary>
        public static string Decrypt(string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) return "";

            using (var aes = Aes.Create())
            {
                aes.Key = AesKey;
                aes.IV = AesIv;
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
