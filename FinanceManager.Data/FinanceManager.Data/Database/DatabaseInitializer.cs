using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Data.Database
{
    /// <summary>
    /// 数据库初始化器 —— 应用启动时调用，确保 LocalDB 数据库及表结构就绪。
    /// 幂等设计：多次调用不会重复初始化。
    /// </summary>
    public static class DatabaseInitializer
    {
        /// <summary>是否已完成初始化</summary>
        private static bool _initialized;
        /// <summary>是否已完成初始化（外部只读）</summary>
        public static bool IsInitialized => _initialized;

        /// <summary>
        /// 执行数据库初始化（幂等，多次调用不会重复初始化）。
        /// 触发 DatabaseManager 单例，其构造函数内部自动检查并建库、建表、插入默认分类。
        /// </summary>
        public static void Initialize()
        {
            if (_initialized) return;

            // 触发 DatabaseManager 单例：
            //   → 连接 master 检查 FinanceManager 库是否存在，不存在则 CREATE DATABASE
            //   → 逐表检查并建表（users, categories, records, budgets, category_budgets, templates）
            //   → 插入默认分类数据（9个支出 + 5个收入）
            var dbManager = DatabaseManager.Instance;
            _initialized = true;
        }
    }
}
