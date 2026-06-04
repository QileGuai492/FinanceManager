using FinanceManager.Data.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
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
    public class RecordServiceTests : TestBase
    {
        private RecordService CreateService() => new RecordService(RecordRepoMock.Object);

        /// <summary>3.1 新增收入：返回新记录 ID</summary>
        [TestMethod]
        public async Task AddRecordAsync_ValidIncome_ReturnsId()
        {
            var record = new RecordEntity
            {
                Amount = 5000m,
                Type = RecordType.Income,
                CategoryId = 1,
                UserId = 1,
                Date = DateTime.Today
            };
            RecordRepoMock.Setup(r => r.InsertAsync(It.IsAny<RecordEntity>())).ReturnsAsync(10);

            var service = CreateService();
            var id = await service.AddRecordAsync(record);

            Assert.AreEqual(10, id);
            RecordRepoMock.Verify(r => r.InsertAsync(It.Is<RecordEntity>(rec =>
                rec.Amount == 5000m && rec.Type == RecordType.Income)), Times.Once);
        }

        /// <summary>3.2 新增支出：返回新记录 ID</summary>
        [TestMethod]
        public async Task AddRecordAsync_ValidExpense_ReturnsId()
        {
            var record = new RecordEntity
            {
                Amount = 200m,
                Type = RecordType.Expense,
                CategoryId = 3,
                UserId = 1,
                Date = DateTime.Today
            };
            RecordRepoMock.Setup(r => r.InsertAsync(It.IsAny<RecordEntity>())).ReturnsAsync(11);

            var service = CreateService();
            var id = await service.AddRecordAsync(record);

            Assert.AreEqual(11, id);
            RecordRepoMock.Verify(r => r.InsertAsync(It.Is<RecordEntity>(rec =>
                rec.Type == RecordType.Expense)), Times.Once);
        }

        /// <summary>3.3 空金额（0元）：仍可正常添加</summary>
        [TestMethod]
        public async Task AddRecordAsync_ZeroAmount_Succeeds()
        {
            var record = new RecordEntity
            {
                Amount = 0m,
                Type = RecordType.Expense,
                CategoryId = 2,
                UserId = 1,
                Date = DateTime.Today
            };
            RecordRepoMock.Setup(r => r.InsertAsync(It.IsAny<RecordEntity>())).ReturnsAsync(12);

            var service = CreateService();
            var id = await service.AddRecordAsync(record);

            Assert.AreEqual(12, id);
        }

        /// <summary>3.5 编辑记录：调用 Update</summary>
        [TestMethod]
        public async Task UpdateRecordAsync_ModifiedRecord_CallsUpdate()
        {
            var record = new RecordEntity
            {
                Id = 5,
                Amount = 999m,
                Type = RecordType.Expense,
                CategoryId = 4,
                UserId = 1
            };

            var service = CreateService();
            await service.UpdateRecordAsync(record);

            RecordRepoMock.Verify(r => r.UpdateAsync(It.IsAny<RecordEntity>()), Times.Once);
        }

        /// <summary>3.7 删除记录：调用 Delete</summary>
        [TestMethod]
        public async Task DeleteRecordAsync_ValidId_CallsDelete()
        {
            var service = CreateService();
            await service.DeleteRecordAsync(5);

            RecordRepoMock.Verify(r => r.DeleteAsync(5), Times.Once);
        }

        #region 筛选查询

        /// <summary>3.10/3.11 按类型查询：传入 type=0(支出) 查询</summary>
        [TestMethod]
        public async Task GetTotalAmountByTypeAsync_ExpenseType_ReturnsSum()
        {
            RecordRepoMock
                .Setup(r => r.GetSumByTypeAndDateRangeAsync(1, 0,
                    It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(3500m);

            var service = CreateService();
            var total = await service.GetTotalAmountByTypeAsync(1, 0,
                new DateTime(2026, 6, 1), new DateTime(2026, 6, 30));

            Assert.AreEqual(3500m, total);
        }

        /// <summary>3.12 按分类+类型查询</summary>
        [TestMethod]
        public async Task GetTotalAmountByCategoryAsync_SpecificCategory_ReturnsSum()
        {
            RecordRepoMock
                .Setup(r => r.GetSumByCategoryAndTypeAndDateRangeAsync(
                    1, 3, 0, It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(800m); // 餐饮支出800元

            var service = CreateService();
            var total = await service.GetTotalAmountByCategoryAsync(
                1, 3, 0, new DateTime(2026, 6, 1), new DateTime(2026, 6, 30));

            Assert.AreEqual(800m, total);
        }

        /// <summary>3.4 未选分类：categoryId 为 0 或负数时查询结果为 0</summary>
        [TestMethod]
        public async Task GetTotalAmountByCategoryAsync_InvalidCategory_ReturnsZero()
        {
            RecordRepoMock
                .Setup(r => r.GetSumByCategoryAndTypeAndDateRangeAsync(
                    1, 0, 0, It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(0m);

            var service = CreateService();
            var total = await service.GetTotalAmountByCategoryAsync(
                1, 0, 0, new DateTime(2026, 6, 1), new DateTime(2026, 6, 30));

            Assert.AreEqual(0m, total);
        }

        #endregion
    }
}
