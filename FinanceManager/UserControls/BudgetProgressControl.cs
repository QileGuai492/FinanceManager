using FinanceManager.Common;
using FinanceManager.Data.Repositories;
using FinanceManager.Data.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.models;
using FinanceManager.Helpers;
using FinanceManager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinanceManager.UserControls
{
    public partial class BudgetProgressControl : UserControl
    {
        private readonly string _connStr;
        private BudgetViewModel _budgetVM;

        public BudgetProgressControl(string connStr)
        {
            InitializeComponent();
            _connStr = connStr;
        }

        private void BudgetProgressControl_Load(object sender, EventArgs e) { }

        // 初始化界面
        public void Init()
        {
            this.Dock = DockStyle.Fill;
            _budgetVM = new BudgetViewModel(
                new BudgetService(
                    new BudgetRepository(_connStr),
                    new CategoryBudgetRepository(_connStr)));

            // 月份下拉 1-12
            for (int i = 1; i <= 12; i++)
                comboBoxMonth.Items.Add(i);
            comboBoxMonth.SelectedItem = DateTime.Now.Month;
            textBoxYear.Text = DateTime.Now.Year.ToString();

            gridWarn.RowHeadersVisible = false;
            gridWarn.AllowUserToAddRows = false;
            gridWarn.ReadOnly = true;
            UiHelper.StyleDataGridView(gridWarn);

            gridSuggest.RowHeadersVisible = false;
            gridSuggest.AllowUserToAddRows = false;
            gridSuggest.ReadOnly = true;
            UiHelper.StyleDataGridView(gridSuggest);

            // 按钮美化
            UiHelper.StyleButton(buttonSaveBudget, UiHelper.DeepBlue, Color.White);
            UiHelper.BindHover(buttonSaveBudget, UiHelper.DeepBlue, UiHelper.LightBlue);
            UiHelper.StyleButton(buttonLoadBudget, UiHelper.SuccessGreen, Color.White);
            UiHelper.BindHover(buttonLoadBudget, UiHelper.SuccessGreen, Color.FromArgb(0x66, 0xBB, 0x6A));
        }

        // 刷新数据
        public async void RefreshData()
        {
            int year = int.TryParse(textBoxYear.Text, out var y) ? y : DateTime.Now.Year;
            int month = (int)comboBoxMonth.SelectedItem;

            // 获取当月总支出
            var statsService = new StatisticsService(
                new FinanceManager.Data.Repositories.RecordRepository(_connStr),
                new CategoryRepository(_connStr));
            var monthlyStats = await statsService.GetMonthlyStatisticsAsync(
                App.CurrentUserId, year, month);
            if (monthlyStats == null) monthlyStats = new MonthlyStatistics();

            var spent = monthlyStats.TotalExpense;

            // 获取预算
            decimal budgetAmount = 0;
            if (radioButtonDaily.Checked)
            {
                var b = await new BudgetService(
                    new BudgetRepository(_connStr),
                    new CategoryBudgetRepository(_connStr))
                    .GetBudgetByYearMonthAsync(App.CurrentUserId, year, month);
                budgetAmount = b?.Amount ?? 0;
                // 日预算 = 月预算 ÷ 当月天数
                var days = DateTime.DaysInMonth(year, month);
                budgetAmount = budgetAmount / days;
            }
            else if (radioButtonMonthly.Checked)
            {
                var b = await new BudgetService(
                    new BudgetRepository(_connStr),
                    new CategoryBudgetRepository(_connStr))
                    .GetBudgetByYearMonthAsync(App.CurrentUserId, year, month);
                budgetAmount = b?.Amount ?? 0;
            }
            else // 年度
            {
                var budgetService = new BudgetService(
                    new BudgetRepository(_connStr),
                    new CategoryBudgetRepository(_connStr));
                for (int m = 1; m <= 12; m++)
                {
                    var b = await budgetService.GetBudgetByYearMonthAsync(
                        App.CurrentUserId, year, m);
                    if (b != null) budgetAmount += b.Amount;
                }
                // 年度支出 = 全年各月支出总和
                spent = 0;
                for (int m = 1; m <= 12; m++)
                {
                    var ms = await statsService.GetMonthlyStatisticsAsync(
                        App.CurrentUserId, year, m);
                    if (ms != null) spent += ms.TotalExpense;
                }
            }

            // 更新概览
            labelBudget.Text = $"预算金额：¥ {budgetAmount:N2}";
            labelSpent.Text = $"已支出：¥ {spent:N2}";
            labelSpent.ForeColor = spent > budgetAmount && budgetAmount > 0
                ? Color.FromArgb(244, 67, 54) : Color.FromArgb(33, 33, 33);

            var percent = budgetAmount > 0 ? (int)(spent / budgetAmount * 100) : 0;
            var remaining = Math.Max(0, 100 - Math.Min(percent, 100));
            progBudget.Value = remaining;
            progBudget.ForeColor = remaining <= 10
                ? Color.FromArgb(244, 67, 54)
                : Color.FromArgb(33, 150, 243);
            labelPercent.Text = $"{percent}%";

            var remain = budgetAmount - spent;
            labelRemain.Text = remain > 0
                ? $"剩余：¥ {remain:N2}"
                : $"超支：¥ {Math.Abs(remain):N2}";
            labelRemain.ForeColor = remain >= 0
                ? Color.FromArgb(76, 175, 80)
                : Color.FromArgb(244, 67, 54);

            // 日均可用
            if (radioButtonYearly.Checked)
            {
                labelDailyAvg.Text = "";
            }
            else
            {
                var today = DateTime.Today;
                var daysLeft = radioButtonDaily.Checked ? 1
                    : DateTime.DaysInMonth(year, month) - today.Day + 1;
                var daily = daysLeft > 0 ? remain / daysLeft : 0;
                labelDailyAvg.Text = $"日均可用：¥ {Math.Max(0, daily):N2}";
            }

            // 加载预警
            await LoadWarnings(year, month, budgetAmount, spent);

            // 加载AI建议
            await LoadAiSuggestion(year, month);
        }

        // 加载预算预警信息
        private async Task LoadWarnings(int year, int month, decimal totalBudget, decimal totalSpent)
        {
            var catService = new CategoryService(new CategoryRepository(_connStr));
            var statsService = new StatisticsService(
                new FinanceManager.Data.Repositories.RecordRepository(_connStr),
                new CategoryRepository(_connStr));

            var cats = await catService.GetCategoriesAsync(App.CurrentUserId);
            var catStats = await GetCategoryStatsForMonth(year, month);

            var budgetService = new BudgetService(
                new BudgetRepository(_connStr),
                new CategoryBudgetRepository(_connStr));

            gridWarn.Rows.Clear();
            gridWarn.Columns.Clear();
            gridWarn.Columns.Add("colWarnMsg", "");

            bool hasWarning = false;

            foreach (var cs in catStats)
            {
                var catAlloc = await budgetService.GetCategoryBudgetAsync(
                    App.CurrentUserId, cs.CategoryId, year, month);
                var allocAmt = catAlloc?.Amount ?? 0;

                if (allocAmt > 0 && cs.Amount > 0)
                {
                    var catPct = cs.Amount / allocAmt * 100;
                    if (catPct >= 80)
                    {
                        var icon = catPct >= 100 ? "!!" : "⚠";
                        gridWarn.Rows.Add(
                            $"{icon} {cs.CategoryName} 已用 ¥{cs.Amount:N0} / 预算 ¥{allocAmt:N0}（{catPct:F0}%）");
                        hasWarning = true;
                    }
                }
            }

            if (!hasWarning)
                gridWarn.Rows.Add("所有分类均在预算范围内");

            // 总预算预警
            if (totalBudget > 0 && totalSpent > totalBudget)
            {
                gridWarn.Rows.Insert(0,
                    $"!! 总预算已超支 ¥{totalSpent - totalBudget:N0}");
            }
        }

        /// <summary>按月份查询分类统计，封装 GetCategoryStatisticsAsync 的日期范围构造</summary>
        private async Task<IEnumerable<CategoryStatistics>> GetCategoryStatsForMonth(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            return await new StatisticsService(
                new FinanceManager.Data.Repositories.RecordRepository(_connStr),
                new CategoryRepository(_connStr))
                .GetCategoryStatisticsAsync(App.CurrentUserId, 0, startDate, endDate);
        }

        // 加载AI建议
        private async Task LoadAiSuggestion(int year, int month)
        {
            var catService = new CategoryService(new CategoryRepository(_connStr));
            var statsService = new StatisticsService(
                new FinanceManager.Data.Repositories.RecordRepository(_connStr),
                new CategoryRepository(_connStr));

            // 构建AI分析上下文
            var ctx = new BudgetContext
            {
                Year = year,
                Month = month
            };

            // 加载当前预算
            var currentBudget = await new BudgetService(
                new BudgetRepository(_connStr),
                new CategoryBudgetRepository(_connStr))
                .GetBudgetByYearMonthAsync(App.CurrentUserId, year, month);
            ctx.CurrentBudget = currentBudget?.Amount ?? 0;

            // 加载近3月历史数据
            for (int i = 1; i <= 3; i++)
            {
                var m = month - i;
                var y = year;
                if (m <= 0) { m += 12; y--; }

                var ms = await statsService.GetMonthlyStatisticsAsync(App.CurrentUserId, y, m);
                var cs = await GetCategoryStatsForMonth(y, m);

                if (ms != null)
                {
                    ctx.PastMonths.Add(new PastMonthData
                    {
                        Year = y,
                        Month = m,
                        TotalExpense = ms.TotalExpense,
                        Categories = cs.Select(c => new CategorySpending
                        {
                            Name = c.CategoryName,
                            Amount = c.Amount
                        }).ToList()
                    });
                }
            }

            // 本月分类支出
            var curCs = await GetCategoryStatsForMonth(year, month);
            ctx.CurrentMonthCategories = curCs.Select(c => new CategorySpending
            {
                Name = c.CategoryName,
                Amount = c.Amount
            }).ToList();

            // 调用AI分析
            var config = AiConfig.Load();
            var analyzer = new AiAnalyzer(config.Endpoint, config.ApiKey, config.Model);
            var result = await analyzer.AnalyzeBudgetAsync(ctx);

            // 显示结果
            if (result.Success)
            {
                labelSuggestion.Text = result.Analysis;
                if (!string.IsNullOrEmpty(result.Warning))
                    labelSuggestion.Text += $"\n\n⚠ {result.Warning}";

                gridSuggest.Rows.Clear();
                gridSuggest.Columns.Clear();
                gridSuggest.Columns.Add("colSugCat", "分类");
                gridSuggest.Columns.Add("colSugAmount", "建议预算");
                gridSuggest.Columns.Add("colSugReason", "理由");

                foreach (var c in result.Categories)
                {
                    gridSuggest.Rows.Add(c.CategoryName,
                        $"¥ {c.Amount:N0}", c.Reason);
                }
            }
            else
            {
                // AI不可用时回退提示
                labelSuggestion.Text = $"AI服务不可用：{result.Error}\n\n" +
                    $"建议参考金额：¥ {result.TotalBudget:N0}（近3月月均上浮5%）";
                gridSuggest.Rows.Clear();
            }
        }

        // 预算类型变化时，调整月份下拉可用性
        private void BudgetType_CheckedChanged(object sender, EventArgs e)
        {
            // 月度/年度时月份下拉可用，日度时也可用（取对应日期的月度预算 ÷ 天数）
            comboBoxMonth.Enabled = !radioButtonYearly.Checked;
        }


        // 加载按钮
        private void buttonLoadBudget_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        // 点击保存预算按钮，进行预算金额的验证和保存
        private async void buttonSaveBudget_Click(object sender, EventArgs e)
        {

            {
                if (!decimal.TryParse(textBoxBudget.Text, out var amt) || amt <= 0)
                {
                    MessageBox.Show("请输入有效预算金额"); return;
                }

                int year = int.TryParse(textBoxYear.Text, out var y) ? y : DateTime.Now.Year;
                int month = (int)comboBoxMonth.SelectedItem;

                if (radioButtonDaily.Checked)
                {
                    // 日预算 × 当月天数 = 月预算 存入
                    var days = DateTime.DaysInMonth(year, month);
                    amt = amt * days;
                }
                else if (radioButtonYearly.Checked)
                {
                    // 年度预算 ÷ 12 平均分配到每月
                    amt = amt / 12;
                }

                // 保存当月预算
                var budgetService = new BudgetService(
                    new BudgetRepository(_connStr),
                    new CategoryBudgetRepository(_connStr));

                var existing = await budgetService.GetBudgetByYearMonthAsync(
                    App.CurrentUserId, year, month);

                if (existing != null)
                {
                    existing.Amount = amt;
                    await budgetService.UpdateBudgetAsync(existing);
                }
                else
                {
                    await budgetService.AddBudgetAsync(new BudgetEntity
                    {
                        Amount = amt,
                        Month = month,
                        Year = year,
                        UserId = App.CurrentUserId
                    });
                }

                MessageBox.Show("预算保存成功");
                textBoxBudget.Clear();
                buttonLoadBudget_Click(null, null); // 刷新显示
            }
        }
    }
}
