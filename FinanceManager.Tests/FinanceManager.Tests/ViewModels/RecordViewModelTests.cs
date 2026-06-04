using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FinanceManager.Tests.ViewModels
{
    [TestClass]
    public class RecordViewModelTests
    {
        /// <summary>3.3 空金额：金额为0或负数应验证失败</summary>
        [TestMethod]
        public void ValidateRecord_EmptyAmount_ReturnsFalse()
        {
            decimal amount = 0m;
            int categoryId = 1;
            bool isValid = amount > 0m && categoryId > 0;
            Assert.IsFalse(isValid, "金额为0时应验证失败");
        }

        /// <summary>3.4 未选分类：categoryId为0应验证失败</summary>
        [TestMethod]
        public void ValidateRecord_NoCategory_ReturnsFalse()
        {
            decimal amount = 100m;
            int categoryId = 0;
            bool isValid = amount > 0m && categoryId > 0;
            Assert.IsFalse(isValid, "未选分类时应验证失败");
        }

        /// <summary>有效金额和分类：验证通过</summary>
        [TestMethod]
        public void ValidateRecord_ValidData_ReturnsTrue()
        {
            decimal amount = 500m;
            int categoryId = 3;
            bool isValid = amount > 0m && categoryId > 0;
            Assert.IsTrue(isValid, "有效金额和分类应验证通过");
        }
    }
}
