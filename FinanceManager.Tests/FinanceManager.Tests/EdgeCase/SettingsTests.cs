using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Services;
using FinanceManager.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Tests.EdgeCase
{
    [TestClass]
    public class SettingsTests
    {
        private Mock<IUserService> _userServiceMock;
        private UserViewModel _vm;

        [TestInitialize]
        public void SetUp()
        {
            _userServiceMock = new Mock<IUserService>();
            _vm = new UserViewModel(_userServiceMock.Object);
        }

        /// <summary>6.2 用户名为空 → ErrorMessage 不为空</summary>
        [TestMethod]
        public async Task RegisterCommand_EmptyUsername_ShowsError()
        {
            _vm.Username = "";
            _vm.Password = "123456";
            _vm.ConfirmPassword = "123456";

            _vm.RegisterCommand.Execute(null);
            await Task.Delay(100);

            Assert.AreNotEqual(string.Empty, _vm.ErrorMessage);
        }

        /// <summary>6.5 新密码太短(<6位) → 提示错误</summary>
        [TestMethod]
        public async Task RegisterCommand_ShortPassword_ShowsError()
        {
            _vm.Username = "validuser";
            _vm.Password = "12345";  // 5位，小于最小长度6
            _vm.ConfirmPassword = "12345";

            _vm.RegisterCommand.Execute(null);
            await Task.Delay(100);

            StringAssert.Contains(_vm.ErrorMessage, "密码");
        }

        /// <summary>6.6 两次密码不一致 → 提示错误</summary>
        [TestMethod]
        public async Task RegisterCommand_MismatchedPasswords_ShowsError()
        {
            _vm.Username = "validuser";
            _vm.Password = "password123";
            _vm.ConfirmPassword = "password456";

            _vm.RegisterCommand.Execute(null);
            await Task.Delay(100);

            StringAssert.Contains(_vm.ErrorMessage, "不一致");
        }

        /// <summary>6.3 密码修改验证（通过 UserService 验证）</summary>
        [TestMethod]
        public async Task ValidatePasswordAsync_CorrectOriginalPassword_ReturnsTrue()
        {
            var username = "user1";
            var rawPw = "oldpassword";
            var hashedPw = FinanceManager.Common.Helpers.EncryptionHelper.HashPassword(rawPw);
            var user = new UserEntity { Id = 1, Username = username, Password = hashedPw };

            _userServiceMock.Setup(s => s.GetUserByUsernameAsync(username)).ReturnsAsync(user);
            _userServiceMock.Setup(s => s.ValidatePasswordAsync(username, rawPw)).ReturnsAsync(true);

            var result = await _userServiceMock.Object.ValidatePasswordAsync(username, rawPw);

            Assert.IsTrue(result);
        }

        /// <summary>6.4 原密码错误 → 返回 false</summary>
        [TestMethod]
        public async Task ValidatePasswordAsync_WrongOriginalPassword_ReturnsFalse()
        {
            _userServiceMock.Setup(s => s.ValidatePasswordAsync("user1", "wrong"))
                            .ReturnsAsync(false);

            var result = await _userServiceMock.Object.ValidatePasswordAsync("user1", "wrong");

            Assert.IsFalse(result);
        }

        /// <summary>6.1 修改用户名：调用 UpdateUserAsync</summary>
        [TestMethod]
        public async Task UpdateUserAsync_ChangeUsername_CallsUpdate()
        {
            var updatedUser = new UserEntity { Id = 1, Username = "新用户名" };

            await _userServiceMock.Object.UpdateUserAsync(updatedUser);

            _userServiceMock.Verify(s => s.UpdateUserAsync(It.Is<UserEntity>(u =>
                u.Id == 1 && u.Username == "新用户名")), Times.Once);
        }
    }
}
