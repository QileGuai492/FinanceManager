using FinanceManager.Common;
using FinanceManager.Common.Constants;
using FinanceManager.Data.Repositories;
using FinanceManager.Data.Services;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
using FinanceManager.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FinanceManager.Forms
{
    /// <summary>
    /// 模板管理弹窗 —— ShowDialog 模式，管理记账模板的增删改查。
    /// 可使用模板快速跳转到记账页并预填金额、分类、备注。
    /// </summary>
    public partial class TemplateForm : Form
    {
        /// <summary>数据库连接字符串</summary>
        private readonly string _connStr;
        /// <summary>当前正在编辑的模板实体，null 表示新增模式</summary>
        private TemplateEntity _editingTemplate;

        /// <summary>调用方在 ShowDialog 后读取选中的模板，用于跳转记账页预填</summary>
        public TemplateEntity SelectedTemplate { get; private set; }

        /// <summary>构造函数：接收连接字符串（由 MainForm 传入）</summary>
        public TemplateForm(string connectionString)
        {
            InitializeComponent();
            _connStr = connectionString;
        }

        /// <summary>
        /// 窗体加载：设置标题 → 定义 DataGridView 7 列 → 美化 → 加载模板列表
        /// </summary>
        private void TemplateForm_Load(object sender, EventArgs e)
        {
            this.Text = "模板管理";
            this.BackColor = UiHelper.BgLight;
            panelEditor.Visible = false;
            this.StartPosition = FormStartPosition.CenterParent;

            // 定义表格列：名称、金额、类型、分类、备注、常用、次数
            gridTemplate.AutoGenerateColumns = false;
            gridTemplate.Columns.Add("colName", "名称");
            gridTemplate.Columns.Add("colAmount", "金额");
            gridTemplate.Columns.Add("colType", "类型");
            gridTemplate.Columns.Add("colCategory", "分类");
            gridTemplate.Columns.Add("colNote", "备注");
            gridTemplate.Columns.Add("colFavorite", "常用");
            gridTemplate.Columns.Add("colCount", "次数");
            UiHelper.StyleDataGridView(gridTemplate);

            _ = RefreshGrid();  // 异步加载模板列表
        }

        /// <summary>刷新模板列表：根据"只看常用"筛选，加载数据并填充到 DataGridView</summary>
        private async Task RefreshGrid()
        {
            var tplService = new TemplateService(new TemplateRepository(_connStr));
            var catService = new CategoryService(new CategoryRepository(_connStr));

            var templates = checkBoxReadOften.Checked
                ? await tplService.GetFavoriteTemplatesAsync(App.CurrentUserId)
                : await tplService.GetTemplatesAsync(App.CurrentUserId);

            labelTplCount.Text = $"模板数量：{templates.Count()}/{AppConstants.MaxTemplates}";
            gridTemplate.Rows.Clear();

            foreach (var t in templates)
            {
                var cat = await catService.GetCategoryByIdAsync(t.CategoryId);
                gridTemplate.Rows[gridTemplate.Rows.Add(
                    t.Name,
                    Math.Abs(t.DefaultAmount).ToString("N2"),
                    t.Type == RecordType.Expense ? "支出" : "收入",
                    cat?.Name ?? "-",
                    t.NoteTemplate ?? "",
                    t.IsFavorite ? "★" : "",
                    t.UseCount.ToString()
                )].Tag = t;
            }
        }

        /// <summary>"只看常用"勾选切换：重新加载列表（过滤 IsFavorite=true 的模板）</summary>
        private void checkBoxReadOften_CheckedChanged(object sender, EventArgs e)
        {
            _ = RefreshGrid();
        }

        /// <summary>"新增"按钮：清空表单 → 显示编辑器 → 加载支出分类下拉</summary>
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            _editingTemplate = null;
            panelEditor.Visible = true;
            textBoxName.Clear();
            textBoxMoney.Clear();
            textBoxNote.Clear();
            checkBoxOften.Checked = false;
            labelCat2.Visible = false;
            textBoxCategory.Visible = false;
            buttonAddCategory.Visible = true;
            buttonSave.Text = "保存";

            // 切换为支出并加载分类（不能依赖 CheckedChanged，因为设计器默认已是 true）
            if (!radioButtonExpense.Checked)
                radioButtonExpense.Checked = true;
            else
                LoadCategories(0);
        }

        private async void buttonSave_Click(object sender, EventArgs e)
        {
            var name = textBoxName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            { MessageBox.Show("请输入模板名称"); return; }

            // 金额可为空，空视为0
            decimal amt = 0;
            if (!string.IsNullOrWhiteSpace(textBoxMoney.Text)
                && !decimal.TryParse(textBoxMoney.Text, out amt))
            { MessageBox.Show("金额格式不正确"); return; }

            var type = radioButtonExpense.Checked ? RecordType.Expense : RecordType.Income;
            int categoryId;

            // 自定义分类输入框开着 → 先创建分类再保存模板
            if (textBoxCategory.Visible && !string.IsNullOrWhiteSpace(textBoxCategory.Text))
            {
                var catService = new CategoryService(new CategoryRepository(_connStr));
                categoryId = await catService.AddCategoryAsync(new CategoryEntity
                {
                    Name = textBoxCategory.Text.Trim(),
                    Type = type,
                    Color = "#607D8B",
                    Icon = "custom",
                    UserId = App.CurrentUserId
                });
                LoadCategories((int)type);
                comboBoxCategory.SelectedValue = categoryId;
                buttonAddCategory.Text = "+";
                comboBoxCategory.Enabled = true;
                textBoxCategory.Visible = false;
                labelCat2.Visible = false;
            }
            else if (comboBoxCategory.SelectedValue != null)
            {
                categoryId = (int)comboBoxCategory.SelectedValue;
            }
            else
            {
                MessageBox.Show("请选择分类"); return;
            }

            var tplService = new TemplateService(new TemplateRepository(_connStr));

            if (_editingTemplate == null)
            {
                var count = await tplService.GetTemplateCountAsync(App.CurrentUserId);
                if (count >= AppConstants.MaxTemplates)
                { MessageBox.Show($"模板数量已达上限（{AppConstants.MaxTemplates}个）"); return; }

                await tplService.AddTemplateAsync(new TemplateEntity
                {
                    Name = name,
                    DefaultAmount = type == RecordType.Expense ? -amt : amt,
                    Type = type,
                    CategoryId = categoryId,
                    NoteTemplate = textBoxNote.Text.Trim(),
                    IsFavorite = checkBoxOften.Checked,
                    UserId = App.CurrentUserId
                });
            }
            else
            {
                _editingTemplate.Name = name;
                _editingTemplate.DefaultAmount = type == RecordType.Expense ? -amt : amt;
                _editingTemplate.Type = type;
                _editingTemplate.CategoryId = categoryId;
                _editingTemplate.NoteTemplate = textBoxNote.Text.Trim();
                _editingTemplate.IsFavorite = checkBoxOften.Checked;
                await tplService.UpdateTemplateAsync(_editingTemplate);
                _editingTemplate = null;
            }

            panelEditor.Visible = false;
            await RefreshGrid();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            panelEditor.Visible = false;
            _editingTemplate = null;
        }

        private void buttonUse_Click(object sender, EventArgs e)
        {
            // 没选中不做
            if (gridTemplate.CurrentRow == null) return;
            var tpl = gridTemplate.CurrentRow.Tag as TemplateEntity;
            if (tpl == null) return;

            // 增加使用次数（不影响界面显示，等下次打开模板管理时才会看到）
            _ = new TemplateService(new TemplateRepository(_connStr))
                .IncrementUseCountAsync(tpl.Id);

            // 选中模板设为公开属性，供调用方读取
            SelectedTemplate = tpl;

            //设置返回值为OK，关闭窗口
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private async void buttonDelete_Click(object sender, EventArgs e)
        {
            if (gridTemplate.CurrentRow == null) return;
            var tpl = gridTemplate.CurrentRow.Tag as TemplateEntity;
            if (tpl == null) return;

            var result = MessageBox.Show(
                $"确定删除模板 \"{ tpl.Name}\" 吗？", 
                "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes) return;

            await new TemplateService(new TemplateRepository(_connStr))
                .DeleteTemplateAsync(tpl.Id);
            await RefreshGrid();
        }

        private void gridTemplate_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            _editingTemplate = gridTemplate.Rows[e.RowIndex].Tag as TemplateEntity;
            if (_editingTemplate == null) return;

            panelEditor.Visible = true;
            textBoxName.Text = _editingTemplate.Name;
            textBoxMoney.Text = Math.Abs(_editingTemplate.DefaultAmount).ToString();
            radioButtonExpense.Checked = _editingTemplate.Type == RecordType.Expense;
            radioButtonIncome.Checked = _editingTemplate.Type == RecordType.Income;
            LoadCategories((int)_editingTemplate.Type);
            comboBoxCategory.SelectedValue = _editingTemplate.CategoryId;
            textBoxNote.Text = _editingTemplate.NoteTemplate ?? "";
            checkBoxOften.Checked = _editingTemplate.IsFavorite;
            buttonSave.Text = "更新";
        }

        private async void LoadCategories(int type)
        {
            // 同时加载系统默认分类 + 用户自定义分类
            var cats = await new CategoryService(new CategoryRepository(_connStr))
                .GetCategoriesByTypeAsync(App.CurrentUserId, type);
            comboBoxCategory.DataSource = cats.ToList();
            comboBoxCategory.DisplayMember = "Name";
            comboBoxCategory.ValueMember = "Id";
        }

        // ========== 类型切换 → 重载分类 ==========
        private void radioButtonExpense_CheckedChanged(object sender, EventArgs e)
        {
            LoadCategories(radioButtonExpense.Checked ? 0 : 1);
        }

        // 新增分类按钮：切换 展开/收起 自定义输入
        private void buttonAddCategory_Click(object sender, EventArgs e)
        {
            if (buttonAddCategory.Text == "+")
            {
                buttonAddCategory.Text = "−";
                comboBoxCategory.Enabled = false;
                labelCat2.Visible = true;
                textBoxCategory.Visible = true;
                textBoxCategory.Clear();
                textBoxCategory.Focus();
            }
            else
            {
                buttonAddCategory.Text = "+";
                comboBoxCategory.Enabled = true;
                labelCat2.Visible = false;
                textBoxCategory.Visible = false;
            }
        }

        // 选中分类后，恢复界面
        private void comboBoxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonAddCategory.Text = "+";
            comboBoxCategory.Enabled = true;
            textBoxCategory.Visible = false;
            labelCat2.Visible = false;
        }

        // 在新增分类输入框按回车，保存新分类并选中；按Esc取消
        private async  void textBoxCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var name = textBoxCategory.Text.Trim();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var type = radioButtonExpense.Checked ? RecordType.Expense : RecordType.Income;
                    var newId = await new CategoryService(new CategoryRepository(_connStr))
                        .AddCategoryAsync(new CategoryEntity
                        {
                            Name = name,
                            Type = type,
                            Color = "#607D8B",
                            Icon = "custom",
                            UserId = App.CurrentUserId
                        });

                    // 刷新分类下拉并选中新分类
                    LoadCategories((int)type);
                    comboBoxCategory.SelectedValue = newId;
                }

                buttonAddCategory.Text = "+";
                comboBoxCategory.Enabled = true;
                labelCat2.Visible = false;
                textBoxCategory.Visible = false;
                buttonAddCategory.Visible = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                labelCat2.Visible = false;
                textBoxCategory.Visible = false;
                buttonAddCategory.Visible = true;
            }
        }
    }
}
