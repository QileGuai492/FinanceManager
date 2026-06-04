namespace FinanceManager.UserControls
{
    partial class RegisterControl
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
            this.label9 = new System.Windows.Forms.Label();
            this.SignInlinkLabel = new System.Windows.Forms.LinkLabel();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.SignUp = new System.Windows.Forms.Button();
            this.emailtextBox = new System.Windows.Forms.TextBox();
            this.checkpasswordtextBox = new System.Windows.Forms.TextBox();
            this.passwordtextBox2 = new System.Windows.Forms.TextBox();
            this.usernametextBox2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(45, 170);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(249, 15);
            this.label9.TabIndex = 25;
            this.label9.Text = "规则：6-20字符，需包含字母和数字";
            // 
            // SignInlinkLabel
            // 
            this.SignInlinkLabel.AutoSize = true;
            this.SignInlinkLabel.BackColor = System.Drawing.Color.Transparent;
            this.SignInlinkLabel.Location = new System.Drawing.Point(191, 369);
            this.SignInlinkLabel.Name = "SignInlinkLabel";
            this.SignInlinkLabel.Size = new System.Drawing.Size(67, 15);
            this.SignInlinkLabel.TabIndex = 15;
            this.SignInlinkLabel.TabStop = true;
            this.SignInlinkLabel.Text = "返回登录";
            this.SignInlinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.SignInlinkLabel_LinkClicked);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(30, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(264, 15);
            this.label8.TabIndex = 26;
            this.label8.Text = "规则：4-20字符，字母、数字、下划线";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(88, 369);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 15);
            this.label10.TabIndex = 14;
            this.label10.Text = "已有账号？→";
            // 
            // SignUp
            // 
            this.SignUp.Location = new System.Drawing.Point(102, 308);
            this.SignUp.Name = "SignUp";
            this.SignUp.Size = new System.Drawing.Size(131, 45);
            this.SignUp.TabIndex = 24;
            this.SignUp.Text = "注册";
            this.SignUp.UseVisualStyleBackColor = true;
            this.SignUp.Click += new System.EventHandler(this.SignUp_Click);
            // 
            // emailtextBox
            // 
            this.emailtextBox.Location = new System.Drawing.Point(145, 260);
            this.emailtextBox.Name = "emailtextBox";
            this.emailtextBox.Size = new System.Drawing.Size(134, 25);
            this.emailtextBox.TabIndex = 20;
            // 
            // checkpasswordtextBox
            // 
            this.checkpasswordtextBox.Location = new System.Drawing.Point(145, 202);
            this.checkpasswordtextBox.Name = "checkpasswordtextBox";
            this.checkpasswordtextBox.PasswordChar = '*';
            this.checkpasswordtextBox.Size = new System.Drawing.Size(134, 25);
            this.checkpasswordtextBox.TabIndex = 21;
            // 
            // passwordtextBox2
            // 
            this.passwordtextBox2.Location = new System.Drawing.Point(145, 129);
            this.passwordtextBox2.Name = "passwordtextBox2";
            this.passwordtextBox2.PasswordChar = '*';
            this.passwordtextBox2.Size = new System.Drawing.Size(134, 25);
            this.passwordtextBox2.TabIndex = 22;
            // 
            // usernametextBox2
            // 
            this.usernametextBox2.Location = new System.Drawing.Point(145, 47);
            this.usernametextBox2.Name = "usernametextBox2";
            this.usernametextBox2.Size = new System.Drawing.Size(134, 25);
            this.usernametextBox2.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(89, 263);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 15);
            this.label7.TabIndex = 16;
            this.label7.Text = "邮箱：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(51, 202);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 15);
            this.label6.TabIndex = 17;
            this.label6.Text = "确认密码*：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(81, 129);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 15);
            this.label5.TabIndex = 18;
            this.label5.Text = "密码*：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(66, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 15);
            this.label4.TabIndex = 19;
            this.label4.Text = "用户名*：";
            // 
            // RegisterControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label9);
            this.Controls.Add(this.SignInlinkLabel);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.SignUp);
            this.Controls.Add(this.emailtextBox);
            this.Controls.Add(this.checkpasswordtextBox);
            this.Controls.Add(this.passwordtextBox2);
            this.Controls.Add(this.usernametextBox2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Name = "RegisterControl";
            this.Size = new System.Drawing.Size(339, 430);
            this.Load += new System.EventHandler(this.RegisterControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.LinkLabel SignInlinkLabel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button SignUp;
        private System.Windows.Forms.TextBox emailtextBox;
        private System.Windows.Forms.TextBox checkpasswordtextBox;
        private System.Windows.Forms.TextBox passwordtextBox2;
        private System.Windows.Forms.TextBox usernametextBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
    }
}
