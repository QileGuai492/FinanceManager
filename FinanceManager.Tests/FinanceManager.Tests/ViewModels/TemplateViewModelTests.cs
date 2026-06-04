using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
using FinanceManager.Domain.Services;
using FinanceManager.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Tests.ViewModels
{
    [TestClass]
    public class TemplateViewModelTests
    {
        private Mock<ITemplateService> _templateServiceMock;
        private TemplateViewModel _vm;

        [TestInitialize]
        public void SetUp()
        {
            _templateServiceMock = new Mock<ITemplateService>();
            _vm = new TemplateViewModel(_templateServiceMock.Object);
            FinanceManager.Common.App.CurrentUserId = 1;
        }

        /// <summary>4.4 空名称 → ErrorMessage 不为空</summary>
        [TestMethod]
        public async Task AddTemplateCommand_EmptyName_ShowsError()
        {
            _vm.TemplateName = "";
            _vm.SelectedCategoryId = 1;
            _vm.SelectedType = (int)RecordType.Expense;

            _vm.AddTemplateCommand.Execute(null);
            await Task.Delay(100);

            Assert.AreNotEqual(string.Empty, _vm.ErrorMessage);
        }

        /// <summary>4.7 切换"只看常用" → 调用 GetFavoriteTemplatesAsync</summary>
        [TestMethod]
        public async Task ShowFavoritesOnly_Toggled_LoadsFavorites()
        {
            var favorites = new List<TemplateEntity>
            {
                new TemplateEntity { Id = 2, Name = "常用模板", IsFavorite = true, UserId = 1 }
            };
            _templateServiceMock.Setup(s => s.GetFavoriteTemplatesAsync(1))
                                .ReturnsAsync(favorites);
            _templateServiceMock.Setup(s => s.GetTemplatesAsync(1))
                                .ReturnsAsync(new List<TemplateEntity>());

            _vm.ShowFavoritesOnly = true;
            await Task.Delay(100);

            Assert.AreEqual(1, _vm.Templates.Count);
            Assert.IsTrue(_vm.Templates[0].IsFavorite);
        }

        /// <summary>4.9 使用模板：触发 OnTemplateSelected 事件</summary>
        [TestMethod]
        public void UseTemplate_ValidTemplate_FiresEvent()
        {
            TemplateEntity selectedTemplate = null;
            _vm.OnTemplateSelected += (s, t) => selectedTemplate = t;

            var template = new TemplateEntity { Id = 1, Name = "午餐", DefaultAmount = 30m };
            _vm.UseTemplateCommand.Execute(template);

            Assert.IsNotNull(selectedTemplate);
            Assert.AreEqual("午餐", selectedTemplate.Name);
        }

        /// <summary>4.8 常用标记切换</summary>
        [TestMethod]
        public async Task ToggleFavoriteCommand_FlipsIsFavorite()
        {
            var template = new TemplateEntity
            {
                Id = 5,
                Name = "可变模板",
                IsFavorite = false,
                UserId = 1
            };
            _templateServiceMock.Setup(s => s.UpdateTemplateAsync(It.IsAny<TemplateEntity>()))
                                .Returns(Task.CompletedTask);
            _templateServiceMock.Setup(s => s.GetTemplatesAsync(1))
                                .ReturnsAsync(new List<TemplateEntity>
                                {
                                    new TemplateEntity { Id = 5, Name = "可变模板", IsFavorite = true, UserId = 1 }
                                });

            _vm.ToggleFavoriteCommand.Execute(template);
            await Task.Delay(100);

            _templateServiceMock.Verify(s => s.UpdateTemplateAsync(
                It.Is<TemplateEntity>(t => t.IsFavorite == true)), Times.Once);
        }
    }
}
