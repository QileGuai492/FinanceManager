using FinanceManager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Tests.Integration
{
    [TestClass]
    public class BudgetServiceIntegrationTests : IntegrationTestBase
    {
        /// <summary>新增月预算 → 查回</summary>
        [TestMethod]
        public async Task AddBudget_PersistsAndRetrieves()
        {
            var budget = new BudgetEntity
            {
                Amount = 10000m,
                Year = 2026,
                Month = 6,
                UserId = TestUserId
            };

            var id = await BudgetService.AddBudgetAsync(budget);
            Assert.IsTrue(id > 0);

            var loaded = await BudgetService.GetBudgetByYearMonthAsync(TestUserId, 2026, 6);
            Assert.IsNotNull(loaded);
            Assert.AreEqual(10000m, loaded.Amount);
        }

        /// <summary>更新预算 → 覆盖旧值</summary>
        [TestMethod]
        public async Task UpdateBudget_OverwritesOldValue()
        {
            var budget = new BudgetEntity
            { Amount = 5000m, Year = 2026, Month = 6, UserId = TestUserId };
            var id = await BudgetService.AddBudgetAsync(budget);

            var loaded = await BudgetService.GetBudgetByYearMonthAsync(TestUserId, 2026, 6);
            loaded.Amount = 15000m;
            await BudgetService.UpdateBudgetAsync(loaded);

            var reloaded = await BudgetService.GetBudgetByYearMonthAsync(TestUserId, 2026, 6);
            Assert.AreEqual(15000m, reloaded.Amount);
        }

        /// <summary>分类预算：为某个分类设置预算</summary>
        [TestMethod]
        public async Task CategoryBudget_PersistsAndRetrieves()
        {
            var categories = await CategoryService.GetCategoriesAsync(TestUserId);
            var catId = categories.First().Id;

            var cb = new CategoryBudgetEntity
            {
                CategoryId = catId,
                Amount = 3000m,
                Year = 2026,
                Month = 6,
                UserId = TestUserId
            };
            var id = await BudgetService.AddCategoryBudgetAsync(cb);
            Assert.IsTrue(id > 0);

            var budgets = await BudgetService.GetCategoryBudgetsAsync(TestUserId, 2026, 6);
            Assert.IsTrue(budgets.Any(b => b.CategoryId == catId));
        }

        /// <summary>删除预算</summary>
        [TestMethod]
        public async Task DeleteBudget_RemovesFromDatabase()
        {
            var budget = new BudgetEntity
            { Amount = 8000m, Year = 2026, Month = 7, UserId = TestUserId };
            var id = await BudgetService.AddBudgetAsync(budget);

            await BudgetService.DeleteBudgetAsync(id);

            var loaded = await BudgetService.GetBudgetByYearMonthAsync(TestUserId, 2026, 7);
            Assert.IsNull(loaded);
        }
    }
}
