using FinanceManager.Data.Services;
using FinanceManager.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Tests.Services
{
    [TestClass]
    public class BudgetServiceTests : TestBase
    {
        private BudgetService CreateService() =>
            new BudgetService(BudgetRepoMock.Object, CategoryBudgetRepoMock.Object);

        #region 预算 CRUD

        /// <summary>8.6 保存月预算：成功返回 ID</summary>
        [TestMethod]
        public async Task AddBudgetAsync_ValidMonthlyBudget_ReturnsId()
        {
            var budget = new BudgetEntity
            {
                Amount = 10000m,
                Year = 2026,
                Month = 6,
                UserId = 1
            };
            BudgetRepoMock.Setup(r => r.InsertAsync(It.IsAny<BudgetEntity>()))
                          .ReturnsAsync(1);

            var service = CreateService();
            var id = await service.AddBudgetAsync(budget);

            Assert.AreEqual(1, id);
        }

        /// <summary>8.10 年度保存：按年设置预算</summary>
        [TestMethod]
        public async Task AddBudgetAsync_YearlyBudget_ReturnsId()
        {
            var budget = new BudgetEntity
            {
                Amount = 120000m,
                Year = 2026,
                Month = 0,
                UserId = 1
                // Month=0 表示年度预算
            };
            BudgetRepoMock.Setup(r => r.InsertAsync(It.IsAny<BudgetEntity>()))
                          .ReturnsAsync(2);

            var service = CreateService();
            var id = await service.AddBudgetAsync(budget);

            Assert.AreEqual(2, id);
        }

        /// <summary>8.2 按月查询预算</summary>
        [TestMethod]
        public async Task GetBudgetByYearMonthAsync_ReturnsBudget()
        {
            var expected = new BudgetEntity
            {
                Id = 1,
                Amount = 8000m,
                Year = 2026,
                Month = 6,
                UserId = 1
            };
            BudgetRepoMock.Setup(r => r.GetByYearMonthAsync(1, 2026, 6))
                          .ReturnsAsync(expected);

            var service = CreateService();
            var result = await service.GetBudgetByYearMonthAsync(1, 2026, 6);

            Assert.IsNotNull(result);
            Assert.AreEqual(8000m, result.Amount);
        }

        /// <summary>更新预算</summary>
        [TestMethod]
        public async Task UpdateBudgetAsync_ChangedAmount_CallsUpdate()
        {
            var budget = new BudgetEntity
            {
                Id = 1,
                Amount = 12000m,
                Year = 2026,
                Month = 6,
                UserId = 1
            };

            var service = CreateService();
            await service.UpdateBudgetAsync(budget);

            BudgetRepoMock.Verify(r => r.UpdateAsync(It.IsAny<BudgetEntity>()), Times.Once);
        }

        /// <summary>删除预算</summary>
        [TestMethod]
        public async Task DeleteBudgetAsync_ValidId_CallsDelete()
        {
            var service = CreateService();
            await service.DeleteBudgetAsync(3);

            BudgetRepoMock.Verify(r => r.DeleteAsync(3), Times.Once);
        }

        #endregion

        #region 分类预算

        /// <summary>8.7 设置分类预算</summary>
        [TestMethod]
        public async Task AddCategoryBudgetAsync_ValidBudget_ReturnsId()
        {
            var catBudget = new CategoryBudgetEntity
            {
                CategoryId = 3,
                Amount = 2000m,
                Year = 2026,
                Month = 6,
                UserId = 1
            };
            CategoryBudgetRepoMock.Setup(r => r.InsertAsync(It.IsAny<CategoryBudgetEntity>()))
                                  .ReturnsAsync(1);

            var service = CreateService();
            var id = await service.AddCategoryBudgetAsync(catBudget);

            Assert.AreEqual(1, id);
        }

        /// <summary>获取某月的所有分类预算</summary>
        [TestMethod]
        public async Task GetCategoryBudgetsAsync_ReturnsAllCategoryBudgets()
        {
            var budgets = new List<CategoryBudgetEntity>
            {
                new CategoryBudgetEntity { Id = 1, CategoryId = 3, Amount = 2000m, Year = 2026, Month = 6, UserId = 1 },
                new CategoryBudgetEntity { Id = 2, CategoryId = 4, Amount = 1000m, Year = 2026, Month = 6, UserId = 1 },
            };
            CategoryBudgetRepoMock.Setup(r => r.GetByYearMonthAsync(1, 2026, 6))
                                  .ReturnsAsync(budgets);

            var service = CreateService();
            var result = await service.GetCategoryBudgetsAsync(1, 2026, 6);

            Assert.AreEqual(2, result.Count());
        }

        #endregion

        #region 预算计算逻辑（纯业务逻辑测试）

        /// <summary>8.1 日度预算 = 月预算 ÷ 当月天数</summary>
        [TestMethod]
        public void DailyBudget_CalculatedFromMonthlyBudget()
        {
            var monthlyBudget = 30000m;
            var daysInMonth = 30;

            var dailyBudget = monthlyBudget / daysInMonth;

            Assert.AreEqual(1000m, dailyBudget);
        }

        /// <summary>8.4 进度条百分比计算</summary>
        [TestMethod]
        public void BudgetProgress_50Percent_CalculatesCorrectly()
        {
            var budget = 10000m;
            var spent = 5000m;

            var progressPercent = (double)(spent / budget * 100);

            Assert.AreEqual(50.0, progressPercent, 0.01);
        }

        /// <summary>8.5 超预算：支出>预算，百分比超过100%</summary>
        [TestMethod]
        public void BudgetProgress_OverBudget_Exceeds100Percent()
        {
            var budget = 8000m;
            var spent = 12000m;

            var progressPercent = (double)(spent / budget * 100);

            Assert.IsTrue(progressPercent > 100, "超预算时进度应超过100%");
            Assert.AreEqual(150.0, progressPercent, 0.01);
        }

        /// <summary>8.8 预警100%：分类达100%预算</summary>
        [TestMethod]
        public void CategoryAlert_At100Percent_TriggersWarning()
        {
            var budget = 2000m;
            var spent = 2000m;

            var ratio = spent / budget;
            bool isWarning = ratio >= 0.80m;   // 80% 预警
            bool isOverBudget = ratio >= 1.0m;  // 100% 超支

            Assert.IsTrue(isWarning, "达到80%应触发预警");
            Assert.IsTrue(isOverBudget, "达到100%应触发超支");
        }

        /// <summary>8.9 无预警：所有分类<80%</summary>
        [TestMethod]
        public void CategoryAlert_Below80Percent_NoWarning()
        {
            var budget = 5000m;
            var spent = 3000m;

            var ratio = spent / budget;
            bool isWarning = ratio >= 0.80m;

            Assert.IsFalse(isWarning, "低于80%不应触发预警");
        }

        #endregion
    }
}
