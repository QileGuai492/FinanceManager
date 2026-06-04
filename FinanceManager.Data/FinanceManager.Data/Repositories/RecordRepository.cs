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
    /// <summary>记账记录仓储 —— 封装 records 表的 CRUD 和按类型/分类/日期范围汇总查询</summary>
    public class RecordRepository : IRecordRepository
    {
        private readonly string _connectionString;

        public RecordRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Task<IEnumerable<RecordEntity>> GetByUserIdAsync(int userId)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM records WHERE user_id = @userId ORDER BY [date] DESC";
                    command.Parameters.AddWithValue("@userId", userId);

                    using (var reader = command.ExecuteReader())
                    {
                        var records = new List<RecordEntity>();
                        while (reader.Read())
                        {
                            records.Add(MapReaderToEntity(reader));
                        }
                        return (IEnumerable<RecordEntity>)records;
                    }
                }
            });
        }

        public Task<IEnumerable<RecordEntity>> GetByDateRangeAsync(int userId, DateTime startDate, DateTime endDate)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    SELECT * FROM records
                    WHERE user_id = @userId
                    AND [date] BETWEEN @startDate AND @endDate
                    ORDER BY [date] DESC";
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@startDate", startDate.Date);
                    command.Parameters.AddWithValue("@endDate", endDate.Date);

                    using (var reader = command.ExecuteReader())
                    {
                        var records = new List<RecordEntity>();
                        while (reader.Read())
                        {
                            records.Add(MapReaderToEntity(reader));
                        }
                        return (IEnumerable<RecordEntity>)records;
                    }
                }
            });
        }

        public Task<RecordEntity> GetByIdAsync(int id)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = "SELECT * FROM records WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapReaderToEntity(reader);
                        }
                        return null;
                    }
                }
            });
        }

        public Task<int> InsertAsync(RecordEntity entity)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    INSERT INTO records (amount, currency, type, category_id, [date], note, user_id, created_at, updated_at)
                    VALUES (@amount, @currency, @type, @categoryId, @date, @note, @userId, @createdAt, @updatedAt)";
                    command.Parameters.AddWithValue("@amount", entity.Amount);
                    command.Parameters.AddWithValue("@currency", entity.Currency);
                    command.Parameters.AddWithValue("@type", (int)entity.Type);
                    command.Parameters.AddWithValue("@categoryId", entity.CategoryId);
                    command.Parameters.AddWithValue("@date", entity.Date);
                    command.Parameters.AddWithValue("@note", (object)entity.Note ?? DBNull.Value);
                    command.Parameters.AddWithValue("@userId", entity.UserId);
                    command.Parameters.AddWithValue("@createdAt", entity.CreatedAt);
                    command.Parameters.AddWithValue("@updatedAt", entity.UpdatedAt);

                    command.ExecuteNonQuery();

                    var idCommand = connection.CreateCommand();
                    idCommand.CommandText = "SELECT @@IDENTITY";
                    var idResult = idCommand.ExecuteScalar();
                    return idResult is DBNull ? 0 : Convert.ToInt32(idResult);
                }
            });
        }

        public Task UpdateAsync(RecordEntity entity)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"
                        UPDATE records
                        SET amount = @amount, 
                            currency = @currency, 
                            type = @type, category_id = @categoryId,
                            [date] = @date, 
                            note = @note, 
                            updated_at = @updatedAt
                        WHERE id = @id";
                    command.Parameters.AddWithValue("@id", entity.Id);
                    command.Parameters.AddWithValue("@amount", entity.Amount);
                    command.Parameters.AddWithValue("@currency", entity.Currency);
                    command.Parameters.AddWithValue("@type", (int)entity.Type);
                    command.Parameters.AddWithValue("@categoryId", entity.CategoryId);
                    command.Parameters.AddWithValue("@date", entity.Date);
                    command.Parameters.AddWithValue("@note", (object)entity.Note ?? DBNull.Value);
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
                    command.CommandText = "DELETE FROM records WHERE id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    command.ExecuteNonQuery();
                }
            });
        }

        public Task<decimal> GetSumByTypeAndDateRangeAsync(int userId, int type, DateTime startDate, DateTime endDate)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    SELECT COALESCE(SUM(amount), 0) FROM records
                    WHERE user_id = @userId AND type = @type
                    AND [date] BETWEEN @startDate AND @endDate";
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@startDate", startDate.Date);
                    command.Parameters.AddWithValue("@endDate", endDate.Date);

                    var result = command.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0m;
                }
            });
        }

        public Task<decimal> GetSumByCategoryAndTypeAndDateRangeAsync(int userId, int categoryId, int type, DateTime startDate, DateTime endDate)
        {
            return Task.Run(() =>
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    var command = connection.CreateCommand();
                    command.CommandText = @"
                    SELECT COALESCE(SUM(amount), 0) FROM records
                    WHERE user_id = @userId AND category_id = @categoryId AND type = @type
                    AND [date] BETWEEN @startDate AND @endDate";
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@categoryId", categoryId);
                    command.Parameters.AddWithValue("@type", type);
                    command.Parameters.AddWithValue("@startDate", startDate.Date);
                    command.Parameters.AddWithValue("@endDate", endDate.Date);

                    var result = command.ExecuteScalar();
                    return result != DBNull.Value ? Convert.ToDecimal(result) : 0m;
                }
            });
        }

        private RecordEntity MapReaderToEntity(SqlDataReader reader)
        {
            return new RecordEntity
            {
                Id = reader.GetInt32(reader.GetOrdinal("id")),
                Amount = reader.GetDecimal(reader.GetOrdinal("amount")),
                Currency = reader.GetString(reader.GetOrdinal("currency")),
                Type = (RecordType)reader.GetInt32(reader.GetOrdinal("type")),
                CategoryId = reader.GetInt32(reader.GetOrdinal("category_id")),
                Date = reader.GetDateTime(reader.GetOrdinal("date")),
                Note = reader.IsDBNull(reader.GetOrdinal("note")) ? null : reader.GetString(reader.GetOrdinal("note")),
                UserId = reader.GetInt32(reader.GetOrdinal("user_id")),
                CreatedAt = reader.GetDateTime(reader.GetOrdinal("created_at")),
                UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updated_at"))
            };
        }
    }
}
