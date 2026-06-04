using FinanceManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services
{
    public interface ITemplateService
    {
        Task<IEnumerable<TemplateEntity>> GetTemplatesAsync(int userId);
        Task<IEnumerable<TemplateEntity>> GetFavoriteTemplatesAsync(int userId);
        Task<TemplateEntity> GetTemplateByIdAsync(int id);
        Task<int> AddTemplateAsync(TemplateEntity template);
        Task UpdateTemplateAsync(TemplateEntity template);
        Task DeleteTemplateAsync(int id);
        Task IncrementUseCountAsync(int id);
        Task<int> GetTemplateCountAsync(int userId);
    }
}
