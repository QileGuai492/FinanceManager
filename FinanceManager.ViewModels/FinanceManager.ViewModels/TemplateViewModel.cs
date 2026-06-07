using FinanceManager.Common;
using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
using FinanceManager.Domain.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FinanceManager.ViewModels
{
    /// <summary>
    /// 模板 ViewModel —— 管理记账模板的增删改查、收藏标记、使用计数。
    /// 通过 OnTemplateSelected 事件将被选模板数据回传给 UI 层用于跳转记账。
    /// </summary>
    public class TemplateViewModel : BaseViewModel
    {
        /// <summary>模板服务接口</summary>
        private readonly ITemplateService _templateService;

        /// <summary>模板列表集合</summary>
        private ObservableCollection<TemplateEntity> _templates = new ObservableCollection<TemplateEntity>();
        /// <summary>模板名称</summary>
        private string _templateName = string.Empty;
        /// <summary>默认金额</summary>
        private decimal _defaultAmount;
        /// <summary>模板类型（0=支出,1=收入）</summary>
        private int _selectedType;
        /// <summary>关联的分类ID</summary>
        private int _selectedCategoryId;
        /// <summary>备注模板</summary>
        private string _noteTemplate = string.Empty;
        /// <summary>是否标记为常用</summary>
        private bool _isFavorite;
        /// <summary>加载状态</summary>
        private bool _isLoading;
        /// <summary>是否只看常用模板</summary>
        private bool _showFavoritesOnly;
        /// <summary>错误消息</summary>
        private string _errorMessage = string.Empty;

        public ObservableCollection<TemplateEntity> Templates
        {
            get => _templates;
            set => SetProperty(ref _templates, value);
        }

        public string TemplateName
        {
            get => _templateName;
            set => SetProperty(ref _templateName, value);
        }

        public decimal DefaultAmount
        {
            get => _defaultAmount;
            set => SetProperty(ref _defaultAmount, value);
        }

        public int SelectedType
        {
            get => _selectedType;
            set => SetProperty(ref _selectedType, value);
        }

        public int SelectedCategoryId
        {
            get => _selectedCategoryId;
            set => SetProperty(ref _selectedCategoryId, value);
        }

        public string NoteTemplate
        {
            get => _noteTemplate;
            set => SetProperty(ref _noteTemplate, value);
        }

        public bool IsFavorite
        {
            get => _isFavorite;
            set => SetProperty(ref _isFavorite, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public bool ShowFavoritesOnly
        {
            get => _showFavoritesOnly;
            set
            {
                if (SetProperty(ref _showFavoritesOnly, value))
                    LoadTemplatesCommand.Execute(App.CurrentUserId);
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoadTemplatesCommand { get; }
        public ICommand AddTemplateCommand { get; }
        public ICommand UpdateTemplateCommand { get; }
        public ICommand DeleteTemplateCommand { get; }
        public ICommand ToggleFavoriteCommand { get; }
        public ICommand UseTemplateCommand { get; }

        public TemplateViewModel(ITemplateService templateService)
        {
            _templateService = templateService;

            LoadTemplatesCommand = new RelayCommand<int>(async (userId) => await LoadTemplatesAsync(userId));
            AddTemplateCommand = new RelayCommand(async () => await AddTemplateAsync());
            UpdateTemplateCommand = new RelayCommand<TemplateEntity>(async (t) => await UpdateTemplateAsync(t));
            DeleteTemplateCommand = new RelayCommand<int>(async (id) => await DeleteTemplateAsync(id));
            ToggleFavoriteCommand = new RelayCommand<TemplateEntity>(async (t) => await ToggleFavoriteAsync(t));
            UseTemplateCommand = new RelayCommand<TemplateEntity>((t) => UseTemplate(t));
        }

        private async System.Threading.Tasks.Task LoadTemplatesAsync(int userId)
        {
            IsLoading = true;
            try
            {
                var templates = ShowFavoritesOnly
                    ? await _templateService.GetFavoriteTemplatesAsync(userId)
                    : await _templateService.GetTemplatesAsync(userId);
                Templates = new ObservableCollection<TemplateEntity>(templates.ToList());
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
            finally { IsLoading = false; }
        }

        private async System.Threading.Tasks.Task AddTemplateAsync()
        {
            if (string.IsNullOrWhiteSpace(TemplateName))
            {
                ErrorMessage = "模板名称不能为空";
                return;
            }
            if (SelectedCategoryId <= 0)
            {
                ErrorMessage = "请选择分类";
                return;
            }

            try
            {
                var template = new TemplateEntity
                {
                    Name = TemplateName,
                    DefaultAmount = DefaultAmount,
                    Currency = App.CurrentUserCurrency,
                    Type = (RecordType)SelectedType,
                    CategoryId = SelectedCategoryId,
                    NoteTemplate = NoteTemplate,
                    IsFavorite = IsFavorite,
                    UserId = App.CurrentUserId
                };

                await _templateService.AddTemplateAsync(template);
                await LoadTemplatesAsync(App.CurrentUserId);

                TemplateName = string.Empty;
                DefaultAmount = 0;
                NoteTemplate = string.Empty;
                IsFavorite = false;
                ErrorMessage = string.Empty;
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
        }

        private async System.Threading.Tasks.Task UpdateTemplateAsync(TemplateEntity template)
        {
            try
            {
                await _templateService.UpdateTemplateAsync(template);
                await LoadTemplatesAsync(App.CurrentUserId);
                ErrorMessage = string.Empty;
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
        }

        private async System.Threading.Tasks.Task DeleteTemplateAsync(int id)
        {
            try
            {
                await _templateService.DeleteTemplateAsync(id);
                await LoadTemplatesAsync(App.CurrentUserId);
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
        }

        private async System.Threading.Tasks.Task ToggleFavoriteAsync(TemplateEntity template)
        {
            try
            {
                template.IsFavorite = !template.IsFavorite;
                await _templateService.UpdateTemplateAsync(template);
                await LoadTemplatesAsync(App.CurrentUserId);
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
        }

        // 使用模板：只是触发事件，由 Form 层监听后跳转到记账页并预填数据
        // 同步方式跳转记账页，预填数据
        private void UseTemplate(TemplateEntity template)
        {
            if (template == null) return;
            OnTemplateSelected?.Invoke(this, template);
        }

        // 模板选中事件，Form 层订阅
        public event EventHandler<TemplateEntity> OnTemplateSelected;
    }
}
