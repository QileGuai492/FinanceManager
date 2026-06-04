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
    /// <summary>分类预算仓储 —— 封装 category_budgets 表的 CRUD，按年月+用户+分类查询</summary>
    public class CategoryBudgetRepository : ICategoryBudgetRepository
    {
        private readonly string _connectionString;

        public CategoryBudgetRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<CategoryBudgetEntity> GetByCategoryAsync(int userId, int categoryId, int year, int month)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        SELECT * FROM category_budgets
                        WHERE user_id = @userId AND category_id = @categoryId
                        AND year = @year AND month = @month";
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@categoryId", categoryId);
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

        public Task<IEnumerable<CategoryBudgetEntity>> GetByYearMonthAsync(int userId, int year, int month)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        SELECT * FROM category_budgets
                        WHERE user_id = @userId AND year = @year AND month = @month
                        ORDER BY category_id";
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@year", year);
                    command.Parameters.AddWithValue("@month", month);

                    var list = new List<CategoryBudgetEntity>();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                            list.Add(MapReader(reader));
                    }
                    return (IEnumerable<CategoryBudgetEntity>)list;
                }
            });
        }

        public Task<int> InsertAsync(CategoryBudgetEntity entity)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        INSERT INTO category_budgets
                            (category_id, amount, currency, month, year, user_id, created_at, updated_at)
                        VALUES
                            (@categoryId, @amount, @currency, @month, @year, @userId, @createdAt, @updatedAt)";
                    command.Parameters.AddWithValue("@categoryId", entity.CategoryId);
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

        public Task UpdateAsync(CategoryBudgetEntity entity)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        UPDATE category_budgets
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
                    command.CommandText = "DELETE FROM category_budgets WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            });
        }

        private CategoryBudgetEntity MapReader(SqlDataReader reader)
        {
            return new CategoryBudgetEntity
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                CategoryId = reader.GetInt32(reader.GetOrdinal("category_id")),
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
