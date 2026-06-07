using FinanceManager.Common;
using FinanceManager.Common.Constants;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.ViewModels
{
    /// <summary>
    /// 用户认证 ViewModel —— 处理登录、注册、登出的业务逻辑和状态管理。
    /// 通过 ICommand 暴露给 UI 层，通过 INotifyPropertyChanged 回传状态变更。
    /// </summary>
    public class UserViewModel : BaseViewModel
    {
        /// <summary>用户服务接口（依赖注入）</summary>
        private readonly IUserService _userService;

        // ===== 绑定属性（通过 SetProperty 自动通知 UI）=====

        /// <summary>用户名输入</summary>
        private string _username = string.Empty;
        /// <summary>密码输入</summary>
        private string _password = string.Empty;
        /// <summary>确认密码输入（注册用）</summary>
        private string _confirmPassword = string.Empty;
        /// <summary>是否正在执行异步操作</summary>
        private bool _isLoading;
        /// <summary>错误消息文本</summary>
        private string _errorMessage = string.Empty;
        /// <summary>是否已登录成功</summary>
        private bool _isLoggedIn;

        /// <summary>用户名（双向绑定）</summary>
        public string Username { get => _username; set => SetProperty(ref _username, value); }
        /// <summary>密码（双向绑定）</summary>
        public string Password { get => _password; set => SetProperty(ref _password, value); }
        /// <summary>确认密码（双向绑定）</summary>
        public string ConfirmPassword { get => _confirmPassword; set => SetProperty(ref _confirmPassword, value); }
        /// <summary>加载状态（双向绑定）</summary>
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value); }
        /// <summary>错误消息（双向绑定）</summary>
        public string ErrorMessage { get => _errorMessage; set => SetProperty(ref _errorMessage, value); }
        /// <summary>登录状态（双向绑定），UI 监听此属性进行跳转</summary>
        public bool IsLoggedIn { get => _isLoggedIn; set => SetProperty(ref _isLoggedIn, value); }

        /// <summary>登录命令</summary>
        public ICommand LoginCommand { get; }
        /// <summary>注册命令</summary>
        public ICommand RegisterCommand { get; }
        /// <summary>登出命令</summary>
        public ICommand LogoutCommand { get; }

        /// <summary>构造函数：接收 IUserService 依赖注入，初始化三个命令</summary>
        public UserViewModel(IUserService userService)
        {
            _userService = userService;
            LoginCommand = new RelayCommand(async () => await LoginAsync());
            RegisterCommand = new RelayCommand(async () => await RegisterAsync());
            LogoutCommand = new RelayCommand(() => Logout());
        }

        /// <summary>执行登录：空值校验 → 调用 UserService.LoginAsync → 设置登录状态</summary>
        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "用户名和密码不能为空";
                return;
            }
            IsLoading = true;
            try
            {
                var user = await _userService.LoginAsync(Username, Password);
                if (user != null)
                {
                    App.CurrentUserId = user.Id;
                    App.CurrentUsername = user.Username;
                    App.CurrentUserCurrency = user.Currency ?? "CNY";
                    IsLoggedIn = true;
                    ErrorMessage = string.Empty;
                }
                else
                {
                    ErrorMessage = "用户名或密码错误";
                }
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
            finally { IsLoading = false; }
        }

        /// <summary>执行注册：多层校验（空/长度/不一致/重名）→ 调用 UserService.RegisterUserAsync → 自动登录</summary>
        private async Task RegisterAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "用户名和密码不能为空";
                return;
            }
            if (Username.Length > AppConstants.MaxUsernameLength)
            {
                ErrorMessage = $"用户名最长{AppConstants.MaxUsernameLength}个字符";
                return;
            }
            if (Password.Length < AppConstants.MinPasswordLength)
            {
                ErrorMessage = $"密码至少{AppConstants.MinPasswordLength}个字符";
                return;
            }
            if (Password != ConfirmPassword)
            {
                ErrorMessage = "两次输入的密码不一致";
                return;
            }
            IsLoading = true;
            try
            {
                var existing = await _userService.GetUserByUsernameAsync(Username);
                if (existing != null)
                {
                    ErrorMessage = "用户名已存在";
                    return;
                }
                var user = new UserEntity { Username = Username, Password = Password };
                await _userService.RegisterUserAsync(user);
                ErrorMessage = string.Empty;
                await LoginAsync();
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
            finally { IsLoading = false; }
        }

        /// <summary>执行登出：清理全局状态，设置 IsLoggedIn=false 通知 UI 返回登录页</summary>
        private void Logout()
        {
            App.Logout();           // 清除 CurrentUserId 和 CurrentUsername
            IsLoggedIn = false;     // 触发 PropertyChanged，LoginForm 监听到后关闭 MainForm
        }
    }
}
