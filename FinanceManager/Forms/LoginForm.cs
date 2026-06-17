using FinanceManager.Common;
using FinanceManager.Data.Database;
using FinanceManager.Data.Repositories;
using FinanceManager.Data.Services;
using FinanceManager.Helpers;
using FinanceManager.UserControls;
using FinanceManager.ViewModels;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FinanceManager.Forms
{
    public partial class LoginForm : Form
    {
        private Control _currentPanel;
        private readonly UserViewModel _viewModel;
        private readonly string _connStr;
        private RegisterControl rgControl;

        public LoginForm()
        {
            _connStr = DatabaseManager.Instance.ConnectionString;
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            _viewModel = new UserViewModel(new UserService(new UserRepository(_connStr)));
            this.FormClosed += (s, e) => Application.Exit();
            BindViewModel();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            ApplyStyles();
            ShowPanel(panelLogin);
            textBoxUserName.Focus();

            // 创建注册面板
            rgControl = new RegisterControl(_connStr);
            rgControl.Init(_viewModel);
            rgControl.Dock = DockStyle.Fill;
            rgControl.Visible = false;
            panelContainer.Controls.Add(rgControl);
            rgControl.SwitchToLogin += () =>
            {
                ShowPanel(panelLogin);
                labelerror.Visible = false;
                this.Text = "AI个人财务管理系统 - 登录";
                Title.Text = "欢迎回来，请登录";
            };
        }

        private void ApplyStyles()
        {
            this.BackColor = UiHelper.BgLight;
            this.StartPosition = FormStartPosition.CenterScreen;

            Title.Font = new Font("微软雅黑", 16f, FontStyle.Bold);
            Title.ForeColor = UiHelper.DeepBlue;

            panelLogin.BackColor = UiHelper.CardWhite;
            UiHelper.MakeRound(panelLogin, 8, UiHelper.CardWhite);

            UiHelper.StyleButton(buttonLogin, UiHelper.DeepBlue, Color.White, 40);
            UiHelper.BindHover(buttonLogin, UiHelper.DeepBlue, UiHelper.LightBlue);

            linkLabelSignup.Font = new Font("微软雅黑", 9f);
            linkLabelSignup.LinkColor = UiHelper.DeepBlue;

            // 输入框美化
            UiHelper.StyleTextBox(textBoxUserName);
            UiHelper.StyleTextBox(textBox1);
            // 密码框回车触发登录
            textBox1.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter) buttonLogin_Click(s, ev);
            };
            // 用户框回车跳到密码框
            textBoxUserName.KeyDown += (s, ev) =>
            {
                if (ev.KeyCode == Keys.Enter) { ev.SuppressKeyPress = true; textBox1.Focus(); }
            };

            labelerror.Visible = false;
        }

        private void BindViewModel()
        {
            _viewModel.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == nameof(_viewModel.IsLoggedIn) && _viewModel.IsLoggedIn)
                {
                    if (checkBoxRemember.Checked)
                        Program.SaveUserId(App.CurrentUserId);
                    var mainform = new MainForm();
                    MainForm.OwnerLoginForm = this;
                    mainform.Show();
                    this.Hide();
                }

                if (e.PropertyName == nameof(_viewModel.ErrorMessage))
                {
                    labelerror.Text = _viewModel.ErrorMessage;
                    labelerror.Visible = !string.IsNullOrEmpty(_viewModel.ErrorMessage);
                }
            };
        }

        private void ShowPanel(Control panel)
        {
            if (_currentPanel != null)
                _currentPanel.Visible = false;
            panel.Visible = true;
            _currentPanel = panel;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            _viewModel.Username = textBoxUserName.Text.Trim();
            _viewModel.Password = textBox1.Text;
            _viewModel.LoginCommand.Execute(null);
        }

        private void linkLabelSignup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowPanel(rgControl);
            labelerror.Visible = false;
            this.Text = "AI个人财务管理系统 - 注册";
            Title.Text = "注册新账号";
        }

        private void checkBoxRemember_CheckedChanged(object sender, EventArgs e) { }
    }
}
