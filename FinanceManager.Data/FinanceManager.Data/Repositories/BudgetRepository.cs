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
    /// <summary>预算仓储 —— 封装 budgets 表的 CRUD，按年月和用户查询</summary>
    public class  BudgetRepository: IBudgetRepository
    {
        private readonly string _connectionString;

        public BudgetRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<BudgetEntity> GetByYearMonthAsync(int userId, int year, int month)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        SELECT * FROM budgets
                        WHERE user_id = @userId AND year = @year AND month = @month";
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            return MapReader(reader);
                        return null;
                    }
                }
            });
        }

        public Task<IEnumerable<BudgetEntity>> GetByUserIdAsync(int userId)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        SELECT * FROM budgets
                        WHERE user_id = @userId
                        ORDER BY year DESC, month DESC";
                    command.Parameters.AddWithValue("@userId", userId);

                    var list = new List<BudgetEntity>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(MapReader(reader));
                    }
                    return (IEnumerable<BudgetEntity>)list;
                }
            });
        }

        public Task<int> InsertAsync(BudgetEntity entity)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        INSERT INTO budgets (amount, currency, month, year, user_id, created_at, updated_at)
                        VALUES (@amount, @currency, @month, @year, @userId, @createdAt, @updatedAt)";
                    command.Parameters.AddWithValue("@amount", entity.Amount);
                    command.Parameters.AddWithValue("@currency", entity.Currency);
                    command.Parameters.AddWithValue("@month", entity.Month);
                    command.Parameters.AddWithValue("@year", entity.Year);
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

        public Task UpdateAsync(BudgetEntity entity)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        UPDATE budgets
                        SET amount = @amount, currency = @currency, updated_at = @updatedAt
                        WHERE id = @id";
                    command.Parameters.AddWithValue("@id", entity.Id);
                    command.Parameters.AddWithValue("@amount", entity.Amount);
                    command.Parameters.AddWithValue("@currency", entity.Currency);
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
                    command.CommandText = "DELETE FROM budgets WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            });
        }

        private BudgetEntity MapReader(SqlDataReader reader)
        {
            return new BudgetEntity
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Amount = reader.GetDecimal(reader.GetOrdinal("amount")),
                Currency = reader.GetString(reader.GetOrdinal("currency")),
                Month = reader.GetInt32(reader.GetOrdinal("month")),
                Year = reader.GetInt32(reader.GetOrdinal("year")),
                UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
            };
        }

    }
}
