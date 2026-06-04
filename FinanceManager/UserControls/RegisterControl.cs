using FinanceManager.Common;
using FinanceManager.Data.Repositories;
using FinanceManager.Data.Services;
using FinanceManager.Helpers;
using FinanceManager.ViewModels;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FinanceManager.UserControls
{
    public partial class RegisterControl : UserControl
    {
        /// <summary>切换回登录面板的事件</summary>
        public event Action SwitchToLogin;

        /// <summary>数据库连接字符串</summary>
        private readonly string _connStr;
        /// <summary>共享的 ViewModel（由 LoginForm 传入）</summary>
        private UserViewModel _viewModel;

        public RegisterControl(string connStr)
        {
            _connStr = connStr;
            InitializeComponent();
            SignUp.Click += SignUp_Click;
            SignInlinkLabel.LinkClicked += SignInlinkLabel_LinkClicked;
        }

        /// <summary>LoginForm 传入共享的 ViewModel</summary>
        public void Init(UserViewModel viewModel)
        {
            _viewModel = viewModel;
            this.BackColor = Color.White;
        }


        private void SignUp_Click(object sender, EventArgs e)
        {
            // 客户端快速校验
            if (string.IsNullOrWhiteSpace(usernametextBox2.Text))
            {
                MessageBox.Show("用户名不能为空");
                return;
            }
            if (string.IsNullOrWhiteSpace(passwordtextBox2.Text))
            {
                MessageBox.Show("密码不能为空");
                return;
            }
            if (passwordtextBox2.Text != checkpasswordtextBox.Text)
            {
                MessageBox.Show("两次输入的密码不一致");
                return;
            }

            // 交给 ViewModel
            _viewModel.Username = usernametextBox2.Text.Trim();
            _viewModel.Password = passwordtextBox2.Text;
            _viewModel.ConfirmPassword = checkpasswordtextBox.Text;
            _viewModel.RegisterCommand.Execute(null);
        }

        private void SignInlinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SwitchToLogin?.Invoke();
        }

        private void RegisterControl_Load(object sender, EventArgs e)
        {

        }
    }
}
