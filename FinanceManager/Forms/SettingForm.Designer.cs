namespace FinanceManager.Forms
{
    partial class SettingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxUsername = new System.Windows.Forms.TextBox();
            this.buttonSaveName = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.linkLabelDeepSeek = new System.Windows.Forms.LinkLabel();
            this.buttonAISettings = new System.Windows.Forms.Button();
            this.textBoxAIModel = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBoxAPIKey = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxEndPoint = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxAI = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.labelStatus = new System.Windows.Forms.Label();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.textBoxCheck = new System.Windows.Forms.TextBox();
            this.textBoxNew = new System.Windows.Forms.TextBox();
            this.textBoxOrgin = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(104, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名：";
            // 
            // textBoxUsername
            // 
            this.textBoxUsername.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxUsername.Location = new System.Drawing.Point(181, 31);
            this.textBoxUsername.Name = "textBoxUsername";
            this.textBoxUsername.Size = new System.Drawing.Size(203, 25);
            this.textBoxUsername.TabIndex = 1;
            // 
            // buttonSaveName
            // 
            this.buttonSaveName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSaveName.AutoSize = true;
            this.buttonSaveName.Location = new System.Drawing.Point(413, 31);
            this.buttonSaveName.Name = "buttonSaveName";
            this.buttonSaveName.Size = new System.Drawing.Size(75, 25);
            this.buttonSaveName.TabIndex = 2;
            this.buttonSaveName.Text = "保存";
            this.buttonSaveName.UseVisualStyleBackColor = true;
            this.buttonSaveName.Click += new System.EventHandler(this.buttonSaveName_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.linkLabelDeepSeek);
            this.panel1.Controls.Add(this.buttonAISettings);
            this.panel1.Controls.Add(this.textBoxAIModel);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.textBoxAPIKey);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.textBoxEndPoint);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.checkBoxAI);
            this.panel1.Location = new System.Drawing.Point(84, 355);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(404, 306);
            this.panel1.TabIndex = 3;
            // 
            // linkLabelDeepSeek
            // 
            this.linkLabelDeepSeek.AutoSize = true;
            this.linkLabelDeepSeek.Location = new System.Drawing.Point(9, 279);
            this.linkLabelDeepSeek.Name = "linkLabelDeepSeek";
            this.linkLabelDeepSeek.Size = new System.Drawing.Size(371, 15);
            this.linkLabelDeepSeek.TabIndex = 10;
            this.linkLabelDeepSeek.TabStop = true;
            this.linkLabelDeepSeek.Text = "此处仅以Deepseek作为参考，详情请查看官方接口文档";
            this.linkLabelDeepSeek.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelDeepSeek_LinkClicked);
            // 
            // buttonAISettings
            // 
            this.buttonAISettings.AutoSize = true;
            this.buttonAISettings.Location = new System.Drawing.Point(142, 240);
            this.buttonAISettings.Name = "buttonAISettings";
            this.buttonAISettings.Size = new System.Drawing.Size(77, 25);
            this.buttonAISettings.TabIndex = 9;
            this.buttonAISettings.Text = "保存配置";
            this.buttonAISettings.UseVisualStyleBackColor = true;
            this.buttonAISettings.Click += new System.EventHandler(this.buttonAISettings_Click);
            // 
            // textBoxAIModel
            // 
            this.textBoxAIModel.Location = new System.Drawing.Point(93, 199);
            this.textBoxAIModel.Name = "textBoxAIModel";
            this.textBoxAIModel.Size = new System.Drawing.Size(254, 25);
            this.textBoxAIModel.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(25, 204);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(62, 15);
            this.label9.TabIndex = 7;
            this.label9.Text = "Model：";
            // 
            // textBoxAPIKey
            // 
            this.textBoxAPIKey.Location = new System.Drawing.Point(94, 149);
            this.textBoxAPIKey.Name = "textBoxAPIKey";
            this.textBoxAPIKey.PasswordChar = '·';
            this.textBoxAPIKey.Size = new System.Drawing.Size(253, 25);
            this.textBoxAPIKey.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 149);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(78, 15);
            this.label8.TabIndex = 5;
            this.label8.Text = "API KEY：";
            // 
            // textBoxEndPoint
            // 
            this.textBoxEndPoint.Location = new System.Drawing.Point(93, 97);
            this.textBoxEndPoint.Name = "textBoxEndPoint";
            this.textBoxEndPoint.Size = new System.Drawing.Size(254, 25);
            this.textBoxEndPoint.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "EndPoint：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(265, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "进行消费分析 / 异常提醒 / 预算建议";
            // 
            // checkBoxAI
            // 
            this.checkBoxAI.AutoSize = true;
            this.checkBoxAI.Location = new System.Drawing.Point(8, 21);
            this.checkBoxAI.Name = "checkBoxAI";
            this.checkBoxAI.Size = new System.Drawing.Size(105, 19);
            this.checkBoxAI.TabIndex = 0;
            this.checkBoxAI.Text = "开启AI建议";
            this.checkBoxAI.UseVisualStyleBackColor = true;
            this.checkBoxAI.CheckedChanged += new System.EventHandler(this.checkBoxAI_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.labelStatus);
            this.panel2.Controls.Add(this.buttonCheck);
            this.panel2.Controls.Add(this.textBoxCheck);
            this.panel2.Controls.Add(this.textBoxNew);
            this.panel2.Controls.Add(this.textBoxOrgin);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Location = new System.Drawing.Point(84, 82);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(404, 257);
            this.panel2.TabIndex = 4;
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.ForeColor = System.Drawing.Color.Red;
            this.labelStatus.Location = new System.Drawing.Point(152, 182);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(67, 15);
            this.labelStatus.TabIndex = 6;
            this.labelStatus.Text = "状态提示";
            // 
            // buttonCheck
            // 
            this.buttonCheck.AutoSize = true;
            this.buttonCheck.Location = new System.Drawing.Point(155, 210);
            this.buttonCheck.Name = "buttonCheck";
            this.buttonCheck.Size = new System.Drawing.Size(77, 25);
            this.buttonCheck.TabIndex = 5;
            this.buttonCheck.Text = "确认修改";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.buttonCheck_Click);
            // 
            // textBoxCheck
            // 
            this.textBoxCheck.Location = new System.Drawing.Point(95, 145);
            this.textBoxCheck.Name = "textBoxCheck";
            this.textBoxCheck.PasswordChar = '*';
            this.textBoxCheck.Size = new System.Drawing.Size(252, 25);
            this.textBoxCheck.TabIndex = 4;
            // 
            // textBoxNew
            // 
            this.textBoxNew.Location = new System.Drawing.Point(95, 101);
            this.textBoxNew.Name = "textBoxNew";
            this.textBoxNew.PasswordChar = '*';
            this.textBoxNew.Size = new System.Drawing.Size(252, 25);
            this.textBoxNew.TabIndex = 4;
            // 
            // textBoxOrgin
            // 
            this.textBoxOrgin.Location = new System.Drawing.Point(96, 52);
            this.textBoxOrgin.Name = "textBoxOrgin";
            this.textBoxOrgin.PasswordChar = '*';
            this.textBoxOrgin.Size = new System.Drawing.Size(251, 25);
            this.textBoxOrgin.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 143);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 3;
            this.label7.Text = "确认密码：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 15);
            this.label6.TabIndex = 2;
            this.label6.Text = "新密码：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 15);
            this.label5.TabIndex = 1;
            this.label5.Text = "原密码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 14);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 15);
            this.label4.TabIndex = 0;
            this.label4.Text = "修改密码";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(633, 691);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonSaveName);
            this.Controls.Add(this.textBoxUsername);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "个人设置";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxUsername;
        private System.Windows.Forms.Button buttonSaveName;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxAI;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.TextBox textBoxCheck;
        private System.Windows.Forms.TextBox textBoxNew;
        private System.Windows.Forms.TextBox textBoxOrgin;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Button buttonAISettings;
        private System.Windows.Forms.TextBox textBoxAIModel;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxAPIKey;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxEndPoint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabelDeepSeek;
    }
}