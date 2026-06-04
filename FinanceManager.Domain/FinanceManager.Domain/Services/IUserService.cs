using FinanceManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services
{
    public interface IUserService
    {
        Task<UserEntity> GetUserByIdAsync(int id);
        Task<UserEntity> GetUserByUsernameAsync(string username);
        Task<int> RegisterUserAsync(UserEntity user);
        Task<UserEntity> LoginAsync(string username, string password);
        Task UpdateUserAsync(UserEntity user);
        Task UpdateLastLoginTimeAsync(int userId);
        Task UpdateAiSuggestionEnabledAsync(int userId, bool enabled);
        Task<bool> ValidatePasswordAsync(string username, string password);
    }
}
