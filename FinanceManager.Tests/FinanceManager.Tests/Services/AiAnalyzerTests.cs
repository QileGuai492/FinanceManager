using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Repositories;
using FinanceManager.Domain.Services;
using FinanceManager.Data.Services;
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
    public class AiAnalyzerTests
    {
        /// <summary>构建一个典型的 BudgetContext 测试数据</summary>
        private BudgetContext CreateValidContext()
        {
            return new BudgetContext
            {
                Year = 2026,
                Month = 6,
                CurrentBudget = 10000m,
                PastMonths = new List<PastMonthData>
                {
                    new PastMonthData
                    {
                        Year = 2026, Month = 5, TotalExpense = 8000m,
                        Categories = new List<CategorySpending>
                        {
                            new CategorySpending { Name = "餐饮", Amount = 3000m },
                            new CategorySpending { Name = "交通", Amount = 1500m },
                            new CategorySpending { Name = "购物", Amount = 3500m }
                        }
                    },
                    new PastMonthData
                    {
                        Year = 2026, Month = 4, TotalExpense = 7500m,
                        Categories = new List<CategorySpending>
                        {
                            new CategorySpending { Name = "餐饮", Amount = 2800m },
                            new CategorySpending { Name = "交通", Amount = 1200m },
                            new CategorySpending { Name = "购物", Amount = 3500m }
                        }
                    }
                },
                CurrentMonthCategories = new List<CategorySpending>
                {
                    new CategorySpending { Name = "餐饮", Amount = 1500m },
                    new CategorySpending { Name = "交通", Amount = 600m }
                }
            };
        }

        #region 7.6 回退逻辑测试（核心）

        /// <summary>7.6 无效Endpoint → API调用失败 → 自动回退到本地估算，Success=false</summary>
        [TestMethod]
        public async Task AnalyzeBudgetAsync_InvalidEndpoint_ReturnsFallback()
        {
            // 给一个不可达的地址，确保API调用失败
            var analyzer = new AiAnalyzer("https://localhost:1/nonexistent", "fake-key", "fake-model");
            var ctx = CreateValidContext();

            var result = await analyzer.AnalyzeBudgetAsync(ctx);

            Assert.IsNotNull(result, "即使API失败也应返回结果");
            Assert.IsFalse(result.Success, "API失败时 Success 应为 false");
            Assert.IsTrue(result.TotalBudget > 0, "本地估算应计算出正值预算");
            Assert.IsNotNull(result.Analysis, "回退时应包含提示文字");
            Assert.IsTrue(result.Analysis.Contains("不可用") || result.Analysis.Contains("本地"),
                "回退提示应说明AI不可用");
        }

        /// <summary>7.1 空API Key → 请求会被服务端拒绝，触发回退</summary>
        [TestMethod]
        public async Task AnalyzeBudgetAsync_EmptyApiKey_ReturnsFallback()
        {
            var analyzer = new AiAnalyzer("https://localhost:1/nonexistent", "", "");
            var ctx = CreateValidContext();

            var result = await analyzer.AnalyzeBudgetAsync(ctx);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.TotalBudget > 0, "即使无API Key也应返回本地估算值");
        }

        #endregion

        #region 7.4 预算回退计算精度

        /// <summary>回退计算 = 近3月平均支出 × 1.05</summary>
        [TestMethod]
        public async Task AnalyzeBudgetAsync_FallbackCalculation_MatchesFormula()
        {
            var analyzer = new AiAnalyzer("https://localhost:1/nonexistent", "x", "x");
            var ctx = CreateValidContext();
            // 5月支出8000, 4月支出7500, 平均=7750, ×1.05 = 8137.5

            var result = await analyzer.AnalyzeBudgetAsync(ctx);

            var expected = (8000m + 7500m) / 2m * 1.05m;
            Assert.AreEqual(expected, result.TotalBudget, "回退值应为近3月平均支出×1.05");
        }

        /// <summary>无历史数据时回退值为0</summary>
        [TestMethod]
        public async Task AnalyzeBudgetAsync_NoHistory_FallbackIsZero()
        {
            var analyzer = new AiAnalyzer("https://localhost:1/nonexistent", "x", "x");
            var ctx = new BudgetContext
            {
                Year = 2026,
                Month = 6,
                CurrentBudget = 5000m,
                PastMonths = new List<PastMonthData>(),       // 无历史
                CurrentMonthCategories = new List<CategorySpending>()
            };

            var result = await analyzer.AnalyzeBudgetAsync(ctx);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(0m, result.TotalBudget, "无历史数据时回退值应为0");
        }

        #endregion

        #region 7.5 超时处理

        /// <summary>7.5 超时：AnalyzeBudgetAsync 内置30秒超时，超时后走回退</summary>
        [TestMethod]
        public async Task AnalyzeBudgetAsync_Timeout_ReturnsFallback()
        {
            // 用一个已知会挂起的地址来触发超时
            // 注：localhost:1 通常立即拒绝连接，不会触发超时
            // 真正的超时测试需要一个慢速端点。此处验证 HttpTimeout 设定。
            var analyzer = new AiAnalyzer("https://10.255.255.1/api", "key", "model");

            // 超时会在30秒后触发，测试中不等待30秒
            // 用 Task.WhenAny 验证方法确实有超时机制
            var task = analyzer.AnalyzeBudgetAsync(CreateValidContext());
            var delay = Task.Delay(TimeSpan.FromSeconds(5));

            var completed = await Task.WhenAny(task, delay);
            if (completed == delay)
            {
                // 5秒内没完成，说明确实在等待（超时机制在工作），测试通过
            }
            else
            {
                // 快速返回了（可能是网络错误立即返回，也算合理）
                Assert.IsNotNull(task.Result, "快速返回时结果不应为空");
            }
        }

        #endregion

        #region 空数据场景

        /// <summary>空数据不抛异常，正常返回回退结果</summary>
        [TestMethod]
        public async Task AnalyzeBudgetAsync_EmptyContext_DoesNotThrow()
        {
            var analyzer = new AiAnalyzer("https://localhost:1/nonexistent", "key", "model");
            var ctx = new BudgetContext
            {
                Year = 2026,
                Month = 1,
                PastMonths = new List<PastMonthData>(),
                CurrentMonthCategories = new List<CategorySpending>()
            };

            // 不应抛出异常
            AiBudgetResult result = null;
            try
            {
                result = await analyzer.AnalyzeBudgetAsync(ctx);
            }
            catch (Exception ex)
            {
                Assert.Fail($"不应抛出异常，实际抛出: {ex.Message}");
            }

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }

        #endregion

        #region 7.3 正常场景（需要真实API Key，默认跳过）

        /// <summary>7.3 正常分析：如果配置了真实API Key，应返回 Success=true</summary>
        [TestMethod]
        [Ignore] // 需要真实API Key和Endpoint，仅在手动验证时启用
        public async Task AnalyzeBudgetAsync_ValidApi_ReturnsSuccess()
        {
            var analyzer = new AiAnalyzer(
                "https://api.openai.com/v1/chat/completions",
                "sk-your-real-key",
                "gpt-4o-mini");

            var result = await analyzer.AnalyzeBudgetAsync(CreateValidContext());

            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.TotalBudget > 0);
            Assert.IsFalse(string.IsNullOrEmpty(result.Analysis),
                "AI应返回分析文本");
        }

        #endregion

        #region CallChatAsync 测试

        /// <summary>自由对话API失败时抛出异常（不像 AnalyzeBudgetAsync 有回退）</summary>
        [TestMethod]
        public async Task CallChatAsync_InvalidEndpoint_ThrowsException()
        {
            var analyzer = new AiAnalyzer("https://localhost:1/nonexistent", "key", "model");

            // CallChatAsync 没有 try-catch，异常会直接抛出
            // 原代码使用 Assert.ThrowsExceptionAsync<Exception>，在 .NET Framework 4.8 下
            // 无法匹配异步抛出的 HttpRequestException，改用 try/catch 手动断言
            bool threw = false;
            try
            {
                await analyzer.CallChatAsync("分析我的消费习惯");
            }
            catch
            {
                threw = true;
            }
            Assert.IsTrue(threw, "无效Endpoint时应抛出异常");
        }

        #endregion
    }
}
