namespace FinanceManager.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panNav = new System.Windows.Forms.Panel();
            this.logo = new System.Windows.Forms.PictureBox();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnStatistics = new System.Windows.Forms.Button();
            this.btnBudget = new System.Windows.Forms.Button();
            this.btnTemplate = new System.Windows.Forms.Button();
            this.btnData = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panContainer = new System.Windows.Forms.Panel();
            this.panelDashBoard = new System.Windows.Forms.Panel();
            this.chartExpense = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartIncome = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel1 = new System.Windows.Forms.Panel();
            this._lblIncomeValue = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this._lblBalanceValue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this._lblExpenseValue = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.panNav.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logo)).BeginInit();
            this.panContainer.SuspendLayout();
            this.panelDashBoard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartExpense)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartIncome)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panNav
            // 
            this.panNav.Controls.Add(this.logo);
            this.panNav.Controls.Add(this.btnDashboard);
            this.panNav.Controls.Add(this.btnRecord);
            this.panNav.Controls.Add(this.btnStatistics);
            this.panNav.Controls.Add(this.btnBudget);
            this.panNav.Controls.Add(this.btnTemplate);
            this.panNav.Controls.Add(this.btnData);
            this.panNav.Controls.Add(this.btnSettings);
            this.panNav.Controls.Add(this.btnLogout);
            this.panNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.panNav.Location = new System.Drawing.Point(0, 0);
            this.panNav.Name = "panNav";
            this.panNav.Size = new System.Drawing.Size(150, 533);
            this.panNav.TabIndex = 0;
            // 
            // logo
            // 
            this.logo.Dock = System.Windows.Forms.DockStyle.Top;
            this.logo.Image = global::FinanceManager.Properties.Resources.logo;
            this.logo.Location = new System.Drawing.Point(0, 0);
            this.logo.Name = "logo";
            this.logo.Size = new System.Drawing.Size(150, 117);
            this.logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logo.TabIndex = 1;
            this.logo.TabStop = false;
            // 
            // btnDashboard
            // 
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Location = new System.Drawing.Point(0, 221);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(150, 39);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "仪表盘";
            this.btnDashboard.UseVisualStyleBackColor = true;
            this.btnDashboard.Click += new System.EventHandler(this.btnDashboard_Click);
            // 
            // btnRecord
            // 
            this.btnRecord.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecord.Location = new System.Drawing.Point(0, 260);
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.Size = new System.Drawing.Size(150, 39);
            this.btnRecord.TabIndex = 0;
            this.btnRecord.Text = "记账";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnStatistics
            // 
            this.btnStatistics.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnStatistics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStatistics.Location = new System.Drawing.Point(0, 299);
            this.btnStatistics.Name = "btnStatistics";
            this.btnStatistics.Size = new System.Drawing.Size(150, 39);
            this.btnStatistics.TabIndex = 0;
            this.btnStatistics.Text = "统计";
            this.btnStatistics.UseVisualStyleBackColor = true;
            this.btnStatistics.Click += new System.EventHandler(this.btnStatistics_Click);
            // 
            // btnBudget
            // 
            this.btnBudget.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnBudget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBudget.Location = new System.Drawing.Point(0, 338);
            this.btnBudget.Name = "btnBudget";
            this.btnBudget.Size = new System.Drawing.Size(150, 39);
            this.btnBudget.TabIndex = 0;
            this.btnBudget.Text = "预算";
            this.btnBudget.UseVisualStyleBackColor = true;
            this.btnBudget.Click += new System.EventHandler(this.btnBudget_Click);
            // 
            // btnTemplate
            // 
            this.btnTemplate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTemplate.Location = new System.Drawing.Point(0, 377);
            this.btnTemplate.Name = "btnTemplate";
            this.btnTemplate.Size = new System.Drawing.Size(150, 39);
            this.btnTemplate.TabIndex = 0;
            this.btnTemplate.Text = "模板";
            this.btnTemplate.UseVisualStyleBackColor = true;
            this.btnTemplate.Click += new System.EventHandler(this.btnTemplate_Click);
            // 
            // btnData
            // 
            this.btnData.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnData.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnData.Location = new System.Drawing.Point(0, 416);
            this.btnData.Name = "btnData";
            this.btnData.Size = new System.Drawing.Size(150, 39);
            this.btnData.TabIndex = 0;
            this.btnData.Text = "数据管理";
            this.btnData.UseVisualStyleBackColor = true;
            this.btnData.Click += new System.EventHandler(this.btnData_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Location = new System.Drawing.Point(0, 455);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(150, 39);
            this.btnSettings.TabIndex = 0;
            this.btnSettings.Text = "设置";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.ForeColor = System.Drawing.Color.Tomato;
            this.btnLogout.Location = new System.Drawing.Point(0, 494);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(150, 39);
            this.btnLogout.TabIndex = 0;
            this.btnLogout.Text = "退出登录";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panContainer
            // 
            this.panContainer.Controls.Add(this.panelDashBoard);
            this.panContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panContainer.Location = new System.Drawing.Point(150, 0);
            this.panContainer.Name = "panContainer";
            this.panContainer.Size = new System.Drawing.Size(879, 533);
            this.panContainer.TabIndex = 1;
            // 
            // panelDashBoard
            // 
            this.panelDashBoard.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelDashBoard.Controls.Add(this.chartExpense);
            this.panelDashBoard.Controls.Add(this.chartIncome);
            this.panelDashBoard.Controls.Add(this.panel1);
            this.panelDashBoard.Controls.Add(this.panel3);
            this.panelDashBoard.Controls.Add(this.panel2);
            this.panelDashBoard.Controls.Add(this.labelDate);
            this.panelDashBoard.Controls.Add(this.labelWelcome);
            this.panelDashBoard.Location = new System.Drawing.Point(48, -20);
            this.panelDashBoard.Name = "panelDashBoard";
            this.panelDashBoard.Size = new System.Drawing.Size(819, 568);
            this.panelDashBoard.TabIndex = 0;
            // 
            // chartExpense
            // 
            this.chartExpense.Anchor = System.Windows.Forms.AnchorStyles.None;
            chartArea1.Name = "ChartArea1";
            this.chartExpense.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartExpense.Legends.Add(legend1);
            this.chartExpense.Location = new System.Drawing.Point(412, 308);
            this.chartExpense.Name = "chartExpense";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartExpense.Series.Add(series1);
            this.chartExpense.Size = new System.Drawing.Size(195, 196);
            this.chartExpense.TabIndex = 6;
            this.chartExpense.Text = "chart1";
            // 
            // chartIncome
            // 
            this.chartIncome.Anchor = System.Windows.Forms.AnchorStyles.None;
            chartArea2.Name = "ChartArea1";
            this.chartIncome.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartIncome.Legends.Add(legend2);
            this.chartIncome.Location = new System.Drawing.Point(150, 308);
            this.chartIncome.Name = "chartIncome";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartIncome.Series.Add(series2);
            this.chartIncome.Size = new System.Drawing.Size(195, 196);
            this.chartIncome.TabIndex = 6;
            this.chartIncome.Text = "chart1";
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.Controls.Add(this._lblIncomeValue);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(30, 133);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(248, 120);
            this.panel1.TabIndex = 5;
            // 
            // _lblIncomeValue
            // 
            this._lblIncomeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._lblIncomeValue.AutoSize = true;
            this._lblIncomeValue.Location = new System.Drawing.Point(28, 41);
            this._lblIncomeValue.Name = "_lblIncomeValue";
            this._lblIncomeValue.Size = new System.Drawing.Size(52, 15);
            this._lblIncomeValue.TabIndex = 0;
            this._lblIncomeValue.Text = "收入值";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "本月收入";
            // 
            // panel3
            // 
            this.panel3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel3.Controls.Add(this._lblBalanceValue);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(528, 133);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(262, 120);
            this.panel3.TabIndex = 2;
            // 
            // _lblBalanceValue
            // 
            this._lblBalanceValue.AutoSize = true;
            this._lblBalanceValue.Location = new System.Drawing.Point(28, 41);
            this._lblBalanceValue.Name = "_lblBalanceValue";
            this._lblBalanceValue.Size = new System.Drawing.Size(52, 15);
            this._lblBalanceValue.TabIndex = 0;
            this._lblBalanceValue.Text = "结余值";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "本月结余";
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.Controls.Add(this._lblExpenseValue);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(284, 133);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(238, 120);
            this.panel2.TabIndex = 2;
            // 
            // _lblExpenseValue
            // 
            this._lblExpenseValue.AutoSize = true;
            this._lblExpenseValue.Location = new System.Drawing.Point(31, 41);
            this._lblExpenseValue.Name = "_lblExpenseValue";
            this._lblExpenseValue.Size = new System.Drawing.Size(52, 15);
            this._lblExpenseValue.TabIndex = 0;
            this._lblExpenseValue.Text = "支出值";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(48, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "本月支出";
            // 
            // labelDate
            // 
            this.labelDate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelDate.AutoSize = true;
            this.labelDate.Location = new System.Drawing.Point(49, 74);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(121, 15);
            this.labelDate.TabIndex = 1;
            this.labelDate.Text = "今天是 年 月 日";
            // 
            // labelWelcome
            // 
            this.labelWelcome.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.Location = new System.Drawing.Point(27, 26);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(143, 15);
            this.labelWelcome.TabIndex = 0;
            this.labelWelcome.Text = "欢迎回来，{用户名}";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1029, 533);
            this.Controls.Add(this.panContainer);
            this.Controls.Add(this.panNav);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panNav.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logo)).EndInit();
            this.panContainer.ResumeLayout(false);
            this.panelDashBoard.ResumeLayout(false);
            this.panelDashBoard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartExpense)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartIncome)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panNav;
        private System.Windows.Forms.Panel panContainer;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnStatistics;
        private System.Windows.Forms.Button btnBudget;
        private System.Windows.Forms.Button btnTemplate;
        private System.Windows.Forms.Button btnData;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.PictureBox logo;
        private System.Windows.Forms.Panel panelDashBoard;
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Label labelDate;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label _lblExpenseValue;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label _lblBalanceValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label _lblIncomeValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartExpense;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartIncome;
    }
}