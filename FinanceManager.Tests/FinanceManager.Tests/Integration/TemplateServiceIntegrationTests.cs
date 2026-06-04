using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Tests.Integration
{
    [TestClass]
    public class TemplateServiceIntegrationTests : IntegrationTestBase
    {
        /// <summary>新增模板 → 查回验证</summary>
        [TestMethod]
        public async Task AddTemplate_PersistsToDatabase()
        {
            var categories = await CategoryService.GetCategoriesByTypeAsync(TestUserId, (int)RecordType.Expense);
            var template = new TemplateEntity
            {
                Name = "午餐",
                DefaultAmount = 30m,
                Type = RecordType.Expense,
                CategoryId = categories.First().Id,
                UserId = TestUserId
            };

            var id = await TemplateService.AddTemplateAsync(template);
            Assert.IsTrue(id > 0);

            var loaded = await TemplateService.GetTemplateByIdAsync(id);
            Assert.AreEqual("午餐", loaded.Name);
            Assert.AreEqual(30m, loaded.DefaultAmount);
        }

        /// <summary>使用次数递增</summary>
        [TestMethod]
        public async Task IncrementUseCount_IncreasesByOne()
        {
            var categories = await CategoryService.GetCategoriesByTypeAsync(TestUserId, (int)RecordType.Expense);
            var template = new TemplateEntity
            {
                Name = "交通",
                DefaultAmount = 10m,
                Type = RecordType.Expense,
                CategoryId = categories.First().Id,
                UserId = TestUserId
            };
            var id = await TemplateService.AddTemplateAsync(template);

            await TemplateService.IncrementUseCountAsync(id);
            var loaded = await TemplateService.GetTemplateByIdAsync(id);

            Assert.AreEqual(1, loaded.UseCount);
        }

        /// <summary>收藏模板 → 只看常用能过滤出来</summary>
        [TestMethod]
        public async Task FavoriteTemplate_FilteredByShowFavorites()
        {
            var categories = await CategoryService.GetCategoriesByTypeAsync(TestUserId, (int)RecordType.Expense);
            var catId = categories.First().Id;

            await TemplateService.AddTemplateAsync(new TemplateEntity
            {
                Name = "常用",
                DefaultAmount = 50m,
                Type = RecordType.Expense,
                CategoryId = catId,
                UserId = TestUserId,
                IsFavorite = true
            });
            await TemplateService.AddTemplateAsync(new TemplateEntity
            {
                Name = "不常用",
                DefaultAmount = 100m,
                Type = RecordType.Expense,
                CategoryId = catId,
                UserId = TestUserId,
                IsFavorite = false
            });

            var all = await TemplateService.GetTemplatesAsync(TestUserId);
            var favorites = await TemplateService.GetFavoriteTemplatesAsync(TestUserId);

            Assert.AreEqual(2, all.Count());
            Assert.AreEqual(1, favorites.Count());
            Assert.AreEqual("常用", favorites.First().Name);
        }

        /// <summary>删除模板</summary>
        [TestMethod]
        public async Task DeleteTemplate_RemovesFromDatabase()
        {
            var categories = await CategoryService.GetCategoriesByTypeAsync(TestUserId, (int)RecordType.Expense);
            var template = new TemplateEntity
            {
                Name = "待删除",
                DefaultAmount = 0m,
                Type = RecordType.Expense,
                CategoryId = categories.First().Id,
                UserId = TestUserId
            };
            var id = await TemplateService.AddTemplateAsync(template);

            await TemplateService.DeleteTemplateAsync(id);

            var loaded = await TemplateService.GetTemplateByIdAsync(id);
            Assert.IsNull(loaded);
        }
    }
}
