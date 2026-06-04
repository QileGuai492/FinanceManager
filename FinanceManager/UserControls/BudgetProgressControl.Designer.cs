namespace FinanceManager.UserControls
{
    partial class BudgetProgressControl
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
            this.buttonLoadBudget = new System.Windows.Forms.Button();
            this.buttonSaveBudget = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxBudget = new System.Windows.Forms.TextBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.gridSuggest = new System.Windows.Forms.DataGridView();
            this.labelSuggestion = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.gridWarn = new System.Windows.Forms.DataGridView();
            this.label17 = new System.Windows.Forms.Label();
            this.panel8 = new System.Windows.Forms.Panel();
            this.labelPercent = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.labelDailyAvg = new System.Windows.Forms.Label();
            this.labelRemain = new System.Windows.Forms.Label();
            this.progBudget = new System.Windows.Forms.ProgressBar();
            this.labelSpent = new System.Windows.Forms.Label();
            this.labelBudget = new System.Windows.Forms.Label();
            this.comboBoxMonth = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxYear = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.radioButtonYearly = new System.Windows.Forms.RadioButton();
            this.radioButtonMonthly = new System.Windows.Forms.RadioButton();
            this.radioButtonDaily = new System.Windows.Forms.RadioButton();
            this.label14 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSuggest)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridWarn)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonLoadBudget
            // 
            this.buttonLoadBudget.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonLoadBudget.AutoSize = true;
            this.buttonLoadBudget.Location = new System.Drawing.Point(796, 151);
            this.buttonLoadBudget.Name = "buttonLoadBudget";
            this.buttonLoadBudget.Size = new System.Drawing.Size(98, 46);
            this.buttonLoadBudget.TabIndex = 27;
            this.buttonLoadBudget.Text = "加载";
            this.buttonLoadBudget.UseVisualStyleBackColor = true;
            this.buttonLoadBudget.Click += new System.EventHandler(this.buttonLoadBudget_Click);
            // 
            // buttonSaveBudget
            // 
            this.buttonSaveBudget.AutoSize = true;
            this.buttonSaveBudget.Location = new System.Drawing.Point(316, 289);
            this.buttonSaveBudget.Name = "buttonSaveBudget";
            this.buttonSaveBudget.Size = new System.Drawing.Size(102, 30);
            this.buttonSaveBudget.TabIndex = 26;
            this.buttonSaveBudget.Text = "保存预算";
            this.buttonSaveBudget.UseVisualStyleBackColor = true;
            this.buttonSaveBudget.Click += new System.EventHandler(this.buttonSaveBudget_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(50, 297);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(82, 15);
            this.label19.TabIndex = 16;
            this.label19.Text = "修改预算：";
            // 
            // textBoxBudget
            // 
            this.textBoxBudget.Location = new System.Drawing.Point(145, 292);
            this.textBoxBudget.Name = "textBoxBudget";
            this.textBoxBudget.Size = new System.Drawing.Size(100, 25);
            this.textBoxBudget.TabIndex = 18;
            // 
            // panel10
            // 
            this.panel10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel10.Controls.Add(this.gridSuggest);
            this.panel10.Controls.Add(this.buttonSaveBudget);
            this.panel10.Controls.Add(this.labelSuggestion);
            this.panel10.Controls.Add(this.label19);
            this.panel10.Controls.Add(this.textBoxBudget);
            this.panel10.Controls.Add(this.label18);
            this.panel10.Location = new System.Drawing.Point(509, 245);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(486, 341);
            this.panel10.TabIndex = 25;
            // 
            // gridSuggest
            // 
            this.gridSuggest.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSuggest.Location = new System.Drawing.Point(26, 96);
            this.gridSuggest.Name = "gridSuggest";
            this.gridSuggest.RowHeadersWidth = 51;
            this.gridSuggest.RowTemplate.Height = 27;
            this.gridSuggest.Size = new System.Drawing.Size(438, 165);
            this.gridSuggest.TabIndex = 2;
            // 
            // labelSuggestion
            // 
            this.labelSuggestion.Location = new System.Drawing.Point(23, 35);
            this.labelSuggestion.Name = "labelSuggestion";
            this.labelSuggestion.Size = new System.Drawing.Size(395, 42);
            this.labelSuggestion.TabIndex = 1;
            this.labelSuggestion.Text = "建议总预算：￥NaN";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(143, 11);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(188, 15);
            this.label18.TabIndex = 0;
            this.label18.Text = "预算建议（开启AI后可用）";
            // 
            // gridWarn
            // 
            this.gridWarn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridWarn.Location = new System.Drawing.Point(91, 208);
            this.gridWarn.Name = "gridWarn";
            this.gridWarn.RowHeadersWidth = 51;
            this.gridWarn.RowTemplate.Height = 27;
            this.gridWarn.Size = new System.Drawing.Size(202, 111);
            this.gridWarn.TabIndex = 1;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(164, 173);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(67, 15);
            this.label17.TabIndex = 0;
            this.label17.Text = "预警信息";
            // 
            // panel8
            // 
            this.panel8.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel8.Controls.Add(this.gridWarn);
            this.panel8.Controls.Add(this.label17);
            this.panel8.Controls.Add(this.labelPercent);
            this.panel8.Controls.Add(this.label20);
            this.panel8.Controls.Add(this.labelDailyAvg);
            this.panel8.Controls.Add(this.labelRemain);
            this.panel8.Controls.Add(this.progBudget);
            this.panel8.Controls.Add(this.labelSpent);
            this.panel8.Controls.Add(this.labelBudget);
            this.panel8.Location = new System.Drawing.Point(40, 245);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(412, 341);
            this.panel8.TabIndex = 23;
            // 
            // labelPercent
            // 
            this.labelPercent.AutoSize = true;
            this.labelPercent.Location = new System.Drawing.Point(352, 86);
            this.labelPercent.Name = "labelPercent";
            this.labelPercent.Size = new System.Drawing.Size(39, 15);
            this.labelPercent.TabIndex = 14;
            this.labelPercent.Text = "NaN%";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(164, 11);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(67, 15);
            this.label20.TabIndex = 13;
            this.label20.Text = "预算概览";
            // 
            // labelDailyAvg
            // 
            this.labelDailyAvg.AutoSize = true;
            this.labelDailyAvg.Location = new System.Drawing.Point(238, 133);
            this.labelDailyAvg.Name = "labelDailyAvg";
            this.labelDailyAvg.Size = new System.Drawing.Size(121, 15);
            this.labelDailyAvg.TabIndex = 12;
            this.labelDailyAvg.Text = "日均可用：￥NaN";
            // 
            // labelRemain
            // 
            this.labelRemain.AutoSize = true;
            this.labelRemain.Location = new System.Drawing.Point(61, 133);
            this.labelRemain.Name = "labelRemain";
            this.labelRemain.Size = new System.Drawing.Size(91, 15);
            this.labelRemain.TabIndex = 11;
            this.labelRemain.Text = "剩余：￥NaN";
            // 
            // progBudget
            // 
            this.progBudget.Location = new System.Drawing.Point(7, 78);
            this.progBudget.Name = "progBudget";
            this.progBudget.Size = new System.Drawing.Size(337, 23);
            this.progBudget.TabIndex = 10;
            // 
            // labelSpent
            // 
            this.labelSpent.AutoSize = true;
            this.labelSpent.Location = new System.Drawing.Point(238, 35);
            this.labelSpent.Name = "labelSpent";
            this.labelSpent.Size = new System.Drawing.Size(106, 15);
            this.labelSpent.TabIndex = 9;
            this.labelSpent.Text = "已支出：￥NaN";
            // 
            // labelBudget
            // 
            this.labelBudget.AutoSize = true;
            this.labelBudget.Location = new System.Drawing.Point(61, 35);
            this.labelBudget.Name = "labelBudget";
            this.labelBudget.Size = new System.Drawing.Size(91, 15);
            this.labelBudget.TabIndex = 8;
            this.labelBudget.Text = "预算：￥NaN";
            // 
            // comboBoxMonth
            // 
            this.comboBoxMonth.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.comboBoxMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMonth.FormattingEnabled = true;
            this.comboBoxMonth.Location = new System.Drawing.Point(642, 163);
            this.comboBoxMonth.Name = "comboBoxMonth";
            this.comboBoxMonth.Size = new System.Drawing.Size(121, 23);
            this.comboBoxMonth.TabIndex = 22;
            // 
            // label16
            // 
            this.label16.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(584, 167);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(52, 15);
            this.label16.TabIndex = 21;
            this.label16.Text = "月份：";
            // 
            // textBoxYear
            // 
            this.textBoxYear.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxYear.Location = new System.Drawing.Point(471, 161);
            this.textBoxYear.Name = "textBoxYear";
            this.textBoxYear.Size = new System.Drawing.Size(100, 25);
            this.textBoxYear.TabIndex = 20;
            // 
            // label15
            // 
            this.label15.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(416, 169);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 15);
            this.label15.TabIndex = 19;
            this.label15.Text = "年份：";
            // 
            // panel7
            // 
            this.panel7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel7.Controls.Add(this.radioButtonYearly);
            this.panel7.Controls.Add(this.radioButtonMonthly);
            this.panel7.Controls.Add(this.radioButtonDaily);
            this.panel7.Controls.Add(this.label14);
            this.panel7.Location = new System.Drawing.Point(130, 156);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(254, 41);
            this.panel7.TabIndex = 17;
            // 
            // radioButtonYearly
            // 
            this.radioButtonYearly.AutoSize = true;
            this.radioButtonYearly.Location = new System.Drawing.Point(178, 12);
            this.radioButtonYearly.Name = "radioButtonYearly";
            this.radioButtonYearly.Size = new System.Drawing.Size(58, 19);
            this.radioButtonYearly.TabIndex = 2;
            this.radioButtonYearly.TabStop = true;
            this.radioButtonYearly.Text = "年度";
            this.radioButtonYearly.UseVisualStyleBackColor = true;
            this.radioButtonYearly.CheckedChanged += new System.EventHandler(this.BudgetType_CheckedChanged);
            // 
            // radioButtonMonthly
            // 
            this.radioButtonMonthly.AutoSize = true;
            this.radioButtonMonthly.Checked = true;
            this.radioButtonMonthly.Location = new System.Drawing.Point(121, 11);
            this.radioButtonMonthly.Name = "radioButtonMonthly";
            this.radioButtonMonthly.Size = new System.Drawing.Size(58, 19);
            this.radioButtonMonthly.TabIndex = 2;
            this.radioButtonMonthly.TabStop = true;
            this.radioButtonMonthly.Text = "月度";
            this.radioButtonMonthly.UseVisualStyleBackColor = true;
            this.radioButtonMonthly.CheckedChanged += new System.EventHandler(this.BudgetType_CheckedChanged);
            // 
            // radioButtonDaily
            // 
            this.radioButtonDaily.AutoSize = true;
            this.radioButtonDaily.Location = new System.Drawing.Point(59, 11);
            this.radioButtonDaily.Name = "radioButtonDaily";
            this.radioButtonDaily.Size = new System.Drawing.Size(58, 19);
            this.radioButtonDaily.TabIndex = 2;
            this.radioButtonDaily.TabStop = true;
            this.radioButtonDaily.Text = "日度";
            this.radioButtonDaily.UseVisualStyleBackColor = true;
            this.radioButtonDaily.CheckedChanged += new System.EventHandler(this.BudgetType_CheckedChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1, 11);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(52, 15);
            this.label14.TabIndex = 1;
            this.label14.Text = "类型：";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(33, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(129, 37);
            this.label5.TabIndex = 15;
            this.label5.Text = "预算管理";
            // 
            // BudgetProgressControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.buttonLoadBudget);
            this.Controls.Add(this.panel10);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.comboBoxMonth);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.textBoxYear);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.label5);
            this.Name = "BudgetProgressControl";
            this.Size = new System.Drawing.Size(1036, 671);
            this.Load += new System.EventHandler(this.BudgetProgressControl_Load);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSuggest)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridWarn)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonLoadBudget;
        private System.Windows.Forms.Button buttonSaveBudget;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxBudget;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.DataGridView gridSuggest;
        private System.Windows.Forms.Label labelSuggestion;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.DataGridView gridWarn;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Label labelPercent;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label labelDailyAvg;
        private System.Windows.Forms.Label labelRemain;
        private System.Windows.Forms.ProgressBar progBudget;
        private System.Windows.Forms.Label labelSpent;
        private System.Windows.Forms.Label labelBudget;
        private System.Windows.Forms.ComboBox comboBoxMonth;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxYear;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.RadioButton radioButtonYearly;
        private System.Windows.Forms.RadioButton radioButtonMonthly;
        private System.Windows.Forms.RadioButton radioButtonDaily;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label5;
    }
}
