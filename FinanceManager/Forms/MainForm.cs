using FinanceManager.Common;
using FinanceManager.Common.Helpers;
using FinanceManager.Data.Database;
using FinanceManager.Data.Repositories;
using FinanceManager.Data.Services;
using FinanceManager.Helpers;
using FinanceManager.UserControls;
using FinanceManager.ViewModels;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FinanceManager.Forms
{
    /// <summary>
    /// 主窗体 —— 左侧导航栏 + 右侧内容区，包含仪表盘、记账、统计、预算四个功能面板。
    /// 通过 ShowPanel 切换可视面板，各面板通过独立的 ViewModel 管理数据。
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>原始 LoginForm 引用，退出时显示回去</summary>
        public static Form OwnerLoginForm { get; set; }

        // ===== UI 状态字段 =====

        /// <summary>当前显示的内容面板，用于切换页面时隐藏上一个</summary>
        private Control _currentPanel;
        /// <summary>当前高亮的导航按钮</summary>
        private Button _activeNavBtn;

        // ===== 数据层字段 =====

        /// <summary>数据库连接字符串，从 DatabaseManager 单例获取</summary>
        private readonly string _connStr = DatabaseManager.Instance.ConnectionString;
        /// <summary>统计页的 ViewModel，负责加载和计算月度/分类/趋势统计数据</summary>
        private StatisticsViewModel _statsVM;

        // ===== 创建各页 UserControl =====
        private BudgetProgressControl budgetControl;
        private StatisticsControl statisticsControl;
        private RecordListControl recordControl;

        /// <summary>构造函数：仅初始化设计器生成的控件，实际逻辑在 MainForm_Load</summary>
        public MainForm()
        {
            InitializeComponent();
            // 点 X 关闭主窗口时同时关闭登录窗口，退出应用；代码调用 Close 不影响
            this.FormClosing += (s, e) =>
            {
                if (e.CloseReason == CloseReason.UserClosing)
                    OwnerLoginForm?.Close();
            };
        }

        /// <summary>
        /// 窗体加载入口：设置窗口属性 → 美化导航栏和仪表盘卡片 →
        /// 初始化各 ViewModel → 添加功能 Panel → 加载默认内容
        /// </summary>
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Text = "AI个人财务管理系统";
            this.Size = new Size(1350, 800);
            this.StartPosition = FormStartPosition.Manual;
            var area = Screen.PrimaryScreen.WorkingArea;
            this.Location = new Point((area.Width - this.Width) / 2, (area.Height - this.Height) / 2 - 10);
            this.BackColor = UiHelper.BgLight;
            this.MinimumSize = new Size(960, 600);
            logo.Image = Properties.Resources.logo;
            logo.SizeMode = PictureBoxSizeMode.Zoom;

            StyleNavBar();
            StyleDashboardCards();

            // ===== 初始化统计页的 ViewModel(仪表盘也要用） =====
            _statsVM = new StatisticsViewModel(
                new StatisticsService(
                    new FinanceManager.Data.Repositories.RecordRepository(_connStr),
                    new CategoryRepository(_connStr)
                )
            );

            // 将所有功能 Panel 添加到主容器中（先添加的在下层，后添加的在上层）
            panContainer.Controls.Add(panelDashBoard);

            recordControl = new RecordListControl(_connStr) { Visible = false };
            statisticsControl = new StatisticsControl(_connStr) { Visible = false };
            budgetControl = new BudgetProgressControl(_connStr) { Visible = false };

            recordControl.RecordChanged += RefreshDashboard;
            recordControl.Init();
            statisticsControl.Init();
            budgetControl.Init();

            // panel容器加入记账页


            panContainer.Controls.Add(recordControl);

            // panel容器加入统计页
            
            
            panContainer.Controls.Add(statisticsControl);

            // panel容器加入预算页
            
            
            panContainer.Controls.Add(budgetControl);

            // ===== 初始化各个 Panel 的内容 =====
            ShowPanel(panelDashBoard);
            SetActiveNav(btnDashboard);
            UiHelper.MakeGradient(panelDashBoard, UiHelper.SoftBlue, Color.White);
    RefreshDashboard();
        }

        /// <summary>切换内容区：隐藏上一个 Panel，显示目标 Panel</summary>
        private void ShowPanel(Control panel)
        {
            if (_currentPanel != null)
                _currentPanel.Visible = false;
            panel.Visible = true;
            _currentPanel = panel;
        }

        /// <summary>刷新仪表盘：加载当月收支统计并更新三张卡片 + 两张饼图</summary>
        private async void RefreshDashboard()
        {
            labelWelcome.Text = $"欢迎回来，{App.CurrentUsername}";
            labelWelcome.Font = new Font("微软雅黑", 18f, FontStyle.Bold);
            labelDate.Text = $"今天是 {DateTime.Now:yyyy年MM月dd日}";
            labelDate.Font = new Font("微软雅黑", 12f);
            labelDate.TextAlign = ContentAlignment.MiddleRight;
            panelDashBoard.Dock = DockStyle.Fill;

            try
            {
                var now = DateTime.Now;
                _statsVM.SelectedYear = now.Year;
                _statsVM.SelectedMonth = now.Month;
                await _statsVM.LoadMonthlyAsync();

                var stats = _statsVM.MonthlyStats;
                if (stats == null) return;

                var dashSymbol = CurrencyHelper.GetSymbol(App.CurrentUserCurrency);
                _lblIncomeValue.Text = $"{dashSymbol}{stats.TotalIncome:N2}";
                _lblIncomeValue.Font = new Font("微软雅黑", 18f, FontStyle.Bold);
                _lblExpenseValue.Text = $"{dashSymbol}{stats.TotalExpense:N2}";
                _lblExpenseValue.Font = new Font("微软雅黑", 18f, FontStyle.Bold);
                _lblBalanceValue.Text = $"{dashSymbol}{stats.Balance:N2}";
                _lblBalanceValue.Font = new Font("微软雅黑", 18f, FontStyle.Bold);
                _lblBalanceValue.ForeColor = stats.Balance < 0
                    ? UiHelper.DangerRed
                    : UiHelper.BalanceBlue;

                // 加载当月分类收支明细并更新饼图
                var from = new DateTime(now.Year, now.Month, 1);
                var to = from.AddMonths(1);
                var statsService = new StatisticsService(
                    new FinanceManager.Data.Repositories.RecordRepository(_connStr),
                    new CategoryRepository(_connStr));

                var incomeList = (await statsService.GetCategoryStatisticsAsync(
                    App.CurrentUserId, 1, from, to, App.CurrentUserCurrency)).ToList();
                var expenseList = (await statsService.GetCategoryStatisticsAsync(
                    App.CurrentUserId, 0, from, to, App.CurrentUserCurrency)).ToList();

                UpdateDashPieChart(chartIncome, "收入", incomeList, stats.TotalIncome, dashSymbol);
                UpdateDashPieChart(chartExpense, "支出", expenseList, stats.TotalExpense, dashSymbol);
            }
            catch { }
        }

        /// <summary>更新仪表盘饼图</summary>
        private void UpdateDashPieChart(System.Windows.Forms.DataVisualization.Charting.Chart chart,
            string title, System.Collections.Generic.IEnumerable<FinanceManager.Domain.models.CategoryStatistics> stats, decimal total, string currencySymbol = "¥")
        {
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.Titles.Clear();
            chart.Titles.Add(title);
            var s = new System.Windows.Forms.DataVisualization.Charting.Series("s")
            {
                ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie
            };
            chart.Series.Add(s);
            chart.Legends.Add("L");
            chart.Legends["L"].Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;

            if (total <= 0)
            {
                chart.Titles.Add("暂无数据");
                return;
            }
            var itemList = stats.Where(c => c.Amount > 0).OrderByDescending(c => c.Amount).Take(5).ToList();
            var top5Sum = itemList.Sum(i => i.Amount);
            if (stats.Count(c => c.Amount > 0) > 5)
                itemList.Add(new FinanceManager.Domain.models.CategoryStatistics
                { CategoryName = "其他", Amount = total - top5Sum, CategoryColor = "#BDBDBD" });

            foreach (var item in itemList)
            {
                var pt = s.Points.Add((double)item.Amount);
                pt.LegendText = $"{item.CategoryName} {currencySymbol}{item.Amount:N0}";
                if (!string.IsNullOrEmpty(item.CategoryColor))
                    pt.Color = ColorTranslator.FromHtml(item.CategoryColor);
            }
        }

        /// <summary>美化左侧导航栏：深蓝背景 + 白色文字 + hover 浅蓝效果 + 底部退出按钮</summary>
        private void StyleNavBar()
        {
            panNav.BackColor = UiHelper.DeepBlue;
            panNav.Width = 150;
            logo.Height = 135;

            var navBdrNormal = UiHelper.NavBorderNormal;
            var navBdrActive = UiHelper.NavBorderActive;
            var navBtns = new[] { btnDashboard, btnRecord, btnStatistics, btnBudget, btnTemplate, btnData, btnSettings };
            for (int i = 0; i < navBtns.Length; i++)
            {
                var btn = navBtns[i];
                panNav.Controls.Remove(btn);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.FlatAppearance.BorderColor = navBdrNormal;
                btn.BackColor = UiHelper.DeepBlue;
                btn.ForeColor = Color.White;
                btn.Font = new Font("微软雅黑", 10f);
                btn.TextAlign = ContentAlignment.MiddleCenter;
                btn.Height = 48;
                btn.Dock = DockStyle.Top;
                btn.Cursor = Cursors.Hand;
                btn.MouseEnter += (s, e) => { if (btn.Tag?.ToString() != "active") btn.BackColor = UiHelper.LightBlue; };
                btn.MouseLeave += (s, e) => { if (btn.Tag?.ToString() != "active") btn.BackColor = UiHelper.DeepBlue; };
                btn.MouseDown += (s, e) => {
                    btn.BackColor = UiHelper.ActiveBlue;
                    btn.FlatAppearance.BorderColor = navBdrActive;
                    btn.FlatAppearance.BorderSize = 2;
                };
                btn.MouseUp += (s, e) => {
                    if (btn.Tag?.ToString() != "active")
                    {
                        btn.BackColor = UiHelper.DeepBlue;
                        btn.FlatAppearance.BorderColor = navBdrNormal;
                        btn.FlatAppearance.BorderSize = 1;
                    }
                };
            }
            // 倒序加回：Dashboard 最后加 → Z序最高 → DockTop 下排最上面
            for (int i = navBtns.Length - 1; i >= 0; i--)
                panNav.Controls.Add(navBtns[i]);

            // 确保 logo 始终在最上面（DockTop 下 index 越大越靠上）
            panNav.Controls.SetChildIndex(logo, panNav.Controls.Count - 1);

            panNav.Controls.Remove(btnLogout);
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.BackColor = UiHelper.DeepBlue;
            btnLogout.ForeColor = UiHelper.LogoutPink;
            btnLogout.Font = new Font("微软雅黑", 9f);
            btnLogout.Dock = DockStyle.Bottom;
            btnLogout.Height = 48;
            btnLogout.Cursor = Cursors.Hand;
            panNav.Controls.Add(btnLogout);
        }

        /// <summary>美化仪表盘三张卡片：白底圆角 + 收入绿/支出红/结余蓝 + 左侧色条 + 金额大字体</summary>
        private void StyleDashboardCards()
        {
            foreach (var p in new[] { panel1, panel2, panel3 })
            {
                p.BackColor = Color.Transparent;
            }
            AddCardColorBar(panel1, UiHelper.SuccessGreen);
            AddCardColorBar(panel2, UiHelper.DangerRed);
            AddCardColorBar(panel3, UiHelper.DeepBlue);
            _lblIncomeValue.ForeColor = UiHelper.SuccessGreen;
            _lblExpenseValue.ForeColor = UiHelper.DangerRed;
            _lblBalanceValue.ForeColor = UiHelper.DeepBlue;
            labelWelcome.ForeColor = UiHelper.TextDark;
            foreach (var lbl in new[] { label1, label2, label3 })
                lbl.Font = new Font("微软雅黑", 14f);
        }

        /// <summary>为卡片面板顶部添加5px颜色标识条</summary>
        private void AddCardColorBar(Panel card, Color color)
        {
            var bar = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(card.Width, 5),
                BackColor = color,
                Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right
            };
            card.Controls.Add(bar);
        }

        /// <summary>高亮当前导航按钮：按下态（深背景+深色粗边框），前一个按钮弹起</summary>
        private void SetActiveNav(Button btn)
        {
            if (_activeNavBtn != null)
            {
                _activeNavBtn.BackColor = UiHelper.DeepBlue;
                _activeNavBtn.FlatAppearance.BorderColor = UiHelper.NavBorderNormal;
                _activeNavBtn.FlatAppearance.BorderSize = 1;
                _activeNavBtn.Tag = null;
            }
            btn.BackColor = UiHelper.ActiveBlue;
            btn.FlatAppearance.BorderColor = UiHelper.NavBorderActive;
            btn.FlatAppearance.BorderSize = 2;
            btn.Tag = "active";
            _activeNavBtn = btn;
        }

        // ===== 导航按钮 =====

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ShowPanel(panelDashBoard);
            SetActiveNav(btnDashboard);
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            ShowPanel(recordControl);
            recordControl.RefreshData();
            SetActiveNav(btnRecord);
        }

        private void btnStatistics_Click(object sender, EventArgs e)
        {
            ShowPanel(statisticsControl);
            statisticsControl.ShowPieChart();
            statisticsControl.ShowBarChart();
            SetActiveNav(btnStatistics);
        }

        private void btnBudget_Click(object sender, EventArgs e)
        {
            ShowPanel(budgetControl);
            budgetControl.RefreshData();
            SetActiveNav(btnBudget);
        }

        private void btnTemplate_Click(object sender, EventArgs e)
        {
            using (var form = new TemplateForm(_connStr))
            {
                if (form.ShowDialog() == DialogResult.OK && form.SelectedTemplate != null)
                {
                    ShowPanel(recordControl);
                    recordControl.UseTemplate(form.SelectedTemplate);
                }
            }
        }

        private void btnData_Click(object sender, EventArgs e)
        {
            using (var form = new DataForm(_connStr))
            {
                form.ShowDialog();
            }
            RefreshDashboard();
            recordControl.RefreshData();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (var form = new SettingForm(_connStr))
            {
                form.ShowDialog();
            }
            labelWelcome.Text = $"欢迎回来，{App.CurrentUsername}";
            RefreshDashboard();
        }

       

        /// <summary>键盘快捷键：Ctrl+1~7 切换导航页</summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.D1: btnDashboard_Click(null, null); return true;
                case Keys.Control | Keys.D2: btnRecord_Click(null, null); return true;
                case Keys.Control | Keys.D3: btnStatistics_Click(null, null); return true;
                case Keys.Control | Keys.D4: btnBudget_Click(null, null); return true;
                case Keys.Control | Keys.D5: btnTemplate_Click(null, null); return true;
                case Keys.Control | Keys.D6: btnData_Click(null, null); return true;
                case Keys.Control | Keys.D7: btnSettings_Click(null, null); return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        // ===== 退出登录 =====

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("确定要退出登录吗？", "退出",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                App.Logout();
                Program.ClearSavedUserId();
                OwnerLoginForm?.Show();
                this.Close();
            }
        }
    }
}
