using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Services
{
    public interface ISyncService
    {
        Task SyncPushAsync(int userId);
        Task SyncPullAsync(int userId);
        Task FullSyncAsync(int userId);
    }
}
