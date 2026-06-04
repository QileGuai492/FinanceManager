using FinanceManager.Data.Database;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
using FinanceManager.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Data.Repositories
{
    /// <summary>分类仓储 —— 封装 categories 表的 CRUD，支持按类型和用户筛选</summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;
        public CategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public Task<IEnumerable<CategoryEntity>> GetByUserIdAsync(int userId)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        SELECT * FROM categories
                        WHERE user_id = @userId OR is_default = 1
                        ORDER BY type, id";
                    command.Parameters.AddWithValue("@userId", userId);

                    var list = new List<CategoryEntity>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(MapReader(reader));
                    }
                    return (IEnumerable<CategoryEntity>)list;
                }
            });
        }

        public Task<IEnumerable<CategoryEntity>> GetByTypeAsync(int userId, int type)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        SELECT * FROM categories
                        WHERE (user_id = @userId OR is_default = 1)
                        AND type = @type
                        ORDER BY id";
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@type", type);

                    var list = new List<CategoryEntity>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(MapReader(reader));
                    }
                    return (IEnumerable<CategoryEntity>)list;
                }
            });
        }

        public Task<CategoryEntity> GetByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM categories WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapReader(reader);
                        return null;
                    }
                }
            });
        }

        public Task<int> InsertAsync(CategoryEntity entity)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        INSERT INTO categories (name, type, icon, color, is_default, user_id, created_at, updated_at)
                        VALUES (@name, @type, @icon, @color, @isDefault, @userId, @createdAt, @updatedAt)";
                    command.Parameters.AddWithValue("@name", entity.Name);
                    command.Parameters.AddWithValue("@type", (int)entity.Type);
                    command.Parameters.AddWithValue("@icon", entity.Icon);
                    command.Parameters.AddWithValue("@color", entity.Color);
                    command.Parameters.AddWithValue("@isDefault", entity.IsDefault);
                    command.Parameters.AddWithValue("@userId", (object)entity.UserId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@createdAt", entity.CreatedAt);
                    command.Parameters.AddWithValue("@updatedAt", entity.UpdatedAt);
                    command.ExecuteNonQuery();

                    var idCmd = connection.CreateCommand();
                    idCmd.CommandText = "SELECT @@IDENTITY";
                    var idResult = idCmd.ExecuteScalar();
                    return idResult is DBNull ? 0 : Convert.ToInt32(idResult);
                }
            });
        }

        public Task UpdateAsync(CategoryEntity entity)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        UPDATE categories
                        SET name = @name, type = @type, icon = @icon, color = @color,
                            is_default = @isDefault, user_id = @userId, updated_at = @updatedAt
                        WHERE id = @id";
                    command.Parameters.AddWithValue("@id", entity.Id);
                    command.Parameters.AddWithValue("@name", entity.Name);
                    command.Parameters.AddWithValue("@type", (int)entity.Type);
                    command.Parameters.AddWithValue("@icon", entity.Icon);
                    command.Parameters.AddWithValue("@color", entity.Color);
                    command.Parameters.AddWithValue("@isDefault", entity.IsDefault);
                    command.Parameters.AddWithValue("@userId", (object)entity.UserId ?? DBNull.Value);
                    command.Parameters.AddWithValue("@updatedAt", entity.UpdatedAt);
                    command.ExecuteNonQuery();
                }
            });
        }

        public Task DeleteAsync(int id)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "DELETE FROM categories WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            });
        }

        public Task<int> GetCustomCountByUserIdAsync(int userId)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        SELECT COUNT(*) FROM categories
                        WHERE user_id = @userId AND is_default = 0";
                    command.Parameters.AddWithValue("@userId", userId);
                    var cmdResult = command.ExecuteScalar();
                    return cmdResult is DBNull ? 0 : Convert.ToInt32(cmdResult);
                }
            });
        }

        private CategoryEntity MapReader(SqlDataReader reader)
        {
            return new CategoryEntity
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Name = reader.GetString(reader.GetOrdinal("name")),
                Type = (RecordType)reader.GetInt32(reader.GetOrdinal("type")),
                Icon = reader.GetString(reader.GetOrdinal("icon")),
                Color = reader.GetString(reader.GetOrdinal("color")),
                IsDefault = reader.GetBoolean(reader.GetOrdinal("is_default")),
                UserId = reader.IsDBNull(reader.GetOrdinal("user_id"))
                    ? (int?)null
                    : reader.GetInt32(reader.GetOrdinal("user_id")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
            };
        }
    }
}
