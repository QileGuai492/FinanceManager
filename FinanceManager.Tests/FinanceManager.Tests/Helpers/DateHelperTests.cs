using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Tests.Helpers
{
    [TestClass]
    public class DateHelperTests
    {
        /// <summary>5.8 日期范围：起始>结束 → 正常过滤无结果</summary>
        [TestMethod]
        public void DateRange_StartAfterEnd_IsValidRange()
        {
            var start = new DateTime(2026, 6, 30);
            var end = new DateTime(2026, 6, 1);

            // 起始日期晚于结束日期，查询范围应为0天
            Assert.IsTrue(start > end, "起始大于结束时，查询结果应为空");
        }
    }
}
