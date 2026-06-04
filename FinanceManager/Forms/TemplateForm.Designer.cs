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
            this.checkBoxReadOften.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxReadOften.Location = new System.Drawing.Point(157, 37);
            this.checkBoxReadOften.Name = "checkBoxReadOften";
            this.checkBoxReadOften.Size = new System.Drawing.Size(124, 27);
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
            this.gridTemplate.Location = new System.Drawing.Point(131, 108);
            this.gridTemplate.Name = "gridTemplate";
            this.gridTemplate.ReadOnly = true;
            this.gridTemplate.RowHeadersWidth = 51;
            this.gridTemplate.RowTemplate.Height = 27;
            this.gridTemplate.Size = new System.Drawing.Size(593, 225);
            this.gridTemplate.TabIndex = 1;
            this.gridTemplate.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridTemplate_CellDoubleClick);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonAdd.AutoSize = true;
            this.buttonAdd.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonAdd.Location = new System.Drawing.Point(222, 391);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(112, 39);
            this.buttonAdd.TabIndex = 2;
            this.buttonAdd.Text = "新增模板";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonUse
            // 
            this.buttonUse.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonUse.AutoSize = true;
            this.buttonUse.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonUse.Location = new System.Drawing.Point(358, 391);
            this.buttonUse.Name = "buttonUse";
            this.buttonUse.Size = new System.Drawing.Size(112, 39);
            this.buttonUse.TabIndex = 3;
            this.buttonUse.Text = "使用模板";
            this.buttonUse.UseVisualStyleBackColor = true;
            this.buttonUse.Click += new System.EventHandler(this.buttonUse_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonDelete.AutoSize = true;
            this.buttonDelete.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonDelete.Location = new System.Drawing.Point(500, 391);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(112, 39);
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
            this.panelEditor.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.panelEditor.Location = new System.Drawing.Point(124, 456);
            this.panelEditor.Name = "panelEditor";
            this.panelEditor.Size = new System.Drawing.Size(622, 297);
            this.panelEditor.TabIndex = 5;
            // 
            // checkBoxOften
            // 
            this.checkBoxOften.AutoSize = true;
            this.checkBoxOften.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxOften.Location = new System.Drawing.Point(98, 132);
            this.checkBoxOften.Name = "checkBoxOften";
            this.checkBoxOften.Size = new System.Drawing.Size(124, 27);
            this.checkBoxOften.TabIndex = 17;
            this.checkBoxOften.Text = "设为常用";
            this.checkBoxOften.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.AutoSize = true;
            this.buttonCancel.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonCancel.Location = new System.Drawing.Point(376, 240);
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
            this.buttonSave.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSave.Location = new System.Drawing.Point(159, 240);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(94, 37);
            this.buttonSave.TabIndex = 15;
            this.buttonSave.Text = "保存";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // textBoxCategory
            // 
            this.textBoxCategory.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxCategory.Location = new System.Drawing.Point(412, 129);
            this.textBoxCategory.Name = "textBoxCategory";
            this.textBoxCategory.Size = new System.Drawing.Size(149, 34);
            this.textBoxCategory.TabIndex = 14;
            this.textBoxCategory.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxCategory_KeyDown);
            // 
            // buttonAddCategory
            // 
            this.buttonAddCategory.Location = new System.Drawing.Point(558, 75);
            this.buttonAddCategory.Name = "buttonAddCategory";
            this.buttonAddCategory.Size = new System.Drawing.Size(42, 33);
            this.buttonAddCategory.TabIndex = 13;
            this.buttonAddCategory.Text = "+";
            this.buttonAddCategory.UseVisualStyleBackColor = true;
            this.buttonAddCategory.Click += new System.EventHandler(this.buttonAddCategory_Click);
            // 
            // textBoxNote
            // 
            this.textBoxNote.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxNote.Location = new System.Drawing.Point(98, 180);
            this.textBoxNote.Name = "textBoxNote";
            this.textBoxNote.Size = new System.Drawing.Size(463, 34);
            this.textBoxNote.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(29, 183);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 23);
            this.label5.TabIndex = 11;
            this.label5.Text = "备注：";
            // 
            // comboBoxCategory
            // 
            this.comboBoxCategory.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxCategory.FormattingEnabled = true;
            this.comboBoxCategory.Location = new System.Drawing.Point(412, 75);
            this.comboBoxCategory.Name = "comboBoxCategory";
            this.comboBoxCategory.Size = new System.Drawing.Size(127, 31);
            this.comboBoxCategory.TabIndex = 9;
            this.comboBoxCategory.SelectedIndexChanged += new System.EventHandler(this.comboBoxCategory_SelectedIndexChanged);
            // 
            // labelCat2
            // 
            this.labelCat2.AutoSize = true;
            this.labelCat2.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCat2.Location = new System.Drawing.Point(258, 132);
            this.labelCat2.Name = "labelCat2";
            this.labelCat2.Size = new System.Drawing.Size(148, 23);
            this.labelCat2.TabIndex = 8;
            this.labelCat2.Text = "自定义分类：";
            // 
            // labelCat1
            // 
            this.labelCat1.AutoSize = true;
            this.labelCat1.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelCat1.Location = new System.Drawing.Point(315, 83);
            this.labelCat1.Name = "labelCat1";
            this.labelCat1.Size = new System.Drawing.Size(91, 23);
            this.labelCat1.TabIndex = 8;
            this.labelCat1.Text = "分类*：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonExpense);
            this.panel1.Controls.Add(this.radioButtonIncome);
            this.panel1.Location = new System.Drawing.Point(81, 75);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(199, 36);
            this.panel1.TabIndex = 7;
            // 
            // radioButtonExpense
            // 
            this.radioButtonExpense.AutoSize = true;
            this.radioButtonExpense.Checked = true;
            this.radioButtonExpense.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButtonExpense.Location = new System.Drawing.Point(17, 4);
            this.radioButtonExpense.Name = "radioButtonExpense";
            this.radioButtonExpense.Size = new System.Drawing.Size(77, 27);
            this.radioButtonExpense.TabIndex = 5;
            this.radioButtonExpense.TabStop = true;
            this.radioButtonExpense.Text = "支出";
            this.radioButtonExpense.UseVisualStyleBackColor = true;
            this.radioButtonExpense.CheckedChanged += new System.EventHandler(this.radioButtonExpense_CheckedChanged);
            // 
            // radioButtonIncome
            // 
            this.radioButtonIncome.AutoSize = true;
            this.radioButtonIncome.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButtonIncome.Location = new System.Drawing.Point(119, 5);
            this.radioButtonIncome.Name = "radioButtonIncome";
            this.radioButtonIncome.Size = new System.Drawing.Size(77, 27);
            this.radioButtonIncome.TabIndex = 6;
            this.radioButtonIncome.Text = "收入";
            this.radioButtonIncome.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(3, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "类型*：";
            // 
            // textBoxMoney
            // 
            this.textBoxMoney.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxMoney.Location = new System.Drawing.Point(409, 20);
            this.textBoxMoney.Name = "textBoxMoney";
            this.textBoxMoney.Size = new System.Drawing.Size(152, 34);
            this.textBoxMoney.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(315, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "金额：";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(142, 20);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(138, 34);
            this.textBoxName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(29, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "名称*：";
            // 
            // labelTplCount
            // 
            this.labelTplCount.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelTplCount.AutoSize = true;
            this.labelTplCount.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelTplCount.Location = new System.Drawing.Point(487, 41);
            this.labelTplCount.Name = "labelTplCount";
            this.labelTplCount.Size = new System.Drawing.Size(209, 23);
            this.labelTplCount.TabIndex = 6;
            this.labelTplCount.Text = "模板数量：NaN/Max";
            // 
            // TemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 782);
            this.Controls.Add(this.labelTplCount);
            this.Controls.Add(this.panelEditor);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonUse);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.gridTemplate);
            this.Controls.Add(this.checkBoxReadOften);
            this.MaximizeBox = false;
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