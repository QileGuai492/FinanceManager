using FinanceManager.Common;
using FinanceManager.Common.Helpers;
using FinanceManager.Data.Repositories;
using FinanceManager.Data.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
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
    public partial class RecordListControl : UserControl
    {
        /// <summary>数据库连接字符串</summary>
        private readonly string _connStr;
        /// <summary>记账页的 ViewModel</summary>
        private RecordViewModel _recordVM;
        /// <summary>当前编辑的记录，null=新增</summary>
        private RecordEntity _editingRecord;
        /// <summary>记录变更事件（新增/编辑/删除），通知 MainForm 刷新仪表盘</summary>
        public event Action RecordChanged;

        // ===== 筛选字段 =====
        /// <summary>-1=全部, 0=支出, 1=收入</summary>
        private int _filterType = -1;
        /// <summary>0=全部分类</summary>
        private int _filterCategoryId = 0;
        /// <summary>防止 DoFilterGrid 并发重入</summary>
        private bool _isFiltering;

        /// <summary>构造函数：接收连接字符串，初始化 UI 控件</summary>
        public RecordListControl(string connStr)
        {
            _connStr = connStr;
            InitializeComponent();
        }

        /// <summary>窗体加载事件（设计器生成，保留空壳）</summary>
        private void RecordListControl_Load(object sender, EventArgs e) { }

        /// <summary>供 MainForm 调用的初始化入口</summary>
        public void Init()
        {
            _recordVM = new RecordViewModel(
                new RecordService(
                    new FinanceManager.Data.Repositories.RecordRepository(_connStr)),
                    new CategoryService(
                        new CategoryRepository(_connStr)
                    )
            );

            this.Dock = DockStyle.Fill;
            UiHelper.MakeGradient(this, UiHelper.SoftBlue, Color.White);
            panelEditor.Visible = false;
            gridRecord.AutoGenerateColumns = false;
            gridRecord.Columns.Add("colDate", "日期");
            gridRecord.Columns.Add("colType", "类型");
            gridRecord.Columns.Add("colCategory", "分类");
            gridRecord.Columns.Add("colAmount", "金额");
            gridRecord.Columns.Add("colNote", "备注");
            UiHelper.StyleDataGridView(gridRecord);

            // 按钮美化
            UiHelper.StyleButton(buttonNew, UiHelper.DeepBlue, Color.White);
            UiHelper.BindHover(buttonNew, UiHelper.DeepBlue, UiHelper.LightBlue);
            UiHelper.StyleButton(buttonSave, UiHelper.SuccessGreen, Color.White);
            UiHelper.BindHover(buttonSave, UiHelper.SuccessGreen, Color.FromArgb(0x66, 0xBB, 0x6A));
            UiHelper.StyleButton(buttonCancel, UiHelper.TextGray, Color.White);
            UiHelper.BindHover(buttonCancel, UiHelper.TextGray, UiHelper.BorderGray);
            UiHelper.StyleButton(buttonDelete, UiHelper.DangerRed, Color.White);
            UiHelper.BindHover(buttonDelete, UiHelper.DangerRed, Color.FromArgb(0xEF, 0x53, 0x50));

            LoadCategories(0);
            RefreshRecordGrid();
        }

        // ===== 编辑面板（新增/编辑/删除记账记录）=====

        /// <summary>"新增"按钮：打开编辑面板，默认支出模式</summary>
        private void buttonNew_Click(object sender, EventArgs e)
        {
            panelEditor.BackColor = Color.White;
            panelEditor.Visible = true;
            _editingRecord = null;
            textBoxMoney.Clear();
            rdoExpense.Checked = true;
            LoadCategories(0);  // 显式加载，避免 rdoExpense 已选中时不触发 CheckedChanged
            dateTimePicker1.Value = DateTime.Today;
            textBoxNote.Clear();
            buttonSave.Text = "保存";
        }

        /// <summary>收支类型切换：支出=0，收入=1，重载分类下拉</summary>
        private void rdoExpense_CheckedChanged(object sender, EventArgs e)
        {
            LoadCategories(rdoExpense.Checked ? 0 : 1);
        }

        /// <summary>"取消"按钮：隐藏编辑面板</summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            panelEditor.Visible = false;
            _editingRecord = null;
        }

        /// <summary>"保存"按钮：校验金额和分类 → 新增或更新记录</summary>
        private async void buttonSave_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(textBoxMoney.Text, out var amt) || amt <= 0)
            {
                MessageBox.Show("请输入有效金额"); return;
            }
            if (comboBoxCategory.SelectedValue == null)
            {
                MessageBox.Show("请选择分类"); return;
            }

            var type = rdoExpense.Checked ? RecordType.Expense : RecordType.Income;

            if (_editingRecord == null)
            {
                await _recordVM.AddRecordAsync(new RecordEntity
                {
                    Amount = type == RecordType.Expense ? -amt : amt,
                    Currency = "CNY",
                    Type = type,
                    CategoryId = (int)comboBoxCategory.SelectedValue,
                    Date = dateTimePicker1.Value,
                    Note = textBoxNote.Text,
                    UserId = App.CurrentUserId
                });
                // 新增后保持面板打开，清空金额和备注继续记
                textBoxMoney.Clear();
                textBoxNote.Clear();
                textBoxMoney.Focus();
            }
            else
            {
                _editingRecord.Amount = type == RecordType.Expense ? -amt : amt;
                _editingRecord.Type = type;
                _editingRecord.CategoryId = (int)comboBoxCategory.SelectedValue;
                _editingRecord.Date = dateTimePicker1.Value;
                _editingRecord.Note = textBoxNote.Text;
                await _recordVM.UpdateRecordAsync(_editingRecord);
                _editingRecord = null;
                panelEditor.Visible = false;
            }
            RefreshRecordGrid();
            RecordChanged?.Invoke();
        }

        /// <summary>双击记录行：填充编辑面板，进入编辑模式</summary>
        private void gridRecord_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gridRecord.CurrentRow == null) return;
            _editingRecord = gridRecord.CurrentRow.Tag as RecordEntity;
            if (_editingRecord == null) return;

            panelEditor.Visible = true;
            textBoxMoney.Text = Math.Abs(_editingRecord.Amount).ToString();
            if (_editingRecord.Type == RecordType.Expense)
                rdoExpense.Checked = true;
            else
                rdoIncome.Checked = true;
            LoadCategories((int)_editingRecord.Type);
            comboBoxCategory.SelectedValue = _editingRecord.CategoryId;
            dateTimePicker1.Value = _editingRecord.Date;
            textBoxNote.Text = _editingRecord.Note ?? "";
            buttonSave.Text = "更新";
        }

        /// <summary>"删除"按钮：确认后删除选中记录</summary>
        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (gridRecord.CurrentRow == null) return;
            if (MessageBox.Show("确定删除？", "确认",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;

            var record = gridRecord.CurrentRow.Tag as RecordEntity;
            if (record != null) await _recordVM.DeleteRecordAsync(record.Id);
            RefreshRecordGrid();
            RecordChanged?.Invoke();
        }

        // ===== 分类加载 =====

        /// <summary>加载指定类型的分类到编辑器下拉框</summary>
        private async void LoadCategories(int type)
        {
            var cats = await new CategoryService(new CategoryRepository(_connStr))
                .GetCategoriesByTypeAsync(App.CurrentUserId, type);
            comboBoxCategory.DataSource = cats.ToList();
            comboBoxCategory.DisplayMember = "Name";
            comboBoxCategory.ValueMember = "Id";
        }

        // ===== 筛选（全部/支出/收入 + 分类下拉）=====

        /// <summary>筛选类型切换：重载分类下拉并触发重新查询</summary>
        private async void FilterType_CheckedChanged(object sender, EventArgs e)
        {
            var rb = sender as RadioButton;
            if (rb == null || !rb.Checked) return;

            if (rb == rdoCTAll)
            {
                _filterType = -1;
                comboBoxFilter.Enabled = true;
                // 重载全部分类（收入+支出）供下拉显示
                var allCats = (await new CategoryService(new CategoryRepository(_connStr))
                    .GetCategoriesAsync(App.CurrentUserId)).ToList();
                allCats.Insert(0, new CategoryEntity { Id = 0, Name = "全部" });
                comboBoxFilter.DataSource = allCats;
                comboBoxFilter.DisplayMember = "Name";
                comboBoxFilter.ValueMember = "Id";
                comboBoxFilter.SelectedIndex = 0;
            }
            else
            {
                _filterType = rdoCTExpense.Checked ? 0 : 1;
                comboBoxFilter.Enabled = true;

                var cats = (await new CategoryService(new CategoryRepository(_connStr))
                    .GetCategoriesByTypeAsync(App.CurrentUserId, _filterType)).ToList();
                cats.Insert(0, new CategoryEntity { Id = 0, Name = "全部" });
                comboBoxFilter.DataSource = cats;
                comboBoxFilter.DisplayMember = "Name";
                comboBoxFilter.ValueMember = "Id";
                comboBoxFilter.SelectedIndex = 0;
            }
            await DoFilterGrid();
        }

        /// <summary>分类筛选下拉切换：更新筛选项并重新查询</summary>
        private async void comboBoxFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFilter.SelectedValue is int val)
                _filterCategoryId = val;
            else return;
            await DoFilterGrid();
        }

        /// <summary>执行筛选查询：按类型+分类过滤记录，刷新 DataGridView，防并发重入</summary>
        private async Task DoFilterGrid()
        {
            if (_isFiltering) return;
            _isFiltering = true;
            try
            {
            await _recordVM.LoadRecordsAsync(App.CurrentUserId);
            var records = _recordVM.Records.ToList();

            if (_filterType >= 0)
                records = records.Where(r => (int)r.Type == _filterType).ToList();
            if (_filterCategoryId > 0)
                records = records.Where(r => r.CategoryId == _filterCategoryId).ToList();

            var catService = new CategoryService(new CategoryRepository(_connStr));
            gridRecord.Rows.Clear();
            foreach (var r in records)
            {
                var cat = await catService.GetCategoryByIdAsync(r.CategoryId);
                var row = gridRecord.Rows[gridRecord.Rows.Add(
                    r.Date.ToString("yyyy-MM-dd"),
                    r.Type == RecordType.Expense ? "支出" : "收入",
                    cat?.Name ?? "-",
                    Math.Abs(r.Amount).ToString("N2"),
                    r.Note ?? "")];
                row.Tag = r;
            }
            }
            finally { _isFiltering = false; }
        }

        /// <summary>刷新记录表格（异步调用 DoFilterGrid）</summary>
        private async void RefreshRecordGrid()
        {
            await DoFilterGrid();
        }

        // ===== 模板跳转：预填编辑器 =====

        /// <summary>供 MainForm 调用，切到记账页时刷新数据</summary>
        public void RefreshData()
        {
            if (!_isFiltering) RefreshRecordGrid();
        }

        /// <summary>从模板跳转：预填金额、分类、备注并打开编辑器</summary>
        public void UseTemplate(TemplateEntity tpl)
        {
            panelEditor.Visible = true;
            _editingRecord = null;
            buttonSave.Text = "保存";
            textBoxMoney.Text = Math.Abs(tpl.DefaultAmount).ToString();
            rdoExpense.Checked = tpl.Type == RecordType.Expense;
            rdoIncome.Checked = tpl.Type == RecordType.Income;
            LoadCategories((int)tpl.Type);
            comboBoxCategory.SelectedValue = tpl.CategoryId;
            dateTimePicker1.Value = DateTime.Today;
            textBoxNote.Text = tpl.NoteTemplate ?? "";
        }

        private async void buttonAI_Click(object sender, EventArgs e)
        {
            var text = textBoxAI.Text.Trim();
            if (string.IsNullOrWhiteSpace(text)) { MessageBox.Show("请粘贴消费记录文字"); return; }

            var config = AiConfig.Load();
            if (string.IsNullOrEmpty(config.ApiKey))
            { MessageBox.Show("请先在设置中配置AI API Key"); return; }

            try
            {
                buttonAI.Enabled = false;
                buttonAI.Text = "解析中...";

                var analyzer = new AiAnalyzer(config.Endpoint, config.ApiKey, config.Model);
                var prompt = $"请将以下消费记录文本解析为CSV格式（日期,类型,分类,金额,备注）。" +
                    $"今天是{DateTime.Today:yyyy-MM-dd}。规则：类型只能是\"支出\"或\"收入\"；金额为正数；日期格式yyyy-MM-dd；无日期的视为今天。" +
                    $"只返回CSV内容，不要任何解释。\n\n{text}";

                var csv = await analyzer.CallChatAsync(prompt);
                if (string.IsNullOrWhiteSpace(csv)) { MessageBox.Show("AI未能解析"); return; }

                var rows = CsvHelper.ParseCsv(csv);
                var dataRows = rows.Where(r => r.Length >= 3).ToList();
                if (!dataRows.Any()) { MessageBox.Show("AI未能解析出有效记录"); return; }

                var catService = new CategoryService(new CategoryRepository(_connStr));
                var allCats = (await catService.GetCategoriesAsync(App.CurrentUserId)).ToList();

                int success = 0, fail = 0;
                foreach (var row in dataRows)
                {
                    try
                    {
                        if (!DateTime.TryParse(row[0], out var date)) { fail++; continue; }
                        var type = row[1].Trim() == "收入" ? RecordType.Income : RecordType.Expense;
                        var catName = row[2].Trim();

                        var cat = allCats.FirstOrDefault(c => c.Name == catName && c.Type == type);
                        if (cat == null)
                        {
                            var match = await AiMatchCategory(catName, type, allCats, config);
                            if (match != null) cat = allCats.FirstOrDefault(c => c.Name == match && c.Type == type);
                            if (cat == null)
                            {
                                var newId = await catService.AddCategoryAsync(new CategoryEntity
                                { Name = catName, Type = type, Color = "#607D8B", Icon = "custom", UserId = App.CurrentUserId });
                                cat = new CategoryEntity { Id = newId, Name = catName, Type = type };
                                allCats.Add(cat);
                            }
                        }

                        if (!decimal.TryParse(row[3], out var amt) || amt <= 0) { fail++; continue; }
                        var note = row.Length > 4 ? row[4].Trim() : "";

                        await _recordVM.AddRecordAsync(new RecordEntity
                        { Date = date, Type = type, CategoryId = cat.Id,
                          Amount = type == RecordType.Expense ? -amt : amt,
                          Currency = "CNY", Note = string.IsNullOrWhiteSpace(note) ? null : note,
                          UserId = App.CurrentUserId });
                        success++;
                    }
                    catch { fail++; }
                }

                textBoxAI.Clear();
                RefreshRecordGrid();
                RecordChanged?.Invoke();
                MessageBox.Show($"导入完成\n成功：{success} 条，跳过：{fail} 条",
                    "AI导入", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { MessageBox.Show($"解析失败：{ex.Message}"); }
            finally { buttonAI.Enabled = true; buttonAI.Text = "确定"; }
        }

        private async Task<string> AiMatchCategory(string catName, RecordType type,
            List<CategoryEntity> allCats, AiConfig config)
        {
            try
            {
                var typeStr = type == RecordType.Expense ? "支出" : "收入";
                var catList = string.Join(", ", allCats.Where(c => c.Type == type).Select(c => c.Name).Distinct());
                if (string.IsNullOrEmpty(catList)) return null;

                var analyzer = new AiAnalyzer(config.Endpoint, config.ApiKey, config.Model);
                var prompt = $"现有{typeStr}分类：[{catList}]。" +
                    $"请判断\"{catName}\"最应该归入其中哪个分类？只返回分类名。如果都不匹配，返回\"无\"。";
                var result = await analyzer.CallChatAsync(prompt);
                var match = result?.Trim().Trim('"', '\'');
                return allCats.Any(c => c.Name == match && c.Type == type) ? match : null;
            }
            catch { return null; }
        }
    }
}
