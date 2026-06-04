namespace FinanceManager.Forms
{
    partial class LoginForm
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
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panelLogin = new System.Windows.Forms.Panel();
            this.linkLabelSignup = new System.Windows.Forms.LinkLabel();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBoxRemember = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.labelerror = new System.Windows.Forms.Label();
            this.panelContainer.SuspendLayout();
            this.panelLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainer
            // 
            this.panelContainer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panelContainer.Controls.Add(this.panelLogin);
            this.panelContainer.Location = new System.Drawing.Point(119, 85);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(341, 450);
            this.panelContainer.TabIndex = 3;
            // 
            // panelLogin
            // 
            this.panelLogin.Controls.Add(this.linkLabelSignup);
            this.panelLogin.Controls.Add(this.label4);
            this.panelLogin.Controls.Add(this.buttonLogin);
            this.panelLogin.Controls.Add(this.label3);
            this.panelLogin.Controls.Add(this.checkBoxRemember);
            this.panelLogin.Controls.Add(this.textBox1);
            this.panelLogin.Controls.Add(this.label2);
            this.panelLogin.Controls.Add(this.textBoxUserName);
            this.panelLogin.Controls.Add(this.label1);
            this.panelLogin.Location = new System.Drawing.Point(22, 18);
            this.panelLogin.Name = "panelLogin";
            this.panelLogin.Size = new System.Drawing.Size(291, 312);
            this.panelLogin.TabIndex = 0;
            // 
            // linkLabelSignup
            // 
            this.linkLabelSignup.AutoSize = true;
            this.linkLabelSignup.Location = new System.Drawing.Point(149, 270);
            this.linkLabelSignup.Name = "linkLabelSignup";
            this.linkLabelSignup.Size = new System.Drawing.Size(67, 15);
            this.linkLabelSignup.TabIndex = 8;
            this.linkLabelSignup.TabStop = true;
            this.linkLabelSignup.Text = "点此注册";
            this.linkLabelSignup.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelSignup_LinkClicked);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 270);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "没有账号？→";
            // 
            // buttonLogin
            // 
            this.buttonLogin.Location = new System.Drawing.Point(65, 192);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(115, 58);
            this.buttonLogin.TabIndex = 6;
            this.buttonLogin.Text = "登录";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(118, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "下次自动登录";
            // 
            // checkBoxRemember
            // 
            this.checkBoxRemember.AutoSize = true;
            this.checkBoxRemember.Location = new System.Drawing.Point(24, 156);
            this.checkBoxRemember.Name = "checkBoxRemember";
            this.checkBoxRemember.Size = new System.Drawing.Size(74, 19);
            this.checkBoxRemember.TabIndex = 4;
            this.checkBoxRemember.Text = "记住我";
            this.checkBoxRemember.UseVisualStyleBackColor = true;
            this.checkBoxRemember.CheckedChanged += new System.EventHandler(this.checkBoxRemember_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(89, 91);
            this.textBox1.Name = "textBox1";
            this.textBox1.PasswordChar = '*';
            this.textBox1.Size = new System.Drawing.Size(153, 25);
            this.textBox1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "密码：";
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Location = new System.Drawing.Point(89, 33);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(153, 25);
            this.textBoxUserName.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名：";
            // 
            // Title
            // 
            this.Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Title.Location = new System.Drawing.Point(163, 41);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(194, 23);
            this.Title.TabIndex = 2;
            this.Title.Text = "欢迎回来，请登录";
            // 
            // labelerror
            // 
            this.labelerror.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelerror.AutoSize = true;
            this.labelerror.ForeColor = System.Drawing.Color.Red;
            this.labelerror.Location = new System.Drawing.Point(233, 538);
            this.labelerror.Name = "labelerror";
            this.labelerror.Size = new System.Drawing.Size(67, 15);
            this.labelerror.TabIndex = 4;
            this.labelerror.Text = "错误信息";
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 596);
            this.Controls.Add(this.labelerror);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.Title);
            this.MaximizeBox = false;
            this.Name = "LoginForm";
            this.Text = "AI个人财务管理系统 - 登录";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panelContainer.ResumeLayout(false);
            this.panelLogin.ResumeLayout(false);
            this.panelLogin.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelContainer;
        private System.Windows.Forms.Panel panelLogin;
        private System.Windows.Forms.LinkLabel linkLabelSignup;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBoxRemember;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Label labelerror;
    }
}