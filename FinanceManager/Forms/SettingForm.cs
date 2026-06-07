using FinanceManager.Common;
using FinanceManager.Common.Helpers;
using FinanceManager.Data.Repositories;
using FinanceManager.Data.Services;
using FinanceManager.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinanceManager.Forms
{
    /// <summary>
    /// 个人设置弹窗 —— 修改用户名、修改密码、AI功能开关、AI API 配置。
    /// 所有修改直接写入数据库，关闭设置页后主窗体通过 RefreshDashboard 同步。
    /// </summary>
    public partial class SettingForm : Form
    {
        /// <summary>数据库连接字符串</summary>
        private readonly string _connStr;

        /// <summary>构造函数：接收连接字符串（由 MainForm 传入）</summary>
        public SettingForm(string connStr)
        {
            _connStr = connStr;
            InitializeComponent();
        }

        /// <summary>窗体加载：设置背景色 → 加载当前用户名 → 加载用户设置和 AI 配置</summary>
        private async void SettingForm_Load(object sender, EventArgs e)
        {
            this.Text = "个人设置";
            this.BackColor = UiHelper.BgLight;
            this.StartPosition = FormStartPosition.CenterParent;
            labelStatus.Visible = false;
            textBoxUsername.Text = App.CurrentUsername;

            comboBoxMoney.Items.AddRange(new[] { "CNY", "USD", "EUR", "JPY", "GBP", "HKD" });

            LoadUserSettings();   // 加载 AI 开关状态和默认货币
            LoadApiConfig();      // 加载 API 配置
        }

        /// <summary>加载用户设置：从数据库读取 AI 建议开关状态并同步到 CheckBox</summary>
        private async void LoadUserSettings()
        {
            var userService = new UserService(new UserRepository(_connStr));
            var user = await userService.GetUserByIdAsync(App.CurrentUserId);
            if (user != null)
            {
                checkBoxAI.CheckedChanged -= checkBoxAI_CheckedChanged;
                checkBoxAI.Checked = user.AiSuggestionEnabled;
                checkBoxAI.CheckedChanged += checkBoxAI_CheckedChanged;

                comboBoxMoney.SelectedIndexChanged -= comboBoxMoney_SelectedIndexChanged;
                comboBoxMoney.SelectedItem = user.Currency ?? "CNY";
                comboBoxMoney.SelectedIndexChanged += comboBoxMoney_SelectedIndexChanged;
                App.CurrentUserCurrency = user.Currency ?? "CNY";
            }
        }

        /// <summary>加载 AI 配置：从本地配置文件读取 Endpoint、API Key、Model</summary>
        private void LoadApiConfig()
        {
            var config = AiConfig.Load();
            textBoxEndPoint.Text = config.Endpoint;
            textBoxAPIKey.Text = config.ApiKey;
            textBoxAIModel.Text = string.IsNullOrEmpty(config.Model)
                ? "deepseek-chat" : config.Model;  // 默认使用 deepseek-chat 模型
        }

        /// <summary>保存用户名：校验非空、无变化 → 更新数据库和全局状态</summary>
        private async void buttonSaveName_Click(object sender, EventArgs e)
        {
            var newName = textBoxUsername.Text.Trim();
            if (string.IsNullOrWhiteSpace(newName))
            {
                labelStatus.Text = "用户名不能为空";
                labelStatus.ForeColor = Color.Red;
                labelStatus.Visible = true; return;
            }
            if (newName == App.CurrentUsername)
            {
                labelStatus.Text = "用户名未改动";
                labelStatus.ForeColor = Color.Gray;
                labelStatus.Visible = true; return;
            }

            var userService = new UserService(new UserRepository(_connStr));
            var user = await userService.GetUserByIdAsync(App.CurrentUserId);
            user.Username = newName;
            await userService.UpdateUserAsync(user);

            App.CurrentUsername = newName;
            labelStatus.Text = "用户名修改成功";
            labelStatus.Visible = true;
            labelStatus.ForeColor = Color.Green;
            MessageBox.Show("用户名保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>AI 开关切换：勾选/取消时立即调用仓储更新数据库</summary>
        private async void checkBoxAI_CheckedChanged(object sender, EventArgs e)
        {
            var userService = new UserService(new UserRepository(_connStr));
            await userService.UpdateAiSuggestionEnabledAsync(
                App.CurrentUserId, checkBoxAI.Checked);
        }

        /// <summary>
        /// 修改密码：校验原密码 → 校验新密码长度≥6 → 校验两次一致 →
        /// 验证原密码正确性 → 加密新密码并写入数据库
        /// </summary>
        private async void buttonCheck_Click(object sender, EventArgs e)
        {
            var oldPwd = textBoxOrgin.Text;       // 原密码
            var newPwd = textBoxNew.Text;          // 新密码
            var confirmPwd = textBoxCheck.Text;    // 确认新密码

            // 第1步：非空校验
            if (string.IsNullOrWhiteSpace(oldPwd)
                || string.IsNullOrWhiteSpace(newPwd)
                || string.IsNullOrWhiteSpace(confirmPwd))
            {
                labelStatus.Text = "请填写所有密码字段";
                labelStatus.ForeColor = Color.Red;
                labelStatus.Visible = true; return;
            }

            // 第2步：长度校验（≥6位）
            if (newPwd.Length < 6)
            {
                labelStatus.Text = "新密码至少6位";
                labelStatus.ForeColor = Color.Red;
                labelStatus.Visible = true; return;
            }

            // 第3步：一致性校验
            if (newPwd != confirmPwd)
            {
                labelStatus.Text = "两次输入的新密码不一致";
                labelStatus.ForeColor = Color.Red;
                labelStatus.Visible = true; return;
            }

            var userService = new UserService(new UserRepository(_connStr));

            // 第4步：验证原密码
            var valid = await userService.ValidatePasswordAsync(
                App.CurrentUsername, oldPwd);
            if (!valid)
            {
                labelStatus.Text = "原密码错误";
                labelStatus.ForeColor = Color.Red;
                labelStatus.Visible = true; return;
            }

            // 第5步：加密新密码并写入数据库
            var user = await userService.GetUserByIdAsync(App.CurrentUserId);
            user.Password = EncryptionHelper.HashPassword(newPwd);  // BCrypt 哈希
            await userService.UpdateUserAsync(user);

            labelStatus.Text = "密码修改成功";
            labelStatus.Visible = true;
            labelStatus.ForeColor = Color.Green;
            textBoxOrgin.Clear();
            textBoxNew.Clear();
            textBoxCheck.Clear();
            MessageBox.Show("密码修改成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // 链接点击事件：打开 DeepSeek 官网
        private void linkLabelDeepSeek_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://api-docs.deepseek.com/");
        }

        // 保存 AI 配置
        private void buttonAISettings_Click(object sender, EventArgs e)
        {
            var config = new AiConfig
            {
                Endpoint = textBoxEndPoint.Text.Trim(),
                ApiKey = textBoxAPIKey.Text.Trim(),
                Model = textBoxAIModel.Text.Trim()
            };
            config.Save();
            MessageBox.Show("AI配置保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ====== 设置页用户体验优化 =====

        // 在用户名输入框按回车键触发保存操作，避免频繁点击按钮
        private void textBoxUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                buttonSaveName_Click(sender, e);
            }
            else return;
        }

        private void textBoxOrgin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                textBoxNew.Focus();
            }
            else return;
        }

        private void textBoxNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                textBoxCheck.Focus();
            }
            else return;
        }

        private void textBoxCheck_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                buttonCheck_Click(sender, e);
            }
            else return;
        }

        private void textBoxAPIKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                buttonAISettings_Click(sender, e);
            }
            else return;
        }

        /// <summary>默认货币切换：立即写入数据库并更新全局状态</summary>
        private async void comboBoxMoney_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxMoney.SelectedItem == null) return;
            var currency = comboBoxMoney.SelectedItem.ToString();
            if (currency == App.CurrentUserCurrency) return;

            var userService = new UserService(new UserRepository(_connStr));
            var user = await userService.GetUserByIdAsync(App.CurrentUserId);
            if (user != null)
            {
                user.Currency = currency;
                await userService.UpdateUserAsync(user);
                App.CurrentUserCurrency = currency;
            }
        }
    }
}
