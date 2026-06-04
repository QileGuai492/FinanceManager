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
    /// <summary>模板仓储 —— 封装 templates 表的 CRUD，支持收藏筛选和使用计数递增</summary>
    public class TemplateRepository : ITemplateRepository
    {
        private readonly string _connectionString;
        public TemplateRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public Task<IEnumerable<TemplateEntity>> GetByUserIdAsync(int userId)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        SELECT * FROM templates
                        WHERE user_id = @userId
                        ORDER BY use_count DESC, created_at DESC";
                    command.Parameters.AddWithValue("@userId", userId);

                    var list = new List<TemplateEntity>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(MapReader(reader));
                    }
                    return (IEnumerable<TemplateEntity>)list;
                }
            });
        }

        public Task<IEnumerable<TemplateEntity>> GetFavoriteByUserIdAsync(int userId)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        SELECT * FROM templates
                        WHERE user_id = @userId AND is_favorite = 1
                        ORDER BY use_count DESC";
                    command.Parameters.AddWithValue("@userId", userId);

                    var list = new List<TemplateEntity>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(MapReader(reader));
                    }
                    return (IEnumerable<TemplateEntity>)list;
                }
            });
        }

        public Task<TemplateEntity> GetByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM templates WHERE id = @id";
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

        public Task<int> InsertAsync(TemplateEntity entity)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        INSERT INTO templates
                            (name, default_amount, currency, type, category_id, note_template,
                             is_favorite, use_count, user_id, created_at, updated_at)
                        VALUES
                            (@name, @defaultAmount, @currency, @type, @categoryId, @noteTemplate,
                             @isFavorite, @useCount, @userId, @createdAt, @updatedAt)";
                    command.Parameters.AddWithValue("@name", entity.Name);
                    command.Parameters.AddWithValue("@defaultAmount", entity.DefaultAmount);
                    command.Parameters.AddWithValue("@currency", entity.Currency);
                    command.Parameters.AddWithValue("@type", (int)entity.Type);
                    command.Parameters.AddWithValue("@categoryId", entity.CategoryId);
                    command.Parameters.AddWithValue("@noteTemplate", (object)entity.NoteTemplate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@isFavorite", entity.IsFavorite);
                    command.Parameters.AddWithValue("@useCount", entity.UseCount);
                    command.Parameters.AddWithValue("@userId", entity.UserId);
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

        public Task UpdateAsync(TemplateEntity entity)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        UPDATE templates
                        SET name = @name, default_amount = @defaultAmount, currency = @currency,
                            type = @type, category_id = @categoryId, note_template = @noteTemplate,
                            is_favorite = @isFavorite, use_count = @useCount, updated_at = @updatedAt
                        WHERE id = @id";
                    command.Parameters.AddWithValue("@id", entity.Id);
                    command.Parameters.AddWithValue("@name", entity.Name);
                    command.Parameters.AddWithValue("@defaultAmount", entity.DefaultAmount);
                    command.Parameters.AddWithValue("@currency", entity.Currency);
                    command.Parameters.AddWithValue("@type", (int)entity.Type);
                    command.Parameters.AddWithValue("@categoryId", entity.CategoryId);
                    command.Parameters.AddWithValue("@noteTemplate", (object)entity.NoteTemplate ?? DBNull.Value);
                    command.Parameters.AddWithValue("@isFavorite", entity.IsFavorite);
                    command.Parameters.AddWithValue("@useCount", entity.UseCount);
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
                    command.CommandText = "DELETE FROM templates WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            });
        }

        public Task IncrementUseCountAsync(int id)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        UPDATE templates SET use_count = use_count + 1 WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            });
        }

        public Task<int> GetCountByUserIdAsync(int userId)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        SELECT COUNT(*) FROM templates WHERE user_id = @userId";
                    command.Parameters.AddWithValue("@userId", userId);
                    var cmdResult = command.ExecuteScalar();
                    return cmdResult is DBNull ? 0 : Convert.ToInt32(cmdResult);
                }
            });
        }

        private TemplateEntity MapReader(SqlDataReader reader)
        {
            return new TemplateEntity
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Name = reader.GetString(reader.GetOrdinal("name")),
                DefaultAmount = reader.GetDecimal(reader.GetOrdinal("default_amount")),
                Currency = reader.GetString(reader.GetOrdinal("currency")),
                Type = (RecordType)reader.GetInt32(reader.GetOrdinal("type")),
                CategoryId = reader.GetInt32(reader.GetOrdinal("category_id")),
                NoteTemplate = reader.IsDBNull(reader.GetOrdinal("note_template"))
                    ? null
                    : reader.GetString(reader.GetOrdinal("note_template")),
                IsFavorite = reader.GetBoolean(reader.GetOrdinal("is_favorite")),
                UseCount = reader.GetInt32(reader.GetOrdinal("use_count")),
                UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
            };
        }
    }
}
