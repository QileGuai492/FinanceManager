using FinanceManager.Common;
using FinanceManager.Common.Helpers;
using FinanceManager.Data.Repositories;
using FinanceManager.Data.Services;
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
using System.Windows.Forms.DataVisualization.Charting;

namespace FinanceManager.UserControls
{

    public partial class StatisticsControl : UserControl
    {
        private readonly string _connStr;

        /// <summary>统计页的 ViewModel，负责加载和计算月度/分类/趋势统计数据</summary>
        private StatisticsViewModel _statsVM;

        public StatisticsControl(string connStr)
        {
            InitializeComponent();
            _connStr = connStr;
        }

        /// <summary>设计器生成的 Load 事件，保留空壳</summary>
        private void StatisticsControl_Load(object sender, EventArgs e) { }

        /// <summary>初始化统计控件：设置下拉选项、图表初始化、美化</summary>
        public void Init()
        {
            _statsVM = new StatisticsViewModel(
                new StatisticsService(
                    new FinanceManager.Data.Repositories.RecordRepository(_connStr),
                    new CategoryRepository(_connStr)
                )
            );

            this.Dock = DockStyle.Fill;
            UiHelper.MakeGradient(this, UiHelper.SoftBlue, Color.White);
            // ===== 饼图区 =====
            comboBoxPieScope.Items.AddRange(new[] { "日", "月", "季度", "年", "多年" });
            comboBoxPieScope.SelectedIndex = 1;
            for (int i = 1; i <= 12; i++) comboBoxPieMonth.Items.Add(i);
            for (int i = 1; i <= 31; i++) comboBoxPieDay.Items.Add(i);
            comboBoxSeason.Items.AddRange(new[] { "第一季度", "第二季度", "第三季度", "第四季度" });
            var q = (DateTime.Now.Month - 1) / 3;
            comboBoxSeason.SelectedIndex = q;
            textBoxPieYear.Text = DateTime.Now.Year.ToString();
            comboBoxPieMonth.SelectedItem = DateTime.Now.Month;
            comboBoxPieDay.SelectedItem = DateTime.Now.Day;
            textBoxPieFromYear.Text = (DateTime.Now.Year - 2).ToString();
            TogglePieControls();

            // ===== 条形图区 =====
            comboBoxBarScope.Items.AddRange(new[] { "日际", "月际", "季际", "年际" });
            comboBoxBarScope.SelectedIndex = 1;
            dtpBarFrom.Value = new DateTime(DateTime.Today.Year, 1, 1);
            dtpBarTo.Value = DateTime.Today;

            // ===== 图表初始化 =====
            SetupPieChart(chartPieIncome);
            SetupPieChart(chartPieExpense);
            SetupBarChart(chartBarIncome);
            SetupBarChart(chartBarExpense);

            // 图表美化
            foreach (var chart in new[] { chartPieIncome, chartPieExpense, chartBarIncome, chartBarExpense })
            {
                chart.BackColor = UiHelper.CardWhite;
                chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.BrightPastel;
            }

            // 查询按钮美化
            UiHelper.StyleButton(buttonPieCheck, UiHelper.DeepBlue, Color.White);
            UiHelper.BindHover(buttonPieCheck, UiHelper.DeepBlue, UiHelper.LightBlue);
            UiHelper.StyleButton(buttonBarCheck, UiHelper.DeepBlue, Color.White);
            UiHelper.BindHover(buttonBarCheck, UiHelper.DeepBlue, UiHelper.LightBlue);

            // ===== 卡片色条 =====
            AddColorBar(panelIncome, UiHelper.SuccessGreen);
            AddColorBar(panelExpense, UiHelper.DangerRed);
            AddColorBar(panelRemain, UiHelper.DeepBlue);
            panelIncome.BackColor = UiHelper.BgLight;
            panelExpense.BackColor = UiHelper.BgLight;
            panelRemain.BackColor = UiHelper.BgLight;

            // ===== 币种筛选（放最后，避免初始化时触发事件导致NRE）=====
            comboBoxMoney.Items.AddRange(new[] { "全部", "CNY", "USD", "EUR", "JPY", "GBP", "HKD" });
            comboBoxMoney.SelectedItem = App.CurrentUserCurrency ?? "CNY";
        }

