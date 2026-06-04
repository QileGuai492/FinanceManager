using FinanceManager.Data.Services;
using FinanceManager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Tests.Services
{
    [TestClass]
    public class UserServiceTests : TestBase
    {
        private UserService CreateService() => new UserService(UserRepoMock.Object);

        #region 登录测试

        /// <summary>1.1 正常登录：输入已注册用户名密码 → 返回用户实体</summary>
        [TestMethod]
        public async Task LoginAsync_ValidCredentials_ReturnsUser()
        {
            // Arrange
            var username = "testuser";
            var password = "123456";
            var hashedPw = FinanceManager.Common.Helpers.EncryptionHelper.HashPassword(password);
            var user = new UserEntity { Id = 1, Username = username, Password = hashedPw };

            UserRepoMock.Setup(r => r.GetByUsernameAsync(username))
                        .ReturnsAsync(user);
            UserRepoMock.Setup(r => r.UpdateLastLoginTimeAsync(It.IsAny<int>()))
                        .Returns(Task.CompletedTask);

            var service = CreateService();

            // Act
            var result = await service.LoginAsync(username, password);

            // Assert
            Assert.IsNotNull(result, "正确凭据应返回用户实体");
            Assert.AreEqual(username, result.Username);
        }

        /// <summary>1.2 空用户名：返回 null</summary>
        [TestMethod]
        public async Task LoginAsync_EmptyUsername_ReturnsNull()
        {
            UserRepoMock.Setup(r => r.GetByUsernameAsync(string.Empty))
                        .ReturnsAsync((UserEntity)null);
            var service = CreateService();

            var result = await service.LoginAsync(string.Empty, "123456");

            Assert.IsNull(result);
        }

        /// <summary>1.3 空密码：返回 null</summary>
        [TestMethod]
        public async Task LoginAsync_EmptyPassword_ReturnsNull()
        {
            var username = "testuser";
            var hashedPw = FinanceManager.Common.Helpers.EncryptionHelper.HashPassword("123456");
            var user = new UserEntity { Id = 1, Username = username, Password = hashedPw };

            UserRepoMock.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync(user);
            var service = CreateService();

            var result = await service.LoginAsync(username, "");

            Assert.IsNull(result, "空密码应返回 null");
        }

        /// <summary>1.4 错误密码：返回 null</summary>
        [TestMethod]
        public async Task LoginAsync_WrongPassword_ReturnsNull()
        {
            var username = "testuser";
            var hashedPw = FinanceManager.Common.Helpers.EncryptionHelper.HashPassword("correct");
            var user = new UserEntity { Id = 1, Username = username, Password = hashedPw };

            UserRepoMock.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync(user);
            var service = CreateService();

            var result = await service.LoginAsync(username, "wrongpassword");

            Assert.IsNull(result, "错误密码应返回 null");
        }

        #endregion

        #region 注册测试

        /// <summary>1.5 注册：正常注册成功并返回新用户 ID</summary>
        [TestMethod]
        public async Task RegisterUserAsync_ValidData_ReturnsNewId()
        {
            var newUser = new UserEntity { Username = "newuser", Password = "123456" };
            UserRepoMock.Setup(r => r.InsertAsync(It.IsAny<UserEntity>())).ReturnsAsync(1);

            var service = CreateService();
            var id = await service.RegisterUserAsync(newUser);

            Assert.AreEqual(1, id);
            UserRepoMock.Verify(r => r.InsertAsync(It.Is<UserEntity>(u =>
                u.Username == "newuser" && u.Status == 0)), Times.Once);
        }

        /// <summary>1.6 注册时密码会被哈希处理</summary>
        [TestMethod]
        public async Task RegisterUserAsync_HashesPassword()
        {
            var rawPassword = "123456";
            var newUser = new UserEntity { Username = "newuser", Password = rawPassword };
            UserRepoMock.Setup(r => r.InsertAsync(It.IsAny<UserEntity>())).ReturnsAsync(1);

            var service = CreateService();
            await service.RegisterUserAsync(newUser);

            // 验证传入仓储的密码已被哈希
            UserRepoMock.Verify(r => r.InsertAsync(It.Is<UserEntity>(u =>
                u.Password != rawPassword && u.Password.Length > 20)), Times.Once);
        }

        #endregion

        #region 验证密码

        /// <summary>1.9 验证密码：正确密码返回 true</summary>
        [TestMethod]
        public async Task ValidatePasswordAsync_CorrectPassword_ReturnsTrue()
        {
            var username = "testuser";
            var rawPw = "123456";
            var hashedPw = FinanceManager.Common.Helpers.EncryptionHelper.HashPassword(rawPw);
            var user = new UserEntity { Id = 1, Username = username, Password = hashedPw };

            UserRepoMock.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync(user);
            var service = CreateService();

            var result = await service.ValidatePasswordAsync(username, rawPw);

            Assert.IsTrue(result);
        }

        /// <summary>错误密码返回 false</summary>
        [TestMethod]
        public async Task ValidatePasswordAsync_WrongPassword_ReturnsFalse()
        {
            var username = "testuser";
            var hashedPw = FinanceManager.Common.Helpers.EncryptionHelper.HashPassword("correct");
            var user = new UserEntity { Id = 1, Username = username, Password = hashedPw };

            UserRepoMock.Setup(r => r.GetByUsernameAsync(username)).ReturnsAsync(user);
            var service = CreateService();

            var result = await service.ValidatePasswordAsync(username, "wrong");

            Assert.IsFalse(result);
        }

        #endregion
    }
}
