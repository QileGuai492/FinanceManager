using FinanceManager.Common.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Tests.Helpers
{
    [TestClass]
    public class CsvHelperTests
    {
        #region 导出测试

        /// <summary>5.3 导出CSV：生成正确的表头和内容</summary>
        [TestMethod]
        public void ToCsv_ValidRows_GeneratesCorrectCsv()
        {
            var rows = new List<string[]>
            {
                new[] { "日期", "类型", "分类", "金额" },
                new[] { "2026-06-01", "支出", "餐饮", "35.00" },
                new[] { "2026-06-02", "收入", "工资", "15000.00" }
            };

            var csv = CsvHelper.ToCsv(rows);

            StringAssert.Contains(csv, "日期,类型,分类,金额");
            StringAssert.Contains(csv, "2026-06-01,支出,餐饮,35.00");
        }

        /// <summary>包含逗号的字段用引号包裹</summary>
        [TestMethod]
        public void ToCsv_CellContainsComma_WrapsInQuotes()
        {
            var rows = new List<string[]>
            {
                new[] { "备注", "金额" },
                new[] { "午餐,加饮料", "50.00" }
            };

            var csv = CsvHelper.ToCsv(rows);

            StringAssert.Contains(csv, "\"午餐,加饮料\"");
        }

        /// <summary>包含引号的字段被转义</summary>
        [TestMethod]
        public void ToCsv_CellContainsQuote_EscapesCorrectly()
        {
            var rows = new List<string[]>
            {
                new[] { "备注" },
                new[] { "他说\"你好\"" }
            };

            var csv = CsvHelper.ToCsv(rows);

            // 内部引号被双写转义
            StringAssert.Contains(csv, "\"他说\"\"你好\"\"\"");
        }

        #endregion

        #region 导入测试

        /// <summary>5.5 导入CSV：正确解析</summary>
        [TestMethod]
        public void ParseCsv_ValidCsv_ReturnsCorrectRows()
        {
            var csv = "日期,类型,分类,金额\n2026-06-01,支出,餐饮,35.00\n2026-06-02,收入,工资,15000.00\n";

            var rows = CsvHelper.ParseCsv(csv);

            Assert.AreEqual(3, rows.Count);
            Assert.AreEqual("2026-06-01", rows[1][0]);
            Assert.AreEqual("支出", rows[1][1]);
            Assert.AreEqual("餐饮", rows[1][2]);
            Assert.AreEqual("35.00", rows[1][3]);
        }

        /// <summary>5.6 格式错误的CSV（列数不足）：能解析但列数不同</summary>
        [TestMethod]
        public void ParseCsv_MalformedCsv_ParsedWithFewerColumns()
        {
            // 缺少"金额"列的 CSV
            var csv = "日期,类型,分类,金额\n2026-06-01,支出\n";

            var rows = CsvHelper.ParseCsv(csv);

            Assert.AreEqual(2, rows.Count);
            Assert.AreEqual(2, rows[1].Length, "第二行只有2列（日期、支出），缺少分类和金额");
        }

        /// <summary>空CSV：返回空列表</summary>
        [TestMethod]
        public void ParseCsv_EmptyContent_ReturnsEmptyList()
        {
            var csv = "";

            var rows = CsvHelper.ParseCsv(csv);

            Assert.AreEqual(0, rows.Count);
        }

        /// <summary>5.5 包含引号字段的导入</summary>
        [TestMethod]
        public void ParseCsv_QuotedFields_ParsesCorrectly()
        {
            var csv = "备注,金额\n\"午餐,加饮料\",50.00\n";

            var rows = CsvHelper.ParseCsv(csv);

            Assert.AreEqual(2, rows.Count);
            Assert.AreEqual("午餐,加饮料", rows[1][0], "引号内的逗号应被正确解析");
            Assert.AreEqual("50.00", rows[1][1]);
        }

        #endregion

        #region 从 CsvHelper 输出转回解析（往返测试）

        /// <summary>5.5 导出再导入：数据一致</summary>
        [TestMethod]
        public void ToCsv_ThenParseCsv_RoundTrip()
        {
            var original = new List<string[]>
            {
                new[] { "日期", "类型", "分类", "金额", "备注" },
                new[] { "2026-06-01", "支出", "餐饮", "35.00", "午餐" },
                new[] { "2026-06-15", "收入", "工资", "15000.00", "6月工资" }
            };

            var csv = CsvHelper.ToCsv(original);
            var parsed = CsvHelper.ParseCsv(csv);

            Assert.AreEqual(original.Count, parsed.Count);
            for (int i = 0; i < original.Count; i++)
            {
                CollectionAssert.AreEqual(original[i], parsed[i],
                    $"第{i}行数据在往返后应一致");
            }
        }

        #endregion
    }
}
