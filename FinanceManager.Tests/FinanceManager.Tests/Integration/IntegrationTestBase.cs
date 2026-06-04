using FinanceManager.Data.Database;
using FinanceManager.Data.Repositories;
using FinanceManager.Data.Services;
using FinanceManager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FinanceManager.Tests.Integration
{
    /// <summary>
    /// 集成测试基类 —— 复用主应用的 DatabaseManager 单例（不自己创建临时数据库）。
    /// 每个测试前创建一个随机命名的隔离测试用户，测试后按外键顺序清理。
    /// </summary>
    [TestClass]
    public abstract class IntegrationTestBase
    {
        /// <summary>当前测试用户ID</summary>
        protected int TestUserId;
        /// <summary>当前测试用户名（随机生成，防冲突）</summary>
        protected string TestUsername;

        /// <summary>数据库连接字符串，从 DatabaseManager 单例获取</summary>
        protected string ConnectionString;

        // ===== 真实仓储（不走 Mock）=====
        protected UserRepository UserRepo;
        protected RecordRepository RecordRepo;
        protected CategoryRepository CategoryRepo;
        protected TemplateRepository TemplateRepo;
        protected BudgetRepository BudgetRepo;
        protected CategoryBudgetRepository CatBudgetRepo;

        // ===== 真实服务 =====
        protected UserService UserService;
        protected RecordService RecordService;
        protected CategoryService CategoryService;
        protected TemplateService TemplateService;
        protected BudgetService BudgetService;

        /// <summary>
        /// 每个测试前：确保主应用数据库已初始化 → 创建隔离测试用户 → 初始化仓储和服务
        /// </summary>
        [TestInitialize]
        public virtual async Task SetUp()
        {
            // 复用主应用已有的数据库（不自己建临时库，避免 SQL CE 本地 DLL 加载问题）
            DatabaseInitializer.Initialize();
            ConnectionString = DatabaseManager.Instance.ConnectionString;

            // 创建隔离的测试用户（随机用户名，避开正常用户）
            TestUsername = $"test_{Guid.NewGuid():N}".Substring(0, 20);
            var rawPw = "123456";
            var hashedPw = FinanceManager.Common.Helpers.EncryptionHelper.HashPassword(rawPw);

            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = @"
                    INSERT INTO users (username, password, currency, ai_suggestion_enabled, created_at, status)
                    VALUES (@u, @p, 'CNY', 0, @now, 0)";
                cmd.Parameters.AddWithValue("@u", TestUsername);
                cmd.Parameters.AddWithValue("@p", hashedPw);
                cmd.Parameters.AddWithValue("@now", DateTime.Now);
                cmd.ExecuteNonQuery();

                cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT @@IDENTITY";
                TestUserId = (cmd.ExecuteScalar() is DBNull ? 0 : Convert.ToInt32(cmd.ExecuteScalar()));
            }

            // 初始化仓储和服务（全部走真实数据库，不 Mock）
            UserRepo = new UserRepository(ConnectionString);
            RecordRepo = new RecordRepository(ConnectionString);
            CatBudgetRepo = new CategoryBudgetRepository(ConnectionString);
            CategoryRepo = new CategoryRepository(ConnectionString);
            BudgetRepo = new BudgetRepository(ConnectionString);
            TemplateRepo = new TemplateRepository(ConnectionString);

            UserService = new UserService(UserRepo);
            RecordService = new RecordService(RecordRepo);
            CategoryService = new CategoryService(CategoryRepo);
            TemplateService = new TemplateService(TemplateRepo);
            BudgetService = new BudgetService(BudgetRepo, CatBudgetRepo);
        }

        /// <summary>
        /// 每个测试后：按外键依赖顺序清理所有测试用户数据
        /// </summary>
        [TestCleanup]
        public virtual void TearDown()
        {
            try
            {
                using (var conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    DeleteByUser(conn, "records");
                    DeleteByUser(conn, "templates");
                    DeleteByUser(conn, "category_budgets");
                    DeleteByUser(conn, "budgets");
                    DeleteByUser(conn, "categories", "AND is_default = 0");
                    DeleteByUser(conn, "users");
                }
            }
            catch { /* 清理失败不影响测试结果 */ }
        }

        /// <summary>删除指定表中属于测试用户的所有数据</summary>
        private void DeleteByUser(SqlConnection conn, string table, string extraWhere = "")
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = $"DELETE FROM {table} WHERE user_id = @uid {extraWhere}";
            cmd.Parameters.AddWithValue("@uid", TestUserId);
            cmd.ExecuteNonQuery();
        }
    }
}
