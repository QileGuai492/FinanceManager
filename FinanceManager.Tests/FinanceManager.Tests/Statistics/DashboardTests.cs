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

// 注意：仪表盘逻辑分散在 StatisticsViewModel 和各个 UserControl 中，
// 此处针对核心 ViewModel 属性进行测试
namespace FinanceManager.Tests.Statistics
{
    [TestClass]
    public class DashboardTests
    {
        private Mock<IStatisticsService> _statsServiceMock;
        private StatisticsViewModel _vm;

        [TestInitialize]
        public void SetUp()
        {
            _statsServiceMock = new Mock<IStatisticsService>();
            _vm = new StatisticsViewModel(_statsServiceMock.Object);
            FinanceManager.Common.App.CurrentUserId = 1;
            FinanceManager.Common.App.CurrentUsername = "测试用户";
        }

        /// <summary>2.3 本月收支卡片：加载月度统计后三张卡片数据正确</summary>
        [TestMethod]
        public async Task MonthlyStats_Loads_CorrectIncomeExpenseBalance()
        {
            var stats = new MonthlyStatistics
            {
                Year = 2026,
                Month = 6,
                TotalIncome = 15000m,
                TotalExpense = 8000m
            };
            _statsServiceMock
                .Setup(s => s.GetMonthlyStatisticsAsync(1, 2026, 6, null))
                .ReturnsAsync(stats);

            await _vm.LoadMonthlyAsync();

            Assert.AreEqual(15000m, _vm.MonthlyStats.TotalIncome);
            Assert.AreEqual(8000m, _vm.MonthlyStats.TotalExpense);
            Assert.AreEqual(7000m, _vm.MonthlyStats.Balance); // 15000 - 8000
        }

        /// <summary>2.4 负数结余：Balance 为负数</summary>
        [TestMethod]
        public async Task MonthlyStats_NegativeBalance_CorrectlyCalculated()
        {
            var stats = new MonthlyStatistics
            {
                Year = 2026,
                Month = 6,
                TotalIncome = 5000m,
                TotalExpense = 12000m
            };
            _statsServiceMock
                .Setup(s => s.GetMonthlyStatisticsAsync(1, 2026, 6, null))
                .ReturnsAsync(stats);

            await _vm.LoadMonthlyAsync();

            Assert.IsTrue(_vm.MonthlyStats.Balance < 0,
                "支出大于收入时结余应为负数");
            Assert.AreEqual(-7000m, _vm.MonthlyStats.Balance);
        }

        /// <summary>月度统计加载失败时设置错误信息</summary>
        [TestMethod]
        public async Task MonthlyStats_LoadError_SetsErrorMessage()
        {
            _statsServiceMock
                .Setup(s => s.GetMonthlyStatisticsAsync(1, 2026, 6, null))
                .ThrowsAsync(new System.Exception("数据库连接失败"));

            await _vm.LoadMonthlyAsync();

            Assert.AreNotEqual(string.Empty, _vm.ErrorMessage);
        }

        /// <summary>2.6 默认年份为当前年份</summary>
        [TestMethod]
        public void DefaultValues_YearIsCurrentYear()
        {
            var now = System.DateTime.Now;
            Assert.AreEqual(now.Year, _vm.SelectedYear);
            Assert.AreEqual(now.Month, _vm.SelectedMonth);
        }
    }
}
