using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace FinanceManager.Data.Services
{
    /// <summary>
    /// AI 分析器 —— 调用外部 AI API（如 DeepSeek/OpenAI）进行消费分析、预算建议。
    /// API 调用失败时自动回退到本地计算（近3月平均支出 × 1.05）。
    /// </summary>
    public class AiAnalyzer
    {
        /// <summary>AI API 端点地址</summary>
        private readonly string _endpoint;
        /// <summary>API 密钥</summary>
        private readonly string _apiKey;
        /// <summary>模型名称（如 deepseek-chat、gpt-4o-mini）</summary>
        private readonly string _model;

        /// <summary>构造函数：注入 AI API 的连接配置</summary>
        public AiAnalyzer(string endpoint, string apiKey, string model)
        {
            _endpoint = endpoint;
            _apiKey = apiKey;
            _model = model;
        }

        public async Task<AiBudgetResult> AnalyzeBudgetAsync(BudgetContext ctx)
        {
            try
            {
                var prompt = BuildPrompt(ctx);
                var response = await CallApiAsync(prompt);
                return ParseResponse(response, ctx);
            }
            catch (Exception ex)
            {
                return new AiBudgetResult
                {
                    Success = false,
                    Error = ex.Message,
                    // 失败时回退到本地计算
                    TotalBudget = CalculateFallback(ctx),
                    Analysis = "（AI服务暂时不可用，以下为本地估算结果）"
                };
            }
        }

        // ========== 构建 Prompt ==========
        private string BuildPrompt(BudgetContext ctx)
        {
            var sb = new StringBuilder();
            sb.AppendLine("你是一位专业的个人财务顾问。请根据以下用户的消费数据，给出预算建议。");
            sb.AppendLine();
            sb.AppendLine($"用户当前月份：{ctx.Year}年{ctx.Month}月");
            sb.AppendLine($"当前设置的总预算：¥{ctx.CurrentBudget:N0}");
            sb.AppendLine();
            sb.AppendLine("=== 近3个月支出 ===");
            foreach (var m in ctx.PastMonths)
            {
                sb.AppendLine($"{m.Year}年{m.Month}月：总支出 ¥{m.TotalExpense:N0}");
                foreach (var c in m.Categories)
                    sb.AppendLine($"  - {c.Name}：¥{c.Amount:N0}");
            }
            sb.AppendLine();
            sb.AppendLine("=== 本月至今支出 ===");
            foreach (var c in ctx.CurrentMonthCategories)
                sb.AppendLine($"  - {c.Name}：¥{c.Amount:N0}");

            sb.AppendLine();
            sb.AppendLine("请按以下JSON格式返回（不要返回其他内容）：");
            sb.AppendLine("{");
            sb.AppendLine("  \"totalBudget\": 数字,");
            sb.AppendLine("  \"categories\": [");
            sb.AppendLine("    { \"name\": \"分类名\", \"amount\": 数字, \"reason\": \"理由，10字以内\" }");
            sb.AppendLine("  ],");
            sb.AppendLine("  \"analysis\": \"总体分析和建议，30字以内\",");
            sb.AppendLine("  \"warning\": \"如果有超支风险写预警，没有则为空字符串\"");
            sb.AppendLine("}");

            return sb.ToString();
        }

        // ========== 调用 API ==========
        private async Task<string> CallApiAsync(string prompt)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                var body = new
                {
                    model = _model,
                    messages = new[]
                    {
                        new { role = "system", content = "你是专业的财务顾问，只返回JSON。" },
                        new { role = "user", content = prompt }
                    },
                    temperature = 0.3,
                    max_tokens = 2000
                };

                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_endpoint, content);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<dynamic>(result);
                return (string)obj.choices[0].message.content;
            }
        }

        // ========== 解析响应 ==========
        private AiBudgetResult ParseResponse(string json, BudgetContext ctx)
        {
            // 提取JSON（模型可能在JSON外加了markdown代码块）
            var start = json.IndexOf('{');
            var end = json.LastIndexOf('}');
            if (start >= 0 && end > start)
                json = json.Substring(start, end - start + 1);

            var obj = JsonConvert.DeserializeObject<dynamic>(json);
            var result = new AiBudgetResult { Success = true };

            result.TotalBudget = (decimal)obj.totalBudget;
            result.Analysis = (string)obj.analysis ?? "";
            result.Warning = (string)obj.warning ?? "";

            foreach (var c in obj.categories)
            {
                result.Categories.Add(new AiCategoryBudget
                {
                    CategoryName = (string)c.name,
                    Amount = (decimal)c.amount,
                    Reason = (string)c.reason ?? ""
                });
            }

            return result;
        }

        // ========== 回退计算 ==========
        private decimal CalculateFallback(BudgetContext ctx)
        {
            decimal total = 0; int count = 0;
            foreach (var m in ctx.PastMonths)
            {
                total += m.TotalExpense; count++;
            }
            return count > 0 ? total / count * 1.05m : 0;
        }

        // ========== 自由对话 ==========
        public async Task<string> CallChatAsync(string userMessage)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                var body = new
                {
                    model = _model,
                    messages = new[]
                    {
                        new { role = "system", content = "你是专业的财务分析师，回复简洁，分析不超过30字。" },
                        new { role = "user", content = userMessage }
                    },
                    temperature = 0.5,
                    max_tokens = 800
                };

                var json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(_endpoint, content);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                var obj = JsonConvert.DeserializeObject<dynamic>(result);
                return (string)obj.choices[0].message.content;
            }
        }
    }

    // ========== 上下文模型 ==========
    public class BudgetContext
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal CurrentBudget { get; set; }
        public List<PastMonthData> PastMonths { get; set; } = new List<PastMonthData>();
        public List<CategorySpending> CurrentMonthCategories { get; set; } = new List<CategorySpending>();
    }

    public class PastMonthData
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal TotalExpense { get; set; }
        public List<CategorySpending> Categories { get; set; } = new List<CategorySpending>();
    }

    public class CategorySpending
    {
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
