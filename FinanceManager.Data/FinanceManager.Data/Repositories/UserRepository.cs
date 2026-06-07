using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Data.Repositories
{
    /// <summary>用户仓储 —— 封装 users 表的 CRUD 操作，直接执行 SQL（SQL Server）</summary>
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<UserEntity> GetByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT * FROM users WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return MapToEntity(reader);
                        return null;
                    }
                }
            });
        }

        public Task<UserEntity> GetByUsernameAsync(string username)
        {
            return Task.Run(() =>
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = new SqlCommand("SELECT * FROM users WHERE username = @u", conn);
                    cmd.Parameters.AddWithValue("@u", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read()) return MapToEntity(reader);
                        return null;
                    }
                }
            });
        }

        public Task<int> InsertAsync(UserEntity entity)
        {
            return Task.Run(() =>
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"
                        INSERT INTO users (username, email, password, currency, ai_suggestion_enabled, created_at, status)
                        VALUES (@u, @e, @p, @c, @ai, @ca, @s)";
                    cmd.Parameters.AddWithValue("@u", entity.Username);
                    cmd.Parameters.AddWithValue("@e", (object)entity.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p", entity.Password);
                    cmd.Parameters.AddWithValue("@c", entity.Currency);
                    cmd.Parameters.AddWithValue("@ai", entity.AiSuggestionEnabled);
                    cmd.Parameters.AddWithValue("@ca", entity.CreatedAt);
                    cmd.Parameters.AddWithValue("@s", entity.Status);
                    cmd.ExecuteNonQuery();

                    var idCmd = conn.CreateCommand();
                    idCmd.CommandText = "SELECT @@IDENTITY";
                    var idResult = idCmd.ExecuteScalar();
                    return idResult is DBNull ? 0 : Convert.ToInt32(idResult);
                }
            });
        }

        public Task UpdateAsync(UserEntity entity)
        {
            return Task.Run(() =>
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = @"
                        UPDATE users
                        SET username = @u, email = @e, password = @p, currency = @c,
                            ai_suggestion_enabled = @ai,
                            last_login_at = @ll, status = @s
                        WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", entity.Id);
                    cmd.Parameters.AddWithValue("@u", entity.Username);
                    cmd.Parameters.AddWithValue("@e", (object)entity.Email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@p", entity.Password);
                    cmd.Parameters.AddWithValue("@c", entity.Currency);
                    cmd.Parameters.AddWithValue("@ai", entity.AiSuggestionEnabled);
                    cmd.Parameters.AddWithValue("@ll", (object)entity.LastLoginAt ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@s", entity.Status);
                    cmd.ExecuteNonQuery();
                }
            });
        }

        public Task UpdateLastLoginTimeAsync(int userId)
        {
            return Task.Run(() =>
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    var cmd = conn.CreateCommand();
                    cmd.CommandText = "UPDATE users SET last_login_at = @ll WHERE id = @id";
                    cmd.Parameters.AddWithValue("@id", userId);
                    cmd.Parameters.AddWithValue("@ll", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            });
        }

        public Task UpdateAiSuggestionEnabledAsync(int userId, bool enabled)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        UPDATE users
                        SET ai_suggestion_enabled = @enabled
                        WHERE id = @userId";
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@enabled", enabled);
                        command.ExecuteNonQuery();
                    }
                }
            });
        }

        private UserEntity MapToEntity(SqlDataReader reader)
        {
            return new UserEntity
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Username = reader.GetString(reader.GetOrdinal("username")),
                Email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString(reader.GetOrdinal("email")),
                Password = reader.GetString(reader.GetOrdinal("password")),
                Currency = reader.GetString(reader.GetOrdinal("currency")),
                AiSuggestionEnabled = reader.GetBoolean(reader.GetOrdinal("ai_suggestion_enabled")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                LastLoginAt = reader.IsDBNull(reader.GetOrdinal("last_login_at")) ? (DateTime?)null : reader.GetDateTime(reader.GetOrdinal("last_login_at")),
                Status = reader.GetInt32(reader.GetOrdinal("status"))
            };
        }
    }
}
