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
    public class TemplateServiceTests : TestBase
    {
        private TemplateService CreateService() => new TemplateService(TemplateRepoMock.Object);

        /// <summary>4.2 新增模板：保存成功返回 ID</summary>
        [TestMethod]
        public async Task AddTemplateAsync_ValidTemplate_ReturnsId()
        {
            var template = new TemplateEntity
            {
                Name = "午餐报销",
                DefaultAmount = 30m,
                Type = RecordType.Expense,
                CategoryId = 3,
                UserId = 1
            };
            TemplateRepoMock.Setup(r => r.InsertAsync(It.IsAny<TemplateEntity>()))
                            .ReturnsAsync(1);

            var service = CreateService();
            var id = await service.AddTemplateAsync(template);

            Assert.AreEqual(1, id);
            TemplateRepoMock.Verify(r => r.InsertAsync(It.Is<TemplateEntity>(t =>
                t.Name == "午餐报销" && t.DefaultAmount == 30m)), Times.Once);
        }

        /// <summary>4.3 金额可为空/0：正常保存</summary>
        [TestMethod]
        public async Task AddTemplateAsync_ZeroAmount_Succeeds()
        {
            var template = new TemplateEntity
            {
                Name = "零金额模板",
                DefaultAmount = 0m,
                Type = RecordType.Expense,
                CategoryId = 3,
                UserId = 1
            };
            TemplateRepoMock.Setup(r => r.InsertAsync(It.IsAny<TemplateEntity>()))
                            .ReturnsAsync(2);

            var service = CreateService();
            var id = await service.AddTemplateAsync(template);

            Assert.AreEqual(2, id);
        }

        /// <summary>4.5 编辑模板：调用 Update</summary>
        [TestMethod]
        public async Task UpdateTemplateAsync_ModifiedTemplate_CallsUpdate()
        {
            var template = new TemplateEntity
            {
                Id = 1,
                Name = "修改后模板",
                DefaultAmount = 50m,
                Type = RecordType.Expense,
                CategoryId = 3,
                UserId = 1
            };

            var service = CreateService();
            await service.UpdateTemplateAsync(template);

            TemplateRepoMock.Verify(r => r.UpdateAsync(It.IsAny<TemplateEntity>()), Times.Once);
        }

        /// <summary>4.6 删除模板：调用 Delete</summary>
        [TestMethod]
        public async Task DeleteTemplateAsync_ValidId_CallsDelete()
        {
            var service = CreateService();
            await service.DeleteTemplateAsync(5);

            TemplateRepoMock.Verify(r => r.DeleteAsync(5), Times.Once);
        }

        /// <summary>4.7 只看常用：获取收藏模板</summary>
        [TestMethod]
        public async Task GetFavoriteTemplatesAsync_ReturnsFavoritesOnly()
        {
            var favorites = new List<TemplateEntity>
            {
                new TemplateEntity { Id = 1, Name = "常用1", IsFavorite = true, UserId = 1 },
                new TemplateEntity { Id = 3, Name = "常用3", IsFavorite = true, UserId = 1 }
            };
            TemplateRepoMock.Setup(r => r.GetFavoriteByUserIdAsync(1))
                            .ReturnsAsync(favorites);

            var service = CreateService();
            var result = await service.GetFavoriteTemplatesAsync(1);

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(result.All(t => t.IsFavorite));
        }

        /// <summary>4.10 使用模板：使用次数+1</summary>
        [TestMethod]
        public async Task IncrementUseCountAsync_ValidId_CallsRepo()
        {
            var service = CreateService();
            await service.IncrementUseCountAsync(1);

            TemplateRepoMock.Verify(r => r.IncrementUseCountAsync(1), Times.Once);
        }

        /// <summary>4.11 模板数量上限检查</summary>
        [TestMethod]
        public async Task GetTemplateCountAsync_ReturnsCurrentCount()
        {
            TemplateRepoMock.Setup(r => r.GetCountByUserIdAsync(1)).ReturnsAsync(48);

            var service = CreateService();
            var count = await service.GetTemplateCountAsync(1);

            Assert.AreEqual(48, count);
        }

        /// <summary>4.13 数量显示：获取当前已建数量</summary>
        [TestMethod]
        public async Task GetTemplateCountAsync_NearLimit_ReturnsCount()
        {
            TemplateRepoMock.Setup(r => r.GetCountByUserIdAsync(1)).ReturnsAsync(50);

            var service = CreateService();
            var count = await service.GetTemplateCountAsync(1);

            Assert.AreEqual(50, count, "数量显示应为'已建数/上限'格式");
        }
    }
}
