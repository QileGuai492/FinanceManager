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

namespace FinanceManager.Tests.ViewModels
{
    [TestClass]
    public class UserViewModelTests
    {
        private Mock<IUserService> _userServiceMock;
        private UserViewModel _vm;

        [TestInitialize]
        public void SetUp()
        {
            _userServiceMock = new Mock<IUserService>();
            _vm = new UserViewModel(_userServiceMock.Object);
        }

        /// <summary>1.2 用户名和密码为空 → ErrorMessage 不为空</summary>
        [TestMethod]
        public async Task LoginCommand_BothEmpty_ShowsError()
        {
            _vm.Username = "";
            _vm.Password = "";

            _vm.LoginCommand.Execute(null);
            // 异步命令通过 Task.Run 执行，等待一小段时间
            await Task.Delay(100);

            Assert.AreNotEqual(string.Empty, _vm.ErrorMessage);
            Assert.IsFalse(_vm.IsLoggedIn);
        }

        /// <summary>1.2 用户名为空 → ErrorMessage 不为空</summary>
        [TestMethod]
        public async Task LoginCommand_EmptyUsername_ShowsError()
        {
            _vm.Username = "";
            _vm.Password = "123456";

            _vm.LoginCommand.Execute(null);
            await Task.Delay(100);

            Assert.AreNotEqual(string.Empty, _vm.ErrorMessage);
        }

        /// <summary>1.3 密码为空 → ErrorMessage 不为空</summary>
        [TestMethod]
        public async Task LoginCommand_EmptyPassword_ShowsError()
        {
            _vm.Username = "testuser";
            _vm.Password = "";

            _vm.LoginCommand.Execute(null);
            await Task.Delay(100);

            Assert.AreNotEqual(string.Empty, _vm.ErrorMessage);
        }

        /// <summary>1.1 正常登录 → IsLoggedIn 为 true</summary>
        [TestMethod]
        public async Task LoginCommand_ValidCredentials_LogsIn()
        {
            var user = new UserEntity { Id = 5, Username = "testuser" };
            _userServiceMock.Setup(s => s.LoginAsync("testuser", "123456"))
                            .ReturnsAsync(user);

            _vm.Username = "testuser";
            _vm.Password = "123456";

            _vm.LoginCommand.Execute(null);
            await Task.Delay(200);

            Assert.IsTrue(_vm.IsLoggedIn);
            Assert.AreEqual(string.Empty, _vm.ErrorMessage);
        }

        /// <summary>1.4 错误密码 → ErrorMessage 含"错误"</summary>
        [TestMethod]
        public async Task LoginCommand_WrongPassword_ShowsError()
        {
            _userServiceMock.Setup(s => s.LoginAsync("testuser", "wrong"))
                            .ReturnsAsync((UserEntity)null);

            _vm.Username = "testuser";
            _vm.Password = "wrong";

            _vm.LoginCommand.Execute(null);
            await Task.Delay(200);

            Assert.IsFalse(_vm.IsLoggedIn);
            StringAssert.Contains(_vm.ErrorMessage, "用户名或密码错误");
        }

        #region 注册测试

        /// <summary>1.5 正常注册 → 自动登录</summary>
        [TestMethod]
        public async Task RegisterCommand_ValidData_RegistersAndLogsIn()
        {
            _userServiceMock.Setup(s => s.GetUserByUsernameAsync("newuser"))
                            .ReturnsAsync((UserEntity)null);
            _userServiceMock.Setup(s => s.RegisterUserAsync(It.IsAny<UserEntity>()))
                            .ReturnsAsync(1);
            var newUser = new UserEntity { Id = 2, Username = "newuser" };
            _userServiceMock.Setup(s => s.LoginAsync("newuser", "123456"))
                            .ReturnsAsync(newUser);

            _vm.Username = "newuser";
            _vm.Password = "123456";
            _vm.ConfirmPassword = "123456";

            _vm.RegisterCommand.Execute(null);
            await Task.Delay(200);

            Assert.IsTrue(_vm.IsLoggedIn);
        }

        /// <summary>1.6 两次密码不一致 → 提示错误</summary>
        [TestMethod]
        public async Task RegisterCommand_PasswordMismatch_ShowsError()
        {
            _vm.Username = "newuser";
            _vm.Password = "123456";
            _vm.ConfirmPassword = "654321";

            _vm.RegisterCommand.Execute(null);
            await Task.Delay(100);

            StringAssert.Contains(_vm.ErrorMessage, "不一致");
        }

        /// <summary>1.7 用户名超长 (>20) → 提示错误</summary>
        [TestMethod]
        public async Task RegisterCommand_UsernameTooLong_ShowsError()
        {
            _vm.Username = new string('a', 21); // 21个字符，超过20上限
            _vm.Password = "123456";
            _vm.ConfirmPassword = "123456";

            _vm.RegisterCommand.Execute(null);
            await Task.Delay(100);

            StringAssert.Contains(_vm.ErrorMessage, "最长");
        }

        #endregion

        /// <summary>1.9 退出登录 → IsLoggedIn 为 false</summary>
        [TestMethod]
        public void LogoutCommand_SetsIsLoggedIn_False()
        {
            // 先模拟已登录状态
            FinanceManager.Common.App.CurrentUserId = 1;
            _vm.GetType().GetProperty("IsLoggedIn")?.SetValue(_vm, true);

            _vm.LogoutCommand.Execute(null);

            Assert.IsFalse(_vm.IsLoggedIn);
            Assert.AreEqual(0, FinanceManager.Common.App.CurrentUserId);
        }
    }
}
