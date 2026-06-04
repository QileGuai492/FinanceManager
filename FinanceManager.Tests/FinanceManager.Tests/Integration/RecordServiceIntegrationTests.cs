using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceManager.Tests.Integration
{
    /// <summary>记账记录集成测试 —— 真实 LocalDB，测试 CRUD + 日期筛选 + 分类汇总全链路</summary>
    [TestClass]
    public class RecordServiceIntegrationTests : IntegrationTestBase
    {
        /// <summary>新增收入 → 查回验证</summary>
        [TestMethod]
        public async Task AddRecord_Income_PersistsToDatabase()
        {
            var categories = await CategoryService.GetCategoriesByTypeAsync(TestUserId, (int)RecordType.Income);
            var catId = categories.First().Id;

            var id = await RecordService.AddRecordAsync(new RecordEntity
            {
                Amount = 15000m, Type = RecordType.Income,
                CategoryId = catId, UserId = TestUserId,
                Date = DateTime.Today, Note = "6月工资"
            });
            Assert.IsTrue(id > 0);

            var loaded = await RecordService.GetRecordByIdAsync(id);
            Assert.AreEqual(15000m, loaded.Amount);
            Assert.AreEqual("6月工资", loaded.Note);
            Assert.AreEqual(RecordType.Income, loaded.Type);
        }

        /// <summary>新增支出 → 查回验证</summary>
        [TestMethod]
        public async Task AddRecord_Expense_PersistsToDatabase()
        {
            var categories = await CategoryService.GetCategoriesByTypeAsync(TestUserId, (int)RecordType.Expense);
            var catId = categories.First().Id;

            var id = await RecordService.AddRecordAsync(new RecordEntity
            {
                Amount = -35m, Type = RecordType.Expense,
                CategoryId = catId, UserId = TestUserId,
                Date = DateTime.Today, Note = "午餐"
            });
            Assert.IsTrue(id > 0);

            var loaded = await RecordService.GetRecordByIdAsync(id);
            Assert.AreEqual(-35m, loaded.Amount);
            Assert.AreEqual(RecordType.Expense, loaded.Type);
        }

        /// <summary>编辑记录 → 金额修改持久化</summary>
        [TestMethod]
        public async Task UpdateRecord_ChangesAmount_Persisted()
        {
            var categories = await CategoryService.GetCategoriesByTypeAsync(TestUserId, (int)RecordType.Expense);
            var catId = categories.First().Id;

            var id = await RecordService.AddRecordAsync(new RecordEntity
            {
                Amount = -100m, Type = RecordType.Expense,
                CategoryId = catId, UserId = TestUserId, Date = DateTime.Today
            });

            var loaded = await RecordService.GetRecordByIdAsync(id);
            loaded.Amount = -200m;
            await RecordService.UpdateRecordAsync(loaded);

            var reloaded = await RecordService.GetRecordByIdAsync(id);
            Assert.AreEqual(-200m, reloaded.Amount);
        }

        /// <summary>删除记录 → 查回为 null</summary>
        [TestMethod]
        public async Task DeleteRecord_RemovesFromDatabase()
        {
            var categories = await CategoryService.GetCategoriesByTypeAsync(TestUserId, (int)RecordType.Expense);
            var catId = categories.First().Id;

            var id = await RecordService.AddRecordAsync(new RecordEntity
            {
                Amount = -50m, Type = RecordType.Expense,
                CategoryId = catId, UserId = TestUserId, Date = DateTime.Today
            });

            await RecordService.DeleteRecordAsync(id);
            var loaded = await RecordService.GetRecordByIdAsync(id);
            Assert.IsNull(loaded);
        }

        /// <summary>日期范围查询 —— 只返回范围内的记录</summary>
        [TestMethod]
        public async Task GetRecordsByDateRange_FiltersCorrectly()
        {
            var categories = await CategoryService.GetCategoriesByTypeAsync(TestUserId, (int)RecordType.Expense);
            var catId = categories.First().Id;

            await RecordService.AddRecordAsync(new RecordEntity
            { Amount = -100m, Type = RecordType.Expense, CategoryId = catId,
              UserId = TestUserId, Date = new DateTime(2026, 6, 15) });
            await RecordService.AddRecordAsync(new RecordEntity
            { Amount = -200m, Type = RecordType.Expense, CategoryId = catId,
              UserId = TestUserId, Date = new DateTime(2026, 7, 1) });

            var june = await RecordService.GetRecordsByDateRangeAsync(
                TestUserId, new DateTime(2026, 6, 1), new DateTime(2026, 6, 30));

            Assert.AreEqual(1, june.Count());
            Assert.AreEqual(-100m, june.First().Amount);
        }

        /// <summary>按类型汇总金额</summary>
        [TestMethod]
        public async Task GetTotalAmountByType_SumsCorrectly()
        {
            var categories = await CategoryService.GetCategoriesByTypeAsync(TestUserId, (int)RecordType.Expense);
            var catId = categories.First().Id;

            await RecordService.AddRecordAsync(new RecordEntity
            { Amount = -500m, Type = RecordType.Expense, CategoryId = catId,
              UserId = TestUserId, Date = new DateTime(2026, 6, 1) });
            await RecordService.AddRecordAsync(new RecordEntity
            { Amount = -300m, Type = RecordType.Expense, CategoryId = catId,
              UserId = TestUserId, Date = new DateTime(2026, 6, 2) });

            var total = await RecordService.GetTotalAmountByTypeAsync(
                TestUserId, (int)RecordType.Expense,
                new DateTime(2026, 6, 1), new DateTime(2026, 6, 30));

            Assert.AreEqual(-800m, total);
        }
    }
}
