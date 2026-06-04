namespace FinanceManager.Forms
{
    partial class TemplateForm
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
            this.checkBoxReadOften = new System.Windows.Forms.CheckBox();
            this.gridTemplate = new System.Windows.Forms.DataGridView();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonUse = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.panelEditor = new System.Windows.Forms.Panel();
            this.checkBoxOften = new System.Windows.Forms.CheckBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.textBoxCategory = new System.Windows.Forms.TextBox();
            this.buttonAddCategory = new System.Windows.Forms.Button();
            this.textBoxNote = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxCategory = new System.Windows.Forms.ComboBox();
            this.labelCat2 = new System.Windows.Forms.Label();
            this.labelCat1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonExpense = new System.Windows.Forms.RadioButton();
            this.radioButtonIncome = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxMoney = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.labelTplCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gridTemplate)).BeginInit();
            this.panelEditor.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxReadOften
            // 
            this.checkBoxReadOften.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.checkBoxReadOften.AutoSize = true;
            this.checkBoxReadOften.Location = new System.Drawing.Point(143, 54);
            this.checkBoxReadOften.Name = "checkBoxReadOften";
            this.checkBoxReadOften.Size = new System.Drawing.Size(89, 19);
            this.checkBoxReadOften.TabIndex = 0;
            this.checkBoxReadOften.Text = "只看常用";
            this.checkBoxReadOften.UseVisualStyleBackColor = true;
            this.checkBoxReadOften.CheckedChanged += new System.EventHandler(this.checkBoxReadOften_CheckedChanged);
            // 
            // gridTemplate
            // 
            this.gridTemplate.AllowUserToAddRows = false;
            this.gridTemplate.AllowUserToDeleteRows = false;
            this.gridTemplate.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.gridTemplate.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTemplate.Location = new System.Drawing.Point(129, 103);
            this.gridTemplate.Name = "gridTemplate";
            this.gridTemplate.ReadOnly = true;
            this.gridTemplate.RowHeadersWidth = 51;
            this.gridTemplate.RowTemplate.Height = 27;
            this.gridTemplate.Size = new System.Drawing.Size(472, 187);
            this.gridTemplate.TabIndex = 1;
            this.gridTemplate.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTemplate_CellDoubleClick);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAdd.AutoSize = true;
            this.buttonAdd.Location = new System.Drawing.Point(176, 334);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(94, 39);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "新增模板";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUse
            // 
            this.buttonUse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonUse.AutoSize = true;
            this.buttonUse.Location = new System.Drawing.Point(312, 334);
            this.buttonUse.Name = "buttonUse";
            this.buttonUse.Size = new System.Drawing.Size(94, 39);
            this.buttonUse.TabIndex = 3;
            this.buttonUse.Text = "使用模板";
            this.buttonUse.UseVisualStyleBackColor = true;
            this.buttonUse.Click += new System.EventHandler(this.buttonUse_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonDelete.AutoSize = true;
            this.buttonDelete.Location = new System.Drawing.Point(454, 334);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(94, 39);
            this.buttonDelete.TabIndex = 4;
            this.buttonDelete.Text = "删除选中";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // panelEditor
            // 
            this.panelEditor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelEditor.Controls.Add(this.checkBoxOften);
            this.panelEditor.Controls.Add(this.buttonCancel);
            this.panelEditor.Controls.Add(this.buttonSave);
            this.panelEditor.Controls.Add(this.textBoxCategory);
            this.panelEditor.Controls.Add(this.buttonAddCategory);
            this.panelEditor.Controls.Add(this.textBoxNote);
            this.panelEditor.Controls.Add(this.label5);
            this.panelEditor.Controls.Add(this.comboBoxCategory);
            this.panelEditor.Controls.Add(this.labelCat2);
            this.panelEditor.Controls.Add(this.labelCat1);
            this.panelEditor.Controls.Add(this.panel1);
            this.panelEditor.Controls.Add(this.label3);
            this.panelEditor.Controls.Add(this.textBoxMoney);
            this.panelEditor.Controls.Add(this.label2);
            this.panelEditor.Controls.Add(this.textBoxName);
            this.panelEditor.Controls.Add(this.label1);
            this.panelEditor.Location = new System.Drawing.Point(129, 399);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(479, 243);
            this.panelEditor.TabIndex = 5;
            // 
            // checkBoxOften
            // 
            this.checkBoxOften.AutoSize = true;
            this.checkBoxOften.Location = new System.Drawing.Point(47, 163);
            this.checkBoxOften.Name = "checkBoxOften";
            this.checkBoxOften.Size = new System.Drawing.Size(89, 19);
            this.checkBoxOften.TabIndex = 17;
            this.checkBoxOften.Text = "设为常用";
            this.checkBoxOften.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.Location = new System.Drawing.Point(269, 188);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(94, 37);
            this.buttonCancel.TabIndex = 16;
            this.buttonCancel.Text = "取消";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.AutoSize = true;
            this.buttonSave.Location = new System.Drawing.Point(112, 188);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(94, 37);
            this.buttonSave.TabIndex = 15;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxCategory
            // 
            this.textBoxCategory.Location = new System.Drawing.Point(319, 129);
            this.textBoxCategory.Name = "textBoxCategory";
            this.textBoxCategory.Size = new System.Drawing.Size(100, 25);
            this.textBoxCategory.TabIndex = 14;
            this.textBoxCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxCategory_KeyDown);
            // 
            // buttonAddCategory
            // 
            this.buttonAddCategory.Location = new System.Drawing.Point(410, 78);
            this.buttonAddCategory.Name = "buttonAddCategory";
            this.buttonAddCategory.Size = new System.Drawing.Size(31, 23);
            this.buttonAddCategory.TabIndex = 13;
            this.buttonAddCategory.Text = "+";
            this.buttonAddCategory.UseVisualStyleBackColor = true;
            this.buttonAddCategory.Click += new System.EventHandler(this.buttonAddCategory_Click);
            // 
            // textBoxNote
            // 
            this.textBoxNote.Location = new System.Drawing.Point(82, 126);
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(117, 25);
            this.textBoxNote.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "备注：";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(283, 78);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(121, 23);
            this.comboBoxCategory.TabIndex = 9;
            this.comboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.comboBoxCategory_SelectedIndexChanged);
            // 
            // labelCat2
            // 
            this.labelCat2.AutoSize = true;
            this.labelCat2.Location = new System.Drawing.Point(225, 132);
            this.labelCat2.Name = "labelCat2";
            this.labelCat2.Size = new System.Drawing.Size(97, 15);
            this.labelCat2.TabIndex = 8;
            this.labelCat2.Text = "自定义分类：";
            // 
            // labelCat1
            // 
            this.labelCat1.AutoSize = true;
            this.labelCat1.Location = new System.Drawing.Point(225, 83);
            this.labelCat1.Name = "labelCat1";
            this.labelCat1.Size = new System.Drawing.Size(60, 15);
            this.labelCat1.TabIndex = 8;
            this.labelCat1.Text = "分类*：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonExpense);
            this.panel1.Controls.Add(this.radioButtonIncome);
            this.panel1.Location = new System.Drawing.Point(81, 75);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(138, 36);
            this.panel1.TabIndex = 7;
            // 
            // radioButtonExpense
            // 
            this.radioButtonExpense.AutoSize = true;
            this.radioButtonExpense.Checked = true;
            this.radioButtonExpense.Location = new System.Drawing.Point(7, 6);
            this.radioButtonExpense.Name = "radioButtonExpense";
            this.radioButtonExpense.Size = new System.Drawing.Size(58, 19);
            this.radioButtonExpense.TabIndex = 5;
            this.radioButtonExpense.TabStop = true;
            this.radioButtonExpense.Text = "支出";
            this.radioButtonExpense.UseVisualStyleBackColor = true;
            this.radioButtonExpense.CheckedChanged += new System.EventHandler(this.radioButtonExpense_CheckedChanged);
            // 
            // radioButtonIncome
            // 
            this.radioButtonIncome.AutoSize = true;
            this.radioButtonIncome.Location = new System.Drawing.Point(71, 5);
            this.radioButtonIncome.Name = "radioButtonIncome";
            this.radioButtonIncome.Size = new System.Drawing.Size(58, 19);
            this.radioButtonIncome.TabIndex = 6;
            this.radioButtonIncome.Text = "收入";
            this.radioButtonIncome.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "类型*：";
            // 
            // textBoxMoney
            // 
            this.textBoxMoney.Location = new System.Drawing.Point(304, 20);
            this.textBoxMoney.Name = "textBoxMoney";
            this.textBoxMoney.Size = new System.Drawing.Size(115, 25);
            this.textBoxMoney.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(225, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "金额：";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(81, 20);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(118, 25);
            this.textBoxName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称*：";
            // 
            // labelTplCount
            // 
            this.labelTplCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTplCount.AutoSize = true;
            this.labelTplCount.Location = new System.Drawing.Point(395, 58);
            this.labelTplCount.Name = "labelTplCount";
            this.labelTplCount.Size = new System.Drawing.Size(138, 15);
            this.labelTplCount.TabIndex = 6;
            this.labelTplCount.Text = "模板数量：NaN/Max";
            // 
            // TemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 669);
            this.Controls.Add(this.labelTplCount);
            this.Controls.Add(this.panelEditor);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUse);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.gridTemplate);
            this.Controls.Add(this.checkBoxReadOften);
            this.Name = "TemplateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模板管理";
            this.Load += new System.EventHandler(this.TemplateForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridTemplate)).EndInit();
            this.panelEditor.ResumeLayout(false);
            this.panelEditor.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxReadOften;
        private System.Windows.Forms.DataGridView gridTemplate;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonUse;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Panel panelEditor;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxCategory;
        private System.Windows.Forms.Label labelCat1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonExpense;
        private System.Windows.Forms.RadioButton radioButtonIncome;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxMoney;
        private System.Windows.Forms.TextBox textBoxNote;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxCategory;
        private System.Windows.Forms.Button buttonAddCategory;
        private System.Windows.Forms.Label labelCat2;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.CheckBox checkBoxOften;
        private System.Windows.Forms.Label labelTplCount;
    }
}