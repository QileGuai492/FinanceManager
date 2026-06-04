namespace FinanceManager.UserControls
{
    partial class RecordListControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label13 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.rdoCTIncome = new System.Windows.Forms.RadioButton();
            this.rdoCTExpense = new System.Windows.Forms.RadioButton();
            this.rdoCTAll = new System.Windows.Forms.RadioButton();
            this.label12 = new System.Windows.Forms.Label();
            this.comboBoxFilter = new System.Windows.Forms.ComboBox();
            this.panelEditor = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.rdoIncome = new System.Windows.Forms.RadioButton();
            this.rdoExpense = new System.Windows.Forms.RadioButton();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxMoney = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.gridRecord = new System.Windows.Forms.DataGridView();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonNew = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxAI = new System.Windows.Forms.TextBox();
            this.buttonAI = new System.Windows.Forms.Button();
            this.panel6.SuspendLayout();
            this.panelEditor.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecord)).BeginInit();
            this.SuspendLayout();
            // 
            // label13
            // 
            this.label13.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(146, 228);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 15);
            this.label13.TabIndex = 26;
            this.label13.Text = "筛选：";
            // 
            // panel6
            // 
            this.panel6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel6.Controls.Add(this.rdoCTIncome);
            this.panel6.Controls.Add(this.rdoCTExpense);
            this.panel6.Controls.Add(this.rdoCTAll);
            this.panel6.Location = new System.Drawing.Point(216, 216);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(215, 35);
            this.panel6.TabIndex = 25;
            // 
            // rdoCTIncome
            // 
            this.rdoCTIncome.AutoSize = true;
            this.rdoCTIncome.Location = new System.Drawing.Point(141, 13);
            this.rdoCTIncome.Name = "rdoCTIncome";
            this.rdoCTIncome.Size = new System.Drawing.Size(58, 19);
            this.rdoCTIncome.TabIndex = 17;
            this.rdoCTIncome.TabStop = true;
            this.rdoCTIncome.Text = "收入";
            this.rdoCTIncome.UseVisualStyleBackColor = true;
            this.rdoCTIncome.CheckedChanged += new System.EventHandler(this.FilterType_CheckedChanged);
            // 
            // rdoCTExpense
            // 
            this.rdoCTExpense.AutoSize = true;
            this.rdoCTExpense.Location = new System.Drawing.Point(73, 13);
            this.rdoCTExpense.Name = "rdoCTExpense";
            this.rdoCTExpense.Size = new System.Drawing.Size(58, 19);
            this.rdoCTExpense.TabIndex = 1;
            this.rdoCTExpense.TabStop = true;
            this.rdoCTExpense.Text = "支出";
            this.rdoCTExpense.UseVisualStyleBackColor = true;
            this.rdoCTExpense.CheckedChanged += new System.EventHandler(this.FilterType_CheckedChanged);
            // 
            // rdoCTAll
            // 
            this.rdoCTAll.AutoSize = true;
            this.rdoCTAll.Checked = true;
            this.rdoCTAll.Location = new System.Drawing.Point(4, 13);
            this.rdoCTAll.Name = "rdoCTAll";
            this.rdoCTAll.Size = new System.Drawing.Size(58, 19);
            this.rdoCTAll.TabIndex = 0;
            this.rdoCTAll.TabStop = true;
            this.rdoCTAll.Text = "全部";
            this.rdoCTAll.UseVisualStyleBackColor = true;
            this.rdoCTAll.CheckedChanged += new System.EventHandler(this.FilterType_CheckedChanged);
            // 
            // label12
            // 
            this.label12.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(185, 288);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(82, 15);
            this.label12.TabIndex = 24;
            this.label12.Text = "具体分类：";
            // 
            // comboBoxFilter
            // 
            this.comboBoxFilter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxFilter.FormattingEnabled = true;
            this.comboBoxFilter.Location = new System.Drawing.Point(273, 284);
            this.comboBoxFilter.Name = "comboBoxFilter";
            this.comboBoxFilter.Size = new System.Drawing.Size(121, 23);
            this.comboBoxFilter.TabIndex = 23;
            this.comboBoxFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilter_SelectedIndexChanged);
            // 
            // panelEditor
            // 
            this.panelEditor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelEditor.Controls.Add(this.panel5);
            this.panelEditor.Controls.Add(this.buttonCancel);
            this.panelEditor.Controls.Add(this.buttonSave);
            this.panelEditor.Controls.Add(this.textBoxNote);
            this.panelEditor.Controls.Add(this.label11);
            this.panelEditor.Controls.Add(this.dateTimePicker1);
            this.panelEditor.Controls.Add(this.label10);
            this.panelEditor.Controls.Add(this.comboBoxCategory);
            this.panelEditor.Controls.Add(this.label9);
            this.panelEditor.Controls.Add(this.label8);
            this.panelEditor.Controls.Add(this.textBoxMoney);
            this.panelEditor.Controls.Add(this.label7);
            this.panelEditor.Location = new System.Drawing.Point(822, 228);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(265, 352);
            this.panelEditor.TabIndex = 22;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.rdoIncome);
            this.panel5.Controls.Add(this.rdoExpense);
            this.panel5.Location = new System.Drawing.Point(92, 74);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(131, 29);
            this.panel5.TabIndex = 14;
            // 
            // rdoIncome
            // 
            this.rdoIncome.AutoSize = true;
            this.rdoIncome.Location = new System.Drawing.Point(68, 4);
            this.rdoIncome.Name = "rdoIncome";
            this.rdoIncome.Size = new System.Drawing.Size(58, 19);
            this.rdoIncome.TabIndex = 13;
            this.rdoIncome.Text = "收入";
            this.rdoIncome.UseVisualStyleBackColor = true;
            // 
            // rdoExpense
            // 
            this.rdoExpense.AutoSize = true;
            this.rdoExpense.Checked = true;
            this.rdoExpense.Location = new System.Drawing.Point(4, 5);
            this.rdoExpense.Name = "rdoExpense";
            this.rdoExpense.Size = new System.Drawing.Size(58, 19);
            this.rdoExpense.TabIndex = 12;
            this.rdoExpense.TabStop = true;
            this.rdoExpense.Text = "支出";
            this.rdoExpense.UseVisualStyleBackColor = true;
            this.rdoExpense.CheckedChanged += new System.EventHandler(this.rdoExpense_CheckedChanged);
            // 
            // buttonCancel
            // 
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCancel.Location = new System.Drawing.Point(143, 296);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 30);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.AutoSize = true;
            this.buttonSave.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSave.Location = new System.Drawing.Point(25, 296);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(75, 37);
            this.buttonSave.TabIndex = 10;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxNote
            // 
            this.textBoxNote.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxNote.Location = new System.Drawing.Point(80, 229);
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(140, 34);
            this.textBoxNote.TabIndex = 9;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(22, 240);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 15);
            this.label11.TabIndex = 8;
            this.label11.Text = "备注：";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(81, 177);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(142, 25);
            this.dateTimePicker1.TabIndex = 7;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(22, 184);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(52, 15);
            this.label10.TabIndex = 6;
            this.label10.Text = "日期：";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(81, 126);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(143, 35);
            this.comboBoxCategory.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(22, 138);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 15);
            this.label9.TabIndex = 4;
            this.label9.Text = "分类：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(22, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(52, 15);
            this.label8.TabIndex = 3;
            this.label8.Text = "类型：";
            // 
            // textBoxMoney
            // 
            this.textBoxMoney.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxMoney.Location = new System.Drawing.Point(80, 19);
            this.textBoxMoney.Name = "textBoxMoney";
            this.textBoxMoney.Size = new System.Drawing.Size(140, 34);
            this.textBoxMoney.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(22, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 0;
            this.label7.Text = "金额：";
            // 
            // gridRecord
            // 
            this.gridRecord.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gridRecord.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridRecord.Location = new System.Drawing.Point(93, 335);
            this.gridRecord.Name = "gridRecord";
            this.gridRecord.RowHeadersWidth = 51;
            this.gridRecord.RowTemplate.Height = 27;
            this.gridRecord.Size = new System.Drawing.Size(580, 226);
            this.gridRecord.TabIndex = 21;
            this.gridRecord.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridRecord_CellContentDoubleClick);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonDelete.Location = new System.Drawing.Point(560, 258);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(113, 45);
            this.buttonDelete.TabIndex = 20;
            this.buttonDelete.Text = "删除选中";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonNew
            // 
            this.buttonNew.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonNew.Location = new System.Drawing.Point(891, 118);
            this.buttonNew.Name = "buttonNew";
            this.buttonNew.Size = new System.Drawing.Size(126, 72);
            this.buttonNew.TabIndex = 19;
            this.buttonNew.Text = "新增记录";
            this.buttonNew.UseVisualStyleBackColor = true;
            this.buttonNew.Click += new System.EventHandler(this.buttonNew_Click);
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(47, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 37);
            this.label6.TabIndex = 18;
            this.label6.Text = "记账管理";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(65, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(352, 24);
            this.label1.TabIndex = 27;
            this.label1.Text = "AI智能添加（建议每次只输入一条记录）：";
            // 
            // textBoxAI
            // 
            this.textBoxAI.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxAI.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxAI.Location = new System.Drawing.Point(69, 136);
            this.textBoxAI.Name = "textBoxAI";
            this.textBoxAI.Size = new System.Drawing.Size(579, 34);
            this.textBoxAI.TabIndex = 28;
            // 
            // buttonAI
            // 
            this.buttonAI.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAI.AutoSize = true;
            this.buttonAI.Location = new System.Drawing.Point(663, 137);
            this.buttonAI.Name = "buttonAI";
            this.buttonAI.Size = new System.Drawing.Size(93, 34);
            this.buttonAI.TabIndex = 29;
            this.buttonAI.Text = "确定";
            this.buttonAI.UseVisualStyleBackColor = true;
            this.buttonAI.Click += new System.EventHandler(this.buttonAI_Click);
            // 
            // RecordListControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.buttonAI);
            this.Controls.Add(this.textBoxAI);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.comboBoxFilter);
            this.Controls.Add(this.panelEditor);
            this.Controls.Add(this.gridRecord);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonNew);
            this.Controls.Add(this.label6);
            this.Name = "RecordListControl";
            this.Size = new System.Drawing.Size(1154, 680);
            this.Load += new System.EventHandler(this.RecordListControl_Load);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panelEditor.ResumeLayout(false);
            this.panelEditor.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecord)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton rdoCTIncome;
        private System.Windows.Forms.RadioButton rdoCTExpense;
        private System.Windows.Forms.RadioButton rdoCTAll;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox comboBoxFilter;
        private System.Windows.Forms.Panel panelEditor;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton rdoIncome;
        private System.Windows.Forms.RadioButton rdoExpense;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.TextBox textBoxNote;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxMoney;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView gridRecord;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonNew;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxAI;
        private System.Windows.Forms.Button buttonAI;
    }
}
