using FinanceManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Repositories
{
    public interface ITemplateRepository
    {
        Task<IEnumerable<TemplateEntity>> GetByUserIdAsync(int userId);
        Task<IEnumerable<TemplateEntity>> GetFavoriteByUserIdAsync(int userId);
        Task<TemplateEntity> GetByIdAsync(int id);
        Task<int> InsertAsync(TemplateEntity entity);
        Task UpdateAsync(TemplateEntity entity);
        Task DeleteAsync(int id);
        Task IncrementUseCountAsync(int id);
        Task<int> GetCountByUserIdAsync(int userId);
    }
}
