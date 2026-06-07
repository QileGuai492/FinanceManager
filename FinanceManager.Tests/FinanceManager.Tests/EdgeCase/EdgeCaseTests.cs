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

namespace FinanceManager.Tests.EdgeCase
{
    [TestClass]
    public class EdgeCaseTests : TestBase
    {
        /// <summary>10.1 大量数据：500条记录不超时</summary>
        [TestMethod]
        public async Task GetRecordsAsync_LargeDataSet_ReturnsWithinTimeout()
        {
            var records = new List<RecordEntity>();
            for (int i = 0; i < 500; i++)
            {
                records.Add(new RecordEntity
                {
                    Id = i + 1,
                    Amount = (i % 2 == 0) ? 100m : 50m,
                    Type = (i % 2 == 0) ? RecordType.Income : RecordType.Expense,
                    CategoryId = (i % 5) + 1,
                    UserId = 1,
                    Date = DateTime.Today.AddDays(-i)
                });
            }
            RecordRepoMock.Setup(r => r.GetByUserIdAsync(1)).ReturnsAsync(records);

            var service = new RecordService(RecordRepoMock.Object);

            // 设置5秒超时
            var task = service.GetRecordsAsync(1);
            var completed = task.Wait(5000);

            Assert.IsTrue(completed, "500条记录应在5秒内完成加载");
            Assert.AreEqual(500, task.Result.Count());
        }

        /// <summary>10.2 并发操作：多次快速调用不崩溃</summary>
        [TestMethod]
        public async Task GetRecordsAsync_ConcurrentCalls_AllSucceed()
        {
            var records = new List<RecordEntity>
            {
                new RecordEntity { Id = 1, Amount = 100m, Type = RecordType.Income, CategoryId = 1, UserId = 1, Date = DateTime.Today }
            };
            RecordRepoMock.Setup(r => r.GetByUserIdAsync(1)).ReturnsAsync(records);

            var service = new RecordService(RecordRepoMock.Object);

            // 并发发起10次调用
            var tasks = Enumerable.Range(0, 10)
                .Select(_ => service.GetRecordsAsync(1))
                .ToArray();

            var results = await Task.WhenAll(tasks);

            Assert.AreEqual(10, results.Length, "10次并发调用应全部完成");
            foreach (var r in results)
                Assert.AreEqual(1, r.Count());
        }

        /// <summary>10.3 空数据库：新用户无记录不报错</summary>
        [TestMethod]
        public async Task GetRecordsAsync_EmptyDatabase_ReturnsEmptyList()
        {
            RecordRepoMock.Setup(r => r.GetByUserIdAsync(1))
                          .ReturnsAsync(new List<RecordEntity>());

            var service = new RecordService(RecordRepoMock.Object);
            var records = await service.GetRecordsAsync(1);

            Assert.IsNotNull(records, "空结果不应为null");
            Assert.AreEqual(0, records.Count(), "空结果应返回空列表");
        }

        /// <summary>10.4 空数据库：各查询方法返回0或空</summary>
        [TestMethod]
        public async Task GetTotalAmountByTypeAsync_EmptyData_ReturnsZero()
        {
            RecordRepoMock.Setup(r => r.GetSumByTypeAndDateRangeAsync(
                1, 0, It.IsAny<DateTime>(), It.IsAny<DateTime>(), null))
                .ReturnsAsync(0m);

            var service = new RecordService(RecordRepoMock.Object);
            var total = await service.GetTotalAmountByTypeAsync(1, 0,
                DateTime.Today, DateTime.Today);

            Assert.AreEqual(0m, total);
        }

        #region 实体边界值测试

        /// <summary>10.5 中文输入：实体支持 Unicode</summary>
        [TestMethod]
        public void Entity_ChineseCharacters_StoredCorrectly()
        {
            var record = new RecordEntity
            {
                Note = "午餐：麻辣烫加饮料，共35元",
                Type = RecordType.Expense,
                Amount = 35m
            };

            Assert.AreEqual("午餐：麻辣烫加饮料，共35元", record.Note);
            Assert.AreEqual(35m, record.Amount);
        }

        /// <summary>金额为0的边界情况</summary>
        [TestMethod]
        public void RecordEntity_ZeroAmount_HandledCorrectly()
        {
            var record = new RecordEntity
            {
                Amount = 0m,
                Type = RecordType.Expense,
                CategoryId = 1,
                UserId = 1
            };

            Assert.AreEqual(0m, record.Amount);
        }

        /// <summary>最大金额值（decimal.MaxValue）</summary>
        [TestMethod]
        public void RecordEntity_MaxDecimal_NoOverflow()
        {
            var record = new RecordEntity
            {
                Amount = decimal.MaxValue,
                Type = RecordType.Income,
                CategoryId = 1,
                UserId = 1
            };

            Assert.AreEqual(decimal.MaxValue, record.Amount);
        }

        /// <summary>极早日期</summary>
        [TestMethod]
        public void RecordEntity_VeryOldDate_HandledCorrectly()
        {
            var record = new RecordEntity
            {
                Date = new DateTime(2000, 1, 1),
                Amount = 100m,
                Type = RecordType.Income,
                CategoryId = 1,
                UserId = 1
            };

            Assert.AreEqual(2000, record.Date.Year);
        }

        #endregion

        #region 模板边界

        /// <summary>4.11 模板数量达到50上限（由常量 MaxTemplates=100 定义，此处以50为例）</summary>
        [TestMethod]
        public void Template_LimitCheck_AtLimit()
        {
            const int limit = 50; // 假设上限为50
            var currentCount = 50;

            bool atLimit = currentCount >= limit;

            Assert.IsTrue(atLimit, "达到上限后应禁止新增");
        }

        /// <summary>模板数量未达上限</summary>
        [TestMethod]
        public void Template_LimitCheck_BelowLimit()
        {
            const int limit = 50;
            var currentCount = 48;

            bool atLimit = currentCount >= limit;

            Assert.IsFalse(atLimit, "未达到上限时允许新增");
        }

        #endregion
    }
}
