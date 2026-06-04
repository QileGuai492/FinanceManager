using FinanceManager.Domain.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// TestBase.cs — 所有测试类的基类，提供通用 Mock 工厂
namespace FinanceManager.Tests
{
    [TestClass]
    public abstract class TestBase
    {
        protected Mock<IUserRepository> UserRepoMock { get; private set; }
        protected Mock<IRecordRepository> RecordRepoMock { get; private set; }
        protected Mock<ICategoryRepository> CategoryRepoMock { get; private set; }
        protected Mock<ITemplateRepository> TemplateRepoMock { get; private set; }
        protected Mock<IBudgetRepository> BudgetRepoMock { get; private set; }
        protected Mock<ICategoryBudgetRepository> CategoryBudgetRepoMock { get; private set; }

        /// <summary>在每个测试方法前初始化 Mock 对象</summary>
        [TestInitialize]
        public virtual void SetUp()
        {
            UserRepoMock = new Mock<IUserRepository>();
            RecordRepoMock = new Mock<IRecordRepository>();
            CategoryRepoMock = new Mock<ICategoryRepository>();
            TemplateRepoMock = new Mock<ITemplateRepository>();
            BudgetRepoMock = new Mock<IBudgetRepository>();
            CategoryBudgetRepoMock = new Mock<ICategoryBudgetRepository>();
        }
    }
}
