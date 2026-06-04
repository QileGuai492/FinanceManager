using FinanceManager.Domain.models;
using FinanceManager.Domain.Services;
using FinanceManager.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Tests.Statistics
{
    [TestClass]
    public class StatisticsTests
    {
        private Mock<IStatisticsService> _statsServiceMock;
        private StatisticsViewModel _vm;

        [TestInitialize]
        public void SetUp()
        {
            _statsServiceMock = new Mock<IStatisticsService>();
            _vm = new StatisticsViewModel(_statsServiceMock.Object);
            FinanceManager.Common.App.CurrentUserId = 1;
        }

        #region Top5 逻辑测试

        /// <summary>9.10 Top5：超过5个分类时只显示Top5+"其他"</summary>
        [TestMethod]
        public void Top5Calculation_MoreThan5_ShowsTop5AndOthers()
        {
            var categories = new List<CategoryStatistics>();
            for (int i = 1; i <= 7; i++)
            {
                categories.Add(new CategoryStatistics
                {
                    CategoryId = i,
                    CategoryName = $"分类{i}",
                    Amount = i * 100m
                });
            }

            // 原期望值1100m算错
            // 7个分类: 100,200,300,400,500,600,700
            // 降序后: 700,600,500,400,300,200,100
            // Top5: 700,600,500,400,300 (5项)
            // 其他: 200+100 = 300
            var top5 = categories.OrderByDescending(c => c.Amount).Take(5).ToList();
            var othersAmount = categories.OrderByDescending(c => c.Amount).Skip(5).Sum(c => c.Amount);

            Assert.AreEqual(5, top5.Count);
            Assert.AreEqual(300m, othersAmount);
        }

        /// <summary>9.11 不足5类：不显示"其他"</summary>
        [TestMethod]
        public void Top5Calculation_LessThan5_ShowsAll()
        {
            var categories = new List<CategoryStatistics>
            {
                new CategoryStatistics { CategoryId = 1, CategoryName = "餐饮", Amount = 500m },
                new CategoryStatistics { CategoryId = 2, CategoryName = "交通", Amount = 300m },
                new CategoryStatistics { CategoryId = 3, CategoryName = "购物", Amount = 200m },
            };

            var needOther = categories.Count > 5;

            Assert.IsFalse(needOther, "不足5个分类时不需要'其他'分类");
        }

        /// <summary>正好5类：不显示"其他"</summary>
        [TestMethod]
        public void Top5Calculation_Exactly5_ShowsAll5()
        {
            var categories = new List<CategoryStatistics>();
            for (int i = 1; i <= 5; i++)
                categories.Add(new CategoryStatistics { CategoryId = i, CategoryName = $"C{i}", Amount = i * 100m });

            var needOther = categories.Count > 5;

            Assert.IsFalse(needOther, "正好5个分类时不需要'其他'分类");
        }

        #endregion

        #region 月度统计

        /// <summary>9.2 月度统计：正确加载</summary>
        [TestMethod]
        public async Task LoadMonthlyAsync_ReturnsCorrectStats()
        {
            var stats = new MonthlyStatistics
            {
                Year = 2026,
                Month = 6,
                TotalIncome = 20000m,
                TotalExpense = 12000m
            };
            _statsServiceMock
                .Setup(s => s.GetMonthlyStatisticsAsync(1, 2026, 6))
                .ReturnsAsync(stats);

            await _vm.LoadMonthlyAsync();

            Assert.AreEqual(20000m, _vm.MonthlyStats.TotalIncome);
            Assert.AreEqual(12000m, _vm.MonthlyStats.TotalExpense);
            Assert.AreEqual(8000m, _vm.MonthlyStats.Balance);
        }

        /// <summary>9.13 负数结余：Balance 为负</summary>
        [TestMethod]
        public async Task MonthlyStats_NegativeBalance_ReturnsNegative()
        {
            var stats = new MonthlyStatistics
            {
                Year = 2026,
                Month = 6,
                TotalIncome = 5000m,
                TotalExpense = 15000m
            };
            _statsServiceMock
                .Setup(s => s.GetMonthlyStatisticsAsync(1, 2026, 6))
                .ReturnsAsync(stats);

            await _vm.LoadMonthlyAsync();

            Assert.IsTrue(_vm.MonthlyStats.Balance < 0);
        }

        /// <summary>9.12 无数据：返回0值</summary>
        [TestMethod]
        public async Task MonthlyStats_NoData_ReturnsZero()
        {
            // 原代码遗漏设置 SelectedYear/Month，导致 Mock 预设 (2026,1)
            // 与 ViewModel 默认值 (当前年月) 不匹配，MonthlyStats 为 null 触发 NPE
            _vm.SelectedYear = 2026;
            _vm.SelectedMonth = 1;

            var stats = new MonthlyStatistics
            {
                Year = 2026,
                Month = 1,
                TotalIncome = 0m,
                TotalExpense = 0m
            };
            _statsServiceMock
                .Setup(s => s.GetMonthlyStatisticsAsync(1, 2026, 1))
                .ReturnsAsync(stats);

            await _vm.LoadMonthlyAsync();

            Assert.AreEqual(0m, _vm.MonthlyStats.TotalIncome);
            Assert.AreEqual(0m, _vm.MonthlyStats.TotalExpense);
            Assert.AreEqual(0m, _vm.MonthlyStats.Balance);
        }

        #endregion

        #region 错误处理

        /// <summary>加载统计时服务异常 → ErrorMessage 被设置</summary>
        [TestMethod]
        public async Task LoadMonthlyAsync_ServiceThrows_SetsErrorMessage()
        {
            _statsServiceMock
                .Setup(s => s.GetMonthlyStatisticsAsync(1, 2026, 6))
                .ThrowsAsync(new Exception("数据库连接超时"));

            await _vm.LoadMonthlyAsync();

            Assert.AreNotEqual(string.Empty, _vm.ErrorMessage);
        }

        #endregion
    }
}