        /// <summary>根据饼图范围选择，切换日/月/季度控件的显示和启用状态</summary>
        private void TogglePieControls()
        {
            var scope = comboBoxPieScope.SelectedItem?.ToString() ?? "";
            bool isDay = (scope == "日");
            bool isMonth = (scope == "日" || scope == "月");
            bool isQuarter = (scope == "季度");
            bool isMulti = (scope == "多年");

            comboBoxPieDay.Enabled = isDay;
            comboBoxPieMonth.Enabled = isMonth;
            comboBoxSeason.Enabled = isQuarter;
            textBoxPieYear.Enabled = !isMulti;
            textBoxPieFromYear.Enabled = isMulti;
            textBoxPieToYear.Enabled = isMulti;
        }




        // ===== 图表初始化辅助方法 =====

        /// <summary>初始化饼图：清空旧数据，创建 Series 和图例</summary>
        private void SetupPieChart(Chart chart)
        {
            chart.Series.Clear();
            chart.Legends.Clear();
            var s = new Series("s");
            s.ChartType = SeriesChartType.Pie;
            chart.Series.Add(s);
            var legend = new Legend("L");
            legend.Docking = Docking.Bottom;
            chart.Legends.Add(legend);
        }

        /// <summary>初始化条形图：清空旧数据，创建图例和 X 轴</summary>
        private void SetupBarChart(Chart chart)
        {
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.Legends.Add(new Legend("L") { Docking = Docking.Bottom });
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add("area");
            chart.ChartAreas["area"].AxisX.Interval = 1;
            var ax = chart.ChartAreas["area"].AxisX;
            ax.ScrollBar.Enabled = true;
            ax.ScrollBar.IsPositionedInside = false;
            ax.ScaleView.Size = 8;
                        ax.LabelStyle.Interval = 1;
        }

        /// <summary>为统计卡片左侧添加颜色标识条</summary>
        private void AddColorBar(Panel pan, Color color)
        {
            var bar = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(5, pan.Height),
                BackColor = color,
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom
            };
            pan.Controls.Add(bar);
        }

        /// <summary>饼图范围切换：根据选择（日/月/季/年/多年）调整控件状态</summary>
        private void comboBoxPieScope_SelectedIndexChanged(object sender, EventArgs e)
        {
            TogglePieControls();
        }

        // ====== 饼状图功能实现 ======

        /// <summary>"查询"按钮：加载饼图数据</summary>
        private void buttonPieCheck_Click(object sender, EventArgs e)
        {
            ShowPieChart();
        }

