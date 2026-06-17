using FinanceManager.Common;
using FinanceManager.Common.Helpers;
using FinanceManager.Data.Database;
using FinanceManager.Data.Repositories;
using FinanceManager.Data.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
using FinanceManager.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace FinanceManager.Forms
{
    /// <summary>
    /// 数据管理弹窗 —— 提供日期/类型/分类筛选查询、CSV 导入导出功能。
    /// 通过 RecordService + CategoryService 操作真实数据库。
    /// </summary>
    public partial class DataForm : Form
    {
        /// <summary>数据库连接字符串</summary>
        private readonly string _connStr;
        /// <summary>当前筛选结果缓存，用于导出和刷新表格</summary>
        private List<RecordEntity> _filteredRecords = new List<RecordEntity>();

        /// <summary>构造函数：接收连接字符串（由 MainForm 传入）</summary>
        public DataForm(string connectionString)
        {
            _connStr = connectionString;
            InitializeComponent();
        }

        /// <summary>窗体加载：显示数据库路径 → 设置默认日期为本月1日到今日 → 美化表格</summary>
        private void DataForm_Load(object sender, EventArgs e)
        {
            this.Text = "数据管理";
            this.BackColor = UiHelper.BgLight;
            this.StartPosition = FormStartPosition.CenterParent;
            labelPath.Text = $"数据文件：{DatabaseManager.Instance.DatabasePath}";

            dtpFrom.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1); // 默认本月1日
            dtpTo.Value = DateTime.Today;    // 默认今天
            gridRecords.AutoGenerateColumns = false;
            gridRecords.Columns.Add("colDate", "日期");
            gridRecords.Columns.Add("colType", "类型");
            gridRecords.Columns.Add("colCategory", "分类");
            gridRecords.Columns.Add("colAmount", "金额");
            gridRecords.Columns.Add("colNote", "备注");
            UiHelper.StyleDataGridView(gridRecords);

            // 按钮美化
            UiHelper.StyleButton(buttonCheck, UiHelper.DeepBlue, Color.White);
            UiHelper.BindHover(buttonCheck, UiHelper.DeepBlue, UiHelper.LightBlue);
            UiHelper.StyleButton(buttonOutput, UiHelper.SuccessGreen, Color.White);
            UiHelper.BindHover(buttonOutput, UiHelper.SuccessGreen, UiHelper.SuccessGreenHover);
            UiHelper.StyleButton(buttonInput, UiHelper.DeepBlue, Color.White);
            UiHelper.BindHover(buttonInput, UiHelper.DeepBlue, UiHelper.LightBlue);
            UiHelper.StyleButton(buttonTxt, UiHelper.DeepBlue, Color.White);
            UiHelper.BindHover(buttonTxt, UiHelper.DeepBlue, UiHelper.LightBlue);
            UiHelper.StyleButton(buttonDeleteAll, UiHelper.DangerRed, Color.White);
            UiHelper.BindHover(buttonDeleteAll, UiHelper.DangerRed, UiHelper.DangerRedHover);

            // 初始加载分类（设计器里 Checked=true 在事件绑定之前，错过了）
            LoadFilterCategories();
        }

        /// <summary>类型筛选单选框切换：根据选中类型（全部/支出/收入）加载对应分类下拉</summary>
        private void FilterType_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            if (rb == null || !rb.Checked) return;

            comboBoxCategory.Enabled = true;
            LoadFilterCategories();
        }

        /// <summary>加载分类下拉列表：根据当前选中的类型筛选对应的分类项，首项插入"全部"</summary>
        private async void LoadFilterCategories()
        {
            try
            {
            List<CategoryEntity> cats;

            if (radioButtonAll.Checked)
                cats = (await new CategoryService(new CategoryRepository(_connStr))
                    .GetCategoriesAsync(App.CurrentUserId)).ToList();                // 加载所有分类
            else if (radioButtonExpense.Checked)
                cats = (await new CategoryService(new CategoryRepository(_connStr))
                    .GetCategoriesByTypeAsync(App.CurrentUserId, 0)).ToList();       // 0=支出
            else
                cats = (await new CategoryService(new CategoryRepository(_connStr))
                    .GetCategoriesByTypeAsync(App.CurrentUserId, 1)).ToList();       // 1=收入

            cats.Insert(0, new CategoryEntity { Id = 0, Name = "全部" });            // 插入"全部"选项
            comboBoxCategory.DataSource = cats;
            comboBoxCategory.DisplayMember = "Name";
            comboBoxCategory.ValueMember = "Id";
            comboBoxCategory.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载分类失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>查询按钮：按 日期范围 → 类型 → 分类 逐层过滤，缓存结果并刷新表格</summary>
        private async void buttonCheck_Click(object sender, EventArgs e)
        {
            try
            {
            var records = (await new RecordService(
                new FinanceManager.Data.Repositories.RecordRepository(_connStr))
                .GetRecordsAsync(App.CurrentUserId)).ToList();

            // 第1层过滤：日期范围（包含当天）
            var from = dtpFrom.Value.Date;
            var to = dtpTo.Value.Date.AddDays(1);
            records = records.Where(r => r.Date >= from && r.Date < to).ToList();

            // 第2层过滤：类型（支出/收入）
            if (radioButtonExpense.Checked)
                records = records.Where(r => r.Type == RecordType.Expense).ToList();
            else if (radioButtonIncome.Checked)
                records = records.Where(r => r.Type == RecordType.Income).ToList();

            // 第3层过滤：具体分类
            if (comboBoxCategory.SelectedValue != null
                && (int)comboBoxCategory.SelectedValue > 0)
            {
                var catId = (int)comboBoxCategory.SelectedValue;
                records = records.Where(r => r.CategoryId == catId).ToList();
            }

            _filteredRecords = records.OrderBy(r => r.Date).ToList();  // 按日期升序排列
            RefreshGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"查询失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>刷新 DataGridView：将 _filteredRecords 逐行填充到表格，行 Tag 存实体引用</summary>
        private async void RefreshGrid()
        {
            try
            {
            var catService = new CategoryService(new CategoryRepository(_connStr));
            gridRecords.Rows.Clear();

            foreach (var r in _filteredRecords)
            {
                var cat = await catService.GetCategoryByIdAsync(r.CategoryId);
                gridRecords.Rows[gridRecords.Rows.Add(
                    r.Date.ToString("yyyy-MM-dd"),          // 列1：日期
                    r.Type == RecordType.Expense ? "支出" : "收入",  // 列2：类型
                    cat?.Name ?? "-",                       // 列3：分类名
                    $"{CurrencyHelper.GetSymbol(r.Currency)}{Math.Abs(r.Amount):N2}",      // 列4：金额（带币种符号）
                    r.Note ?? ""                            // 列5：备注
                )].Tag = r;  // Tag 存储实体引用，供后续操作使用
            }

            labelRecordCount.Text = $"共 {_filteredRecords.Count} 条记录";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"刷新表格失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>导出按钮：将当前筛选结果导出为 UTF-8 CSV 文件（含分类名）</summary>
        private void buttonOutput_Click(object sender, EventArgs e)
        {
            if (_filteredRecords.Count == 0)
            {
                MessageBox.Show("没有可导出的记录，请先查询。", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dialog = new SaveFileDialog
            {
                Filter = "CSV文件|*.csv",
                FileName = $"账本导出_{dtpFrom.Value:yyyyMMdd}-{dtpTo.Value:yyyyMMdd}.csv"
            })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                ExportWithCategoryNames(dialog.FileName);
            }
        }

        /// <summary>执行 CSV 导出：逐行构建含日期、类型、分类名、金额、备注的数据并写入文件</summary>
        private async void ExportWithCategoryNames(string filePath)
        {
            var catService = new CategoryService(new CategoryRepository(_connStr));
            var rows = new List<string[]>
            {
                new[] { "日期", "类型", "分类", "金额", "备注" }  // CSV 表头
            };

            foreach (var r in _filteredRecords)
            {
                var cat = await catService.GetCategoryByIdAsync(r.CategoryId);
                rows.Add(new[]
                {
                    r.Date.ToString("yyyy-MM-dd"),
                    r.Type == RecordType.Expense ? "支出" : "收入",
                    cat?.Name ?? "-",
                    Math.Abs(r.Amount).ToString("F2"),
                    r.Note ?? ""
                });
            }

            var csv = CsvHelper.ToCsv(rows);
            File.WriteAllText(filePath, csv, Encoding.UTF8);       // UTF-8 编码支持中文
            MessageBox.Show($"导出成功：{_filteredRecords.Count} 条记录", "导出完成",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>导入按钮：读取 CSV 文件 → 逐行解析校验 → 匹配分类 → 写入数据库</summary>
        private async void buttonInput_Click(object sender, EventArgs e)
        {
            try
            {
            using (var dialog = new OpenFileDialog { Filter = "CSV文件|*.csv" })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                var csv = File.ReadAllText(dialog.FileName, Encoding.UTF8);
                var rows = CsvHelper.ParseCsv(csv);               // 解析 CSV 内容

                if (rows.Count < 2)  // 至少要有表头 + 1 行数据
                {
                    MessageBox.Show("CSV文件为空或格式不正确", "错误",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var dataRows = rows.Skip(1).ToList();
                await ImportDataRows(dataRows);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"导入CSV失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>AI 辅助分类匹配：将未知分类名发给 AI，推荐最接近的已有分类</summary>
        private async Task<string> AiMatchCategoryAsync(string catName, RecordType type,
            List<CategoryEntity> allCats, AiConfig config)
        {
            try
            {
                var typeStr = type == RecordType.Expense ? "支出" : "收入";
                var catList = string.Join(", ",
                    allCats.Where(c => c.Type == type && c.Id > 0)
                           .Select(c => c.Name).Distinct());

                if (string.IsNullOrEmpty(catList)) return null;

                var prompt = $"现有{typeStr}分类：[{catList}]。" +
                    $"请判断\"{catName}\"最应该归入其中哪个分类？" +
                    $"只返回分类名，不加任何解释。如果都不匹配，返回\"无\"。";

                var analyzer = new AiAnalyzer(config.Endpoint, config.ApiKey, config.Model);
                var result = await analyzer.CallChatAsync(prompt);
                var match = result?.Trim().Trim('"', '\'');

                return allCats.Any(c => c.Name == match && c.Type == type) ? match : null;
            }
            catch { return null; }
        }

        private async void buttonTxt_Click(object sender, EventArgs e)
        {
            var config = AiConfig.Load();
            if (string.IsNullOrEmpty(config.ApiKey))
            {
                MessageBox.Show("请在设置中配置AI API Key后再使用此功能", "提示",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var dialog = new OpenFileDialog { Filter = "文本文件|*.txt" })
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                var text = File.ReadAllText(dialog.FileName, Encoding.UTF8);
                if (string.IsNullOrWhiteSpace(text))
                {
                    MessageBox.Show("文件内容为空", "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 交给 AI 解析为 CSV
                var analyzer = new AiAnalyzer(config.Endpoint, config.ApiKey, config.Model);
                var prompt = $"请将以下消费记录文本解析为CSV格式（日期,类型,分类,金额,备注）。\n" +
                    $"今天是{DateTime.Today:yyyy-MM-dd}。规则：类型只能是“支出”或”收入”；金额为正数；日期格式yyyy-MM-dd；无日期的视为今天。\n" +
                    $"只返回CSV内容，不要解释。\n\n{text}";

                try
                {
                    var csv = await analyzer.CallChatAsync(prompt);
                    if (string.IsNullOrWhiteSpace(csv))
                    {
                        MessageBox.Show("AI未能解析该文本", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    var rows = CsvHelper.ParseCsv(csv);
                    // 跳过可能的表头行（如果AI返回了表头）
                    var dataRows = rows.Where(r => r.Length >= 3).ToList();
                    if (!dataRows.Any())
                    {
                        MessageBox.Show("AI未能解析出有效记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    await ImportDataRows(dataRows);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"AI解析失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>导入数据行（CSV解析后的行数据），供 CSV 和 AI 文本导入共用</summary>
        private async Task ImportDataRows(List<string[]> dataRows)
        {
            var catService = new CategoryService(new CategoryRepository(_connStr));
            var allCats = (await catService.GetCategoriesAsync(App.CurrentUserId)).ToList();
            var recordService = new RecordService(
                new FinanceManager.Data.Repositories.RecordRepository(_connStr));
            var aiConfig = AiConfig.Load();
            bool aiEnabled = !string.IsNullOrEmpty(aiConfig.ApiKey);

            int success = 0, fail = 0;
            foreach (var row in dataRows)
            {
                try
                {
                    if (row.Length < 3) { fail++; continue; }
                    if (!DateTime.TryParse(row[0], out var date)) { fail++; continue; }

                    var typeStr = row[1].Trim();
                    var type = typeStr == "收入" ? RecordType.Income : RecordType.Expense;
                    var catName = row[2].Trim();

                    var cat = allCats.FirstOrDefault(c =>
                        c.Name == catName && c.Type == type);

                    if (cat == null)
                    {
                        if (aiEnabled)
                        {
                            var matchedName = await AiMatchCategoryAsync(catName, type, allCats, aiConfig);
                            cat = allCats.FirstOrDefault(c => c.Name == matchedName && c.Type == type);
                        }
                        if (cat == null)
                        {
                            var newId = await catService.AddCategoryAsync(new CategoryEntity
                            {
                                Name = catName, Type = type, Color = "#607D8B",
                                Icon = "custom", UserId = App.CurrentUserId
                            });
                            cat = new CategoryEntity { Id = newId, Name = catName, Type = type };
                            allCats.Add(cat);
                        }
                    }

                    if (!decimal.TryParse(row[3], out var amt) || amt <= 0) { fail++; continue; }
                    var note = row.Length > 4 ? row[4].Trim() : "";

                    await recordService.AddRecordAsync(new RecordEntity
                    {
                        Date = date, Type = type, CategoryId = cat.Id,
                        Amount = type == RecordType.Expense ? -amt : amt,
                        Currency = App.CurrentUserCurrency,
                        Note = string.IsNullOrWhiteSpace(note) ? null : note,
                        UserId = App.CurrentUserId
                    });
                    success++;
                }
                catch (Exception ex) { fail++; System.Diagnostics.Debug.WriteLine($"CSV导入行{success + fail}失败: {ex.Message}"); }
            }

            MessageBox.Show($"导入完成\n成功：{success} 条\n跳过：{fail} 条",
                "导入结果", MessageBoxButtons.OK,
                success > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            if (success > 0)
                buttonCheck_Click(null, null);
        }

        private async void buttonDeleteAll_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("确定要删除当前用户所有记账记录吗？此操作不可恢复！",
                "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            try
            {
                var recordService = new RecordService(
                    new FinanceManager.Data.Repositories.RecordRepository(_connStr));
                var records = (await recordService.GetRecordsAsync(App.CurrentUserId)).ToList();
                foreach (var r in records)
                    await recordService.DeleteRecordAsync(r.Id);

                _filteredRecords.Clear();
                RefreshGrid();
                MessageBox.Show($"已删除 {records.Count} 条记录", "完成",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"删除失败：{ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
