using FinanceManager.Data.Database;
using FinanceManager.Data.Repositories;
using FinanceManager.Data.Services;
using FinanceManager.Common;
using FinanceManager.Forms;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinanceManager
{
    /// <summary>
    /// 应用程序入口类 —— 负责数据库初始化、自动登录检测、保存/读取用户ID配置。
    /// </summary>
    internal static class Program
    {
        /// <summary>持久化配置文件路径：%APPDATA%/FinanceManager/config.json，用于记住上次登录用户</summary>
        private static readonly string ConfigPath = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "FinanceManager", "config.json");

        /// <summary>
        /// 应用程序主入口：初始化数据库 → 检测自动登录 → 启动 LoginForm 或 MainForm
        /// </summary>
        [STAThread]
        static void Main()
        {
            DatabaseInitializer.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 尝试自动登录
            var savedUserId = LoadSavedUserId();
            if (savedUserId > 0)
            {
                var userService = new UserService(new UserRepository(DatabaseManager.Instance.ConnectionString));
                var user = userService.GetUserByIdAsync(savedUserId).Result;
                if (user != null)
                {
                    App.CurrentUserId = user.Id;
                    App.CurrentUsername = user.Username;
                    Application.Run(new MainForm());
                    return;
                }
            }

            // 无已保存用户或用户已不存在 → 显示登录页
            Application.Run(new LoginForm());
        }

        /// <summary>保存用户ID到配置文件，用于下次"记住登录"自动登录</summary>
        public static void SaveUserId(int userId)
        {
            var dir = Path.GetDirectoryName(ConfigPath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            File.WriteAllText(ConfigPath, JsonConvert.SerializeObject(new { LastUserId = userId }));
        }

        /// <summary>删除保存的用户ID配置文件（登出时调用）</summary>
        public static void ClearSavedUserId()
        {
            if (File.Exists(ConfigPath)) File.Delete(ConfigPath);
        }

        /// <summary>从配置文件读取上次登录的用户ID，用于自动登录。失败或不存在返回0</summary>
        private static int LoadSavedUserId()
        {
            if (!File.Exists(ConfigPath)) return 0;  // 没有配置文件
            try
            {
                var json = File.ReadAllText(ConfigPath);
                var obj = JsonConvert.DeserializeAnonymousType(json, new { LastUserId = 0 });
                return obj.LastUserId;
            }
            catch
            {
                return 0;  // 文件损坏或其他异常，安全返回0
            }
        }
    }
}
