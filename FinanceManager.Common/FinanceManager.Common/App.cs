using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Common
{
    /// <summary>应用全局状态，持有当前登录用户信息</summary>
    public static class App
    {
        public static int CurrentUserId { get; set; }
        public static string CurrentUsername { get; set; } = string.Empty;
        public static string CurrentUserCurrency { get; set; } = "CNY";
        public static bool IsLoggedIn => CurrentUserId > 0;

        public static void Logout()
        {
            CurrentUserId = 0;
            CurrentUsername = string.Empty;
            CurrentUserCurrency = "CNY";
        }
    }
}
