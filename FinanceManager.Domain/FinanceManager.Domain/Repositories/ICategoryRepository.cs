using FinanceManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryEntity>> GetByUserIdAsync(int userId);
        Task<IEnumerable<CategoryEntity>> GetByTypeAsync(int userId, int type);
        Task<CategoryEntity> GetByIdAsync(int id);
        Task<int> InsertAsync(CategoryEntity entity);
        Task UpdateAsync(CategoryEntity entity);
        Task DeleteAsync(int id);
        Task<int> GetCustomCountByUserIdAsync(int userId);
    }
}
