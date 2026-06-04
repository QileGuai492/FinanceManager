using FinanceManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<UserEntity> GetByIdAsync(int id);
        Task<UserEntity> GetByUsernameAsync(string username);
        Task<int> InsertAsync(UserEntity entity);
        Task UpdateAsync(UserEntity entity);
        Task UpdateLastLoginTimeAsync(int userId);
        Task UpdateAiSuggestionEnabledAsync(int userId, bool enabled);
    }
}
