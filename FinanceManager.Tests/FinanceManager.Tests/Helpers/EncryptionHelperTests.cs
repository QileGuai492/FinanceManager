using FinanceManager.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Tests.Helpers
{
    [TestClass]
    public class EncryptionHelperTests
    {
        #region 密码哈希测试

        /// <summary>密码哈希不为空且与原密码不同</summary>
        [TestMethod]
        public void HashPassword_ReturnsNonEmptyString()
        {
            var hash = EncryptionHelper.HashPassword("mypassword");

            Assert.IsFalse(string.IsNullOrEmpty(hash));
            Assert.AreNotEqual("mypassword", hash);
        }

        /// <summary>相同密码生成不同哈希（盐值随机）</summary>
        [TestMethod]
        public void HashPassword_SameInput_DifferentHashes()
        {
            var hash1 = EncryptionHelper.HashPassword("samepassword");
            var hash2 = EncryptionHelper.HashPassword("samepassword");

            // BCrypt 每次生成不同盐值，两次哈希不应相同
            Assert.AreNotEqual(hash1, hash2);
        }

        /// <summary>验证正确密码返回 true</summary>
        [TestMethod]
        public void VerifyPassword_CorrectPassword_ReturnsTrue()
        {
            var password = "123456";
            var hash = EncryptionHelper.HashPassword(password);

            Assert.IsTrue(EncryptionHelper.VerifyPassword(password, hash));
        }

        /// <summary>验证错误密码返回 false</summary>
        [TestMethod]
        public void VerifyPassword_WrongPassword_ReturnsFalse()
        {
            var hash = EncryptionHelper.HashPassword("correct");

            Assert.IsFalse(EncryptionHelper.VerifyPassword("wrong", hash));
        }

        /// <summary>空密码处理</summary>
        [TestMethod]
        public void HashPassword_EmptyString_StillHashes()
        {
            var hash = EncryptionHelper.HashPassword("");

            Assert.IsFalse(string.IsNullOrEmpty(hash));
            Assert.IsTrue(EncryptionHelper.VerifyPassword("", hash));
        }

        #endregion

        #region AES 加解密测试

        /// <summary>6.9 API Key 加密后解密一致</summary>
        [TestMethod]
        public void Encrypt_Decrypt_RoundTrip_ReturnsOriginal()
        {
            var original = "sk-abc123def456";

            var encrypted = EncryptionHelper.Encrypt(original);
            var decrypted = EncryptionHelper.Decrypt(encrypted);

            Assert.AreNotEqual(original, encrypted, "加密后应与原文不同");
            Assert.AreEqual(original, decrypted, "解密后应还原原文");
        }

        /// <summary>空字符串加密解密</summary>
        [TestMethod]
        public void Encrypt_Decrypt_EmptyString_ReturnsEmpty()
        {
            var encrypted = EncryptionHelper.Encrypt("");
            var decrypted = EncryptionHelper.Decrypt("");

            Assert.AreEqual("", encrypted);
            Assert.AreEqual("", decrypted);
        }

        /// <summary>Null 加密解密</summary>
        [TestMethod]
        public void Encrypt_Decrypt_Null_ReturnsEmpty()
        {
            var encrypted = EncryptionHelper.Encrypt(null);
            var decrypted = EncryptionHelper.Decrypt(null);

            Assert.AreEqual("", encrypted);
            Assert.AreEqual("", decrypted);
        }

        /// <summary>长字符串加密解密</summary>
        [TestMethod]
        public void Encrypt_Decrypt_LongString_ReturnsOriginal()
        {
            var original = new string('x', 1000); // 1000个字符

            var encrypted = EncryptionHelper.Encrypt(original);
            var decrypted = EncryptionHelper.Decrypt(encrypted);

            Assert.AreEqual(original, decrypted);
        }

        #endregion
    }
}
