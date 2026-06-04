using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace FinanceManager.Tests.Integration
{
    /// <summary>用户服务集成测试 —— 真实 LocalDB，测试注册→登录→修改→持久化全链路</summary>
    [TestClass]
    public class UserServiceIntegrationTests : IntegrationTestBase
    {
        [TestMethod]
        public async Task Login_ValidCredentials_ReturnsUser()
        {
            var user = await UserRepo.GetByIdAsync(TestUserId);
            var result = await UserService.LoginAsync(user.Username, "123456");
            Assert.IsNotNull(result);
            Assert.AreEqual(TestUserId, result.Id);
        }

        [TestMethod]
        public async Task Login_WrongPassword_ReturnsNull()
        {
            var user = await UserRepo.GetByIdAsync(TestUserId);
            var result = await UserService.LoginAsync(user.Username, "wrongpassword");
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateUser_ChangesUsername_Persisted()
        {
            var user = await UserRepo.GetByIdAsync(TestUserId);
            var newName = $"renamed_{Guid.NewGuid():N}".Substring(0, 20);
            user.Username = newName;
            await UserService.UpdateUserAsync(user);

            var reloaded = await UserRepo.GetByIdAsync(TestUserId);
            Assert.AreEqual(newName, reloaded.Username);
        }

        [TestMethod]
        public async Task UpdateAiSuggestion_Enabled_Persisted()
        {
            await UserService.UpdateAiSuggestionEnabledAsync(TestUserId, true);
            var user = await UserRepo.GetByIdAsync(TestUserId);
            Assert.IsTrue(user.AiSuggestionEnabled);
        }
    }
}
