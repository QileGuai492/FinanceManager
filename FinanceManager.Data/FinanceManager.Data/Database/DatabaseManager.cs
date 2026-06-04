using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Data.Database
{
    /// <summary>
    /// 数据库管理器（单例） —— 负责 SQL Server LocalDB 实例的连接管理、建库、建表和默认数据初始化。
    /// 应用启动时通过 DatabaseInitializer.Initialize() 首次触发实例化。
    /// </summary>
    public class DatabaseManager
    {
        /// <summary>单例实例（懒加载）</summary>
        private static DatabaseManager _instance;
        /// <summary>主数据库连接字符串（连接 FinanceManager 数据库）</summary>
        private readonly string _connectionString;
        /// <summary>主数据库逻辑名称</summary>
        private const string DatabaseName = "FinanceManager";

        /// <summary>
        /// 私有构造函数：构建连接字符串 → 首次连接时自动建库建表
        /// </summary>
        private DatabaseManager()
        {
            // LocalDB 连接字符串（VS 2022 自带，无需额外安装）
            _connectionString = $@"Server=(localdb)\MSSQLLocalDB;Database={DatabaseName};Integrated Security=true;";
            InitializeDatabase();
        }

        /// <summary>全局单例访问点</summary>
        public static DatabaseManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DatabaseManager();
                return _instance;
            }
        }

        /// <summary>FinanceManager 数据库的连接字符串</summary>
        public string ConnectionString => _connectionString;
        /// <summary>兼容旧代码，返回 AppData 路径提示</summary>
        public string DatabasePath => $"(localdb)\\MSSQLLocalDB\\{DatabaseName}";

        /// <summary>
        /// 初始化数据库：若 FinanceManager 库不存在则创建 → 建表 → 插默认分类
        /// </summary>
        private void InitializeDatabase()
        {
            // 第1步：确保数据库存在
            EnsureDatabaseExists();

            // 第2步：建表和默认数据（幂等，已存在则跳过）
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                CreateUsersTable(connection);
                CreateCategoriesTable(connection);
                CreateRecordsTable(connection);
                CreateBudgetsTable(connection);
                CreateCategoryBudgetsTable(connection);
                CreateTemplatesTable(connection);
                InitializeDefaultCategories(connection);
            }
        }

        /// <summary>
        /// 连接到 master 库检查 FinanceManager 是否存在，不存在则创建
        /// </summary>
        private void EnsureDatabaseExists()
        {
            var masterConnStr = $@"Server=(localdb)\MSSQLLocalDB;Database=master;Integrated Security=true;";
            using (var conn = new SqlConnection(masterConnStr))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = $@"
                    IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{DatabaseName}')
                    BEGIN
                        CREATE DATABASE [{DatabaseName}]
                    END";
                cmd.ExecuteNonQuery();
            }
        }

        // ===== 建表 DDL（SQL Server 语法）=====

        /// <summary>检查表是否已存在（幂等建表）</summary>
        private bool TableExists(SqlConnection connection, string tableName)
        {
            var command = connection.CreateCommand();
            command.CommandText = @"
                SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_NAME = @tableName";
            command.Parameters.AddWithValue("@tableName", tableName);
            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        private void CreateUsersTable(SqlConnection connection)
        {
            if (TableExists(connection, "users")) return;
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE users (
                    id INT IDENTITY(1,1) PRIMARY KEY,
                    username NVARCHAR(20) NOT NULL UNIQUE,
                    email NVARCHAR(100) NULL,
                    password NVARCHAR(100) NOT NULL,
                    currency NVARCHAR(3) NOT NULL DEFAULT 'CNY',
                    ai_suggestion_enabled BIT NOT NULL DEFAULT 0,
                    created_at DATETIME NOT NULL,
                    last_login_at DATETIME NULL,
                    status INT NOT NULL DEFAULT 0
                )";
            cmd.ExecuteNonQuery();
        }

        private void CreateCategoriesTable(SqlConnection connection)
        {
            if (TableExists(connection, "categories")) return;
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE categories (
                    id INT IDENTITY(1,1) PRIMARY KEY,
                    name NVARCHAR(20) NOT NULL,
                    type INT NOT NULL,
                    icon NVARCHAR(100) NOT NULL,
                    color NVARCHAR(7) NOT NULL,
                    is_default BIT NOT NULL DEFAULT 0,
                    user_id INT NULL REFERENCES users(id),
                    created_at DATETIME NOT NULL,
                    updated_at DATETIME NOT NULL
                )";
            cmd.ExecuteNonQuery();
        }

        private void CreateRecordsTable(SqlConnection connection)
        {
            if (TableExists(connection, "records")) return;
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE records (
                    id INT IDENTITY(1,1) PRIMARY KEY,
                    amount DECIMAL(18,2) NOT NULL,
                    currency NVARCHAR(3) NOT NULL DEFAULT 'CNY',
                    type INT NOT NULL,
                    category_id INT NOT NULL REFERENCES categories(id),
                    [date] DATETIME NOT NULL,
                    note NVARCHAR(500) NULL,
                    user_id INT NOT NULL REFERENCES users(id),
                    created_at DATETIME NOT NULL,
                    updated_at DATETIME NOT NULL
                )";
            cmd.ExecuteNonQuery();
        }

        private void CreateBudgetsTable(SqlConnection connection)
        {
            if (TableExists(connection, "budgets")) return;
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE budgets (
                    id INT IDENTITY(1,1) PRIMARY KEY,
                    amount DECIMAL(18,2) NOT NULL,
                    currency NVARCHAR(3) NOT NULL DEFAULT 'CNY',
                    month INT NOT NULL,
                    year INT NOT NULL,
                    user_id INT NOT NULL REFERENCES users(id),
                    created_at DATETIME NOT NULL,
                    updated_at DATETIME NOT NULL,
                    UNIQUE(year, month, user_id)
                )";
            cmd.ExecuteNonQuery();
        }

        private void CreateCategoryBudgetsTable(SqlConnection connection)
        {
            if (TableExists(connection, "category_budgets")) return;
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE category_budgets (
                    id INT IDENTITY(1,1) PRIMARY KEY,
                    category_id INT NOT NULL REFERENCES categories(id),
                    amount DECIMAL(18,2) NOT NULL,
                    currency NVARCHAR(3) NOT NULL DEFAULT 'CNY',
                    month INT NOT NULL,
                    year INT NOT NULL,
                    user_id INT NOT NULL REFERENCES users(id),
                    created_at DATETIME NOT NULL,
                    updated_at DATETIME NOT NULL,
                    UNIQUE(year, month, category_id, user_id)
                )";
            cmd.ExecuteNonQuery();
        }

        private void CreateTemplatesTable(SqlConnection connection)
        {
            if (TableExists(connection, "templates")) return;
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"
                CREATE TABLE templates (
                    id INT IDENTITY(1,1) PRIMARY KEY,
                    name NVARCHAR(30) NOT NULL,
                    default_amount DECIMAL(18,2) NOT NULL,
                    currency NVARCHAR(3) NOT NULL DEFAULT 'CNY',
                    type INT NOT NULL,
                    category_id INT NOT NULL REFERENCES categories(id),
                    note_template NVARCHAR(500) NULL,
                    is_favorite BIT NOT NULL DEFAULT 0,
                    use_count INT NOT NULL DEFAULT 0,
                    user_id INT NOT NULL REFERENCES users(id),
                    created_at DATETIME NOT NULL,
                    updated_at DATETIME NOT NULL
                )";
            cmd.ExecuteNonQuery();
        }

        // ===== 默认分类（与 SQL CE 版一致：9 支出 + 5 收入）=====

        private void InitializeDefaultCategories(SqlConnection connection)
        {
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT COUNT(*) FROM categories WHERE is_default = 1";
            var count = Convert.ToInt32(cmd.ExecuteScalar());
            if (count > 0) return;

            var now = DateTime.Now;
            var expenseCategories = new[]
            {
                new { Name = "餐饮", Icon = "food", Color = "#FF5722" },
                new { Name = "交通", Icon = "traffic", Color = "#2196F3" },
                new { Name = "购物", Icon = "shopping", Color = "#E91E63" },
                new { Name = "娱乐", Icon = "entertainment", Color = "#9C27B0" },
                new { Name = "居住", Icon = "home", Color = "#4CAF50" },
                new { Name = "医疗", Icon = "medical", Color = "#F44336" },
                new { Name = "教育", Icon = "education", Color = "#00BCD4" },
                new { Name = "通讯", Icon = "communication", Color = "#795548" },
                new { Name = "其他", Icon = "other", Color = "#607D8B" }
            };
            var incomeCategories = new[]
            {
                new { Name = "工资", Icon = "salary", Color = "#4CAF50" },
                new { Name = "奖金", Icon = "bonus", Color = "#8BC34A" },
                new { Name = "投资", Icon = "investment", Color = "#FF9800" },
                new { Name = "兼职", Icon = "parttime", Color = "#00BCD4" },
                new { Name = "其他", Icon = "other_income", Color = "#9E9E9E" }
            };

            foreach (var cat in expenseCategories)
                InsertCategory(connection, cat.Name, 0, cat.Icon, cat.Color, now);
            foreach (var cat in incomeCategories)
                InsertCategory(connection, cat.Name, 1, cat.Icon, cat.Color, now);
        }

        private void InsertCategory(SqlConnection conn, string name, int type,
            string icon, string color, DateTime now)
        {
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"
                INSERT INTO categories (name, type, icon, color, is_default, created_at, updated_at)
                VALUES (@n, @t, @i, @c, 1, @ca, @ua)";
            cmd.Parameters.AddWithValue("@n", name);
            cmd.Parameters.AddWithValue("@t", type);
            cmd.Parameters.AddWithValue("@i", icon);
            cmd.Parameters.AddWithValue("@c", color);
            cmd.Parameters.AddWithValue("@ca", now);
            cmd.Parameters.AddWithValue("@ua", now);
            cmd.ExecuteNonQuery();
        }
    }
}