        /// <summary>加载饼图：查询分类统计 → 更新摘要卡片 → 绘制饼图 → 触发 AI 分析</summary>
        public async void ShowPieChart()
        {
            try
            {
            int year = int.TryParse(textBoxPieYear.Text, out var y) ? y : DateTime.Now.Year;
            int month = (int)(comboBoxPieMonth.SelectedItem ?? DateTime.Now.Month);
            int day = (int)(comboBoxPieDay.SelectedItem ?? DateTime.Now.Day);
            var scope = comboBoxPieScope.SelectedItem.ToString();
            var currency = GetStatCurrency();

            DateTime from, to;
            GetPieDateRange(scope, year, month, day, out from, out to);

            var statsService = new StatisticsService(
                new FinanceManager.Data.Repositories.RecordRepository(_connStr),
                new CategoryRepository(_connStr));

            var incomeList = (await statsService.GetCategoryStatisticsAsync(
                App.CurrentUserId, 1, from, to, currency)).ToList();
            var totalIncome = incomeList.Sum(c => c.Amount);

            var expenseList = (await statsService.GetCategoryStatisticsAsync(
                App.CurrentUserId, 0, from, to, currency)).ToList();
            var totalExpense = expenseList.Sum(c => c.Amount);

            var statSymbol = CurrencyHelper.GetSymbol(currency ?? App.CurrentUserCurrency);
            labelSIncome.Text = $"{statSymbol}{totalIncome:N2}";
            labelSExpense.Text = $"{statSymbol}{totalExpense:N2}";
            labelSRemain.Text = $"{statSymbol}{totalIncome - totalExpense:N2}";

            var title = "单年";
            if (scope == "日") title = "单日";
            else if (scope == "月") title = "单月";
            else if (scope == "季") title = "单季";
            else if (scope == "多年") title = "多年";

            UpdatePieChart(chartPieIncome, $"{title}收入占比", incomeList, totalIncome, statSymbol);
            UpdatePieChart(chartPieExpense, $"{title}支出占比", expenseList, totalExpense, statSymbol);

            // 触发AI分析建议，传入收支分类明细
            LoadStatsAi(from, to, totalIncome, totalExpense, incomeList, expenseList);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载饼图数据失败：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>根据范围类型（日/月/季/年/多年）计算查询的起止日期</summary>
        private void GetPieDateRange(string scope, int year, int month, int day,
    out DateTime from, out DateTime to)
        {
            switch (scope)
            {
                case "日":
                    from = new DateTime(year, month, day);
                    to = from.AddDays(1); break;
                case "月":
                    from = new DateTime(year, month, 1);
                    to = from.AddMonths(1); break;
                case "季度":
                    var qIdx = comboBoxSeason.SelectedIndex; // 0=Q1, 1=Q2, 2=Q3, 3=Q4
                    from = new DateTime(year, qIdx * 3 + 1, 1);
                    to = from.AddMonths(3); break;
                case "年":
                    from = new DateTime(year, 1, 1);
                    to = new DateTime(year + 1, 1, 1); break;
                default: // 多年
                    var fy = int.TryParse(textBoxPieFromYear.Text, out var f) ? f : year - 2;
                    var ty = int.TryParse(textBoxPieToYear.Text, out var t) ? t : year;
                    from = new DateTime(fy, 1, 1);
                    to = new DateTime(ty + 1, 1, 1); break;
            }
        }

        // 加载数据后更新饼图显示
        /// <summary>更新饼图：Top5 分类 + 其他，设置图例和标签</summary>
        private void UpdatePieChart(Chart chart, string title,
    List<CategoryStatistics> stats, decimal total, string currencySymbol = "¥")
        {
            chart.Titles.Clear();
            chart.Titles.Add(title);
            chart.Series.Clear();
            chart.Legends.Clear();

            var s = new Series("s");
            s.ChartType = SeriesChartType.Pie;
            chart.Series.Add(s);
            chart.Legends.Add("L");
            chart.Legends["L"].Docking = Docking.Bottom;

            if (total <= 0) return;

            var items = stats.Where(c => c.Amount > 0)
                .OrderByDescending(c => c.Amount).ToList();
            var top5 = items.Take(5).ToList();
            var top5Sum = top5.Sum(t => t.Amount);
            if (items.Count > 5) top5.Add(new CategoryStatistics
            {
                CategoryName = "其他",
                Amount = total - top5Sum,
                CategoryColor = "#BDBDBD"
            });

            foreach (var item in top5)
            {
                var pt = s.Points.Add((double)item.Amount);
                pt.LegendText = $"{item.CategoryName}  {currencySymbol}{item.Amount:N0}";
                pt.Label = $"{(item.Amount / total * 100):F1}%";
                if (!string.IsNullOrEmpty(item.CategoryColor))
                    pt.Color = ColorTranslator.FromHtml(item.CategoryColor);
            }
        }

        /// <summary>统计页 AI 分析：构建收支明细 Prompt → 调用 CallChatAsync → 显示结果</summary>
        private async void LoadStatsAi(DateTime from, DateTime to, decimal totalIncome, decimal totalExpense,
            List<CategoryStatistics> incomeList, List<CategoryStatistics> expenseList)
        {
            var config = AiConfig.Load();
            if (string.IsNullOrEmpty(config.ApiKey))
            {
                labelStatsAi.Text = "未配置AI API，请在设置中配置。";
                return;
            }

            try
            {
                var sb = new StringBuilder();
                sb.AppendLine($"时间范围：{from:yyyy-MM-dd} 至 {to:yyyy-MM-dd}");
                sb.AppendLine($"总收入：¥{totalIncome:N0}  总支出：¥{totalExpense:N0}");
                sb.AppendLine(totalIncome - totalExpense >= 0 ? "盈余" : "赤字");
                sb.AppendLine();
                sb.AppendLine("=== 收入明细 ===");
                foreach (var c in incomeList)
                    sb.AppendLine($"  {c.CategoryName}：¥{c.Amount:N0}（{c.Percentage:F1}%）");
                sb.AppendLine();
                sb.AppendLine("=== 支出明细 ===");
                foreach (var c in expenseList)
                    sb.AppendLine($"  {c.CategoryName}：¥{c.Amount:N0}（{c.Percentage:F1}%）");
                sb.AppendLine();
                sb.AppendLine("请根据以上收支结构和占比，简要分析（30-40字以内）：消费结构是否合理、有无异常支出、优化建议。");

                var analyzer = new AiAnalyzer(config.Endpoint, config.ApiKey, config.Model);
                var response = await analyzer.CallChatAsync(sb.ToString());
                labelStatsAi.Text = response ?? "AI无响应";
            }
            catch (Exception ex)
            {
                labelStatsAi.Text = $"AI分析失败：{ex.Message}";
            }
        }

        // ====== 条形图功能实现 ======

        /// <summary>"查询"按钮：加载条形图数据</summary>
        private void buttonBarCheck_Click(object sender, EventArgs e)
        {
            ShowBarChart();
        }

        /// <summary>加载条形图：根据范围类型自动计算起止日期</summary>
        public async void ShowBarChart()
        {
            try
            {
            var scope = comboBoxBarScope.SelectedItem.ToString();
            var currency = GetStatCurrency();
            var now = DateTime.Today;
            DateTime from, to;

            switch (scope)
            {
                case "日际":
                    from = new DateTime(now.Year, now.Month, 1);
                    to = from.AddMonths(1); break;
                case "月际":
                    from = new DateTime(now.Year, 1, 1);
                    to = new DateTime(now.Year + 1, 1, 1); break;
                case "季际":
                    from = new DateTime(now.Year, 1, 1);
                    to = new DateTime(now.Year + 1, 1, 1); break;
                default: // 年际
                    from = new DateTime(now.Year - 4, 1, 1);
                    to = new DateTime(now.Year + 1, 1, 1); break;
            }

            await UpdateBarChart(chartBarIncome, scope, from, to, 1, currency);
            await UpdateBarChart(chartBarExpense, scope, from, to, 0, currency);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载条形图数据失败：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>加载条形图数据：按范围分段统计收支金额并绘制柱状图</summary>
        private bool _isUpdatingBar;
        private async Task UpdateBarChart(Chart chart, string scope,
    DateTime from, DateTime to, int type, string currency = null)
        {
            if (_isUpdatingBar) return;
            _isUpdatingBar = true;
            try
            {
            chart.Titles.Clear();
            chart.Series.Clear();
            chart.Legends.Clear();

            var title = "年际";
            if (scope == "日际") title = $"{from:yyyy年M月}日际";
            else if (scope == "月际") title = $"{from:yyyy年}月际";
            else if (scope == "季际") title = $"{from:yyyy年}季际";
            chart.Titles.Add(type == 1 ? $"{title}收入构成" : $"{title}支出构成");
            chart.Legends.Add("L");
            chart.Legends["L"].Docking = Docking.Bottom;
            chart.ChartAreas.Clear();
            chart.ChartAreas.Add("area");

            // 根据粒度生成时间段列表
            var periods = new List<(string Label, DateTime From, DateTime To)>();
            var cur = from;
            while (cur < to)
            {
                DateTime next;
                string label;
                switch (scope)
                {
                    case "日际":
                        next = cur.AddDays(1);
                        label = cur.Day.ToString(); break;
                    case "月际":
                        next = new DateTime(cur.Year, cur.Month, 1).AddMonths(1);
                        label = cur.Month.ToString(); break;
                    case "季际":
                        var q = (cur.Month - 1) / 3 + 1;
                        next = new DateTime(cur.Year, (q - 1) * 3 + 1, 1).AddMonths(3);
                        label = $"第{q}季度"; break;
                    default: // 年际
                        next = new DateTime(cur.Year + 1, 1, 1);
                        label = cur.ToString("yyyy年"); break;
                }
                if (next > to) next = to;
                periods.Add((label, cur, next));
                cur = next;
            }

            // 以下 Series 构建和数据填充逻辑不变
            var statsService = new StatisticsService(
                new FinanceManager.Data.Repositories.RecordRepository(_connStr),
                new CategoryRepository(_connStr));
            var catService = new CategoryService(new CategoryRepository(_connStr));
            var allCats = (await catService.GetCategoriesByTypeAsync(App.CurrentUserId, type)).ToList();

            var catTotals = new Dictionary<int, (string Name, string Color, decimal Total)>();
            foreach (var p in periods)
            {
                var catStats = await statsService.GetCategoryStatisticsAsync(
                    App.CurrentUserId, type, p.From, p.To, currency);
                foreach (var cs in catStats.Where(cs => cs.Amount > 0))
                {
                    if (!catTotals.ContainsKey(cs.CategoryId))
                        catTotals[cs.CategoryId] = (cs.CategoryName, cs.CategoryColor, 0);
                    var entry = catTotals[cs.CategoryId];
                    catTotals[cs.CategoryId] = (entry.Name, entry.Color, entry.Total + cs.Amount);
                }
            }

            var top5Ids = catTotals.OrderByDescending(kv => kv.Value.Total)
                .Take(5).Select(kv => kv.Key).ToHashSet();

            var seriesMap = new Dictionary<string, Series>();
            foreach (var cat in allCats.Where(c => top5Ids.Contains(c.Id)))
            {
                var s = new Series(cat.Name);
                s.ChartType = SeriesChartType.StackedColumn;
                s.Color = ColorTranslator.FromHtml(cat.Color);
                chart.Series.Add(s);
                seriesMap[cat.Name] = s;
            }
            var otherSeries = new Series("其他");
            otherSeries.ChartType = SeriesChartType.StackedColumn;
            otherSeries.Color = Color.FromArgb(189, 189, 189);
            chart.Series.Add(otherSeries);

            foreach (var p in periods)
            {
                var catStats = (await statsService.GetCategoryStatisticsAsync(
                    App.CurrentUserId, type, p.From, p.To, currency)).ToList();
                decimal otherAmt = 0;
                // 每个周期先给所有系列加 0 点，确保 X 轴对齐
                foreach (var s in seriesMap.Values)
                    s.Points.Add(0).AxisLabel = p.Label;
                otherSeries.Points.Add(0).AxisLabel = p.Label;

                foreach (var cs in catStats.Where(cs => cs.Amount > 0))
                {
                    if (top5Ids.Contains(cs.CategoryId) && seriesMap.ContainsKey(cs.CategoryName))
                    {
                        int idx = seriesMap[cs.CategoryName].Points.Count - 1;
                        seriesMap[cs.CategoryName].Points[idx].SetValueY(cs.Amount);
                    }
                    else
                        otherAmt += cs.Amount;
                }
                int lastIdx = otherSeries.Points.Count - 1;
                otherSeries.Points[lastIdx].SetValueY(otherAmt);
            }

            chart.ChartAreas["area"].AxisX.Interval = 1;

            var ax = chart.ChartAreas["area"].AxisX;
            ax.ScrollBar.Enabled = true;
            ax.ScaleView.Size = 8;
                        }
            finally { _isUpdatingBar = false; }
        }

        private void comboBoxSeason_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPieChart(); // 切换季度时自动刷新饼图
        }

        /// <summary>统计页币种筛选：选中后自动刷新饼图和条形图</summary>
        private void comboBoxMoney_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowPieChart();
            ShowBarChart();
        }

        /// <summary>获取当前选中的币种：返回 null（全部）或货币代码</summary>
        private string GetStatCurrency()
        {
            var sel = comboBoxMoney.SelectedItem?.ToString();
            return (sel == "全部" || sel == null) ? null : sel;
        }
    }
}
