using FinanceManager.Common.Helpers;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Data.Services
{
    /// <summary>用户服务实现 —— 处理登录验证、注册、密码校验和用户信息更新</summary>
    public class UserService : IUserService
    {
        /// <summary>用户仓储接口（依赖注入）</summary>
        private readonly IUserRepository _repo;

        /// <summary>构造函数：注入 IUserRepository 实例</summary>
        public UserService(IUserRepository repo) => _repo = repo;

        public async Task<UserEntity> GetUserByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<UserEntity> GetUserByUsernameAsync(string username)
        {
            return await _repo.GetByUsernameAsync(username);
        }

        public async Task<int> RegisterUserAsync(UserEntity user)
        {
            user.Password = EncryptionHelper.HashPassword(user.Password);
            user.CreatedAt = DateTime.Now;
            user.Status = 0;
            return await _repo.InsertAsync(user);
        }

        public async Task<UserEntity> LoginAsync(string username, string password)
        {
            var entity = await _repo.GetByUsernameAsync(username);
            if (entity == null) return null;
            if (!EncryptionHelper.VerifyPassword(password, entity.Password)) return null;
            await _repo.UpdateLastLoginTimeAsync(entity.Id);
            return entity;
        }

        public async Task UpdateUserAsync(UserEntity user)
        {
            await _repo.UpdateAsync(user);
        }

        public async Task UpdateLastLoginTimeAsync(int userId) =>
            await _repo.UpdateLastLoginTimeAsync(userId);

        public async Task UpdateAiSuggestionEnabledAsync(int userId, bool enabled) =>
            await _repo.UpdateAiSuggestionEnabledAsync(userId, enabled);

        public async Task<bool> ValidatePasswordAsync(string username, string password)
        {
            var entity = await _repo.GetByUsernameAsync(username);
            if (entity == null) return false;
            return EncryptionHelper.VerifyPassword(password, entity.Password);
        }
    }
}
