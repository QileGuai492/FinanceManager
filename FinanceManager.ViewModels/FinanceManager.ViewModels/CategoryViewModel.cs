using FinanceManager.Common;
using FinanceManager.Common.Constants;
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
    /// <summary>分类 ViewModel —— 管理支出/收入分类的加载、新增和删除</summary>
    public class CategoryViewModel : BaseViewModel
    {
        /// <summary>分类服务接口</summary>
        private readonly ICategoryService _categoryService;
        /// <summary>分类列表</summary>
        private ObservableCollection<CategoryEntity> _categories = new ObservableCollection<CategoryEntity>();
        /// <summary>新分类名称</summary>
        private string _newCategoryName = string.Empty;
        /// <summary>当前类型（0=支出,1=收入）</summary>
        private int _selectedType;
        /// <summary>加载状态</summary>
        private bool _isLoading;
        /// <summary>错误消息</summary>
        private string _errorMessage = string.Empty;

        public ObservableCollection<CategoryEntity> Categories
        {
            get => _categories;
            set => SetProperty(ref _categories, value);
        }

        public string NewCategoryName
        {
            get => _newCategoryName;
            set => SetProperty(ref _newCategoryName, value);
        }

        public int SelectedType
        {
            get => _selectedType;
            set => SetProperty(ref _selectedType, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoadCategoriesCommand { get; }
        public ICommand AddCategoryCommand { get; }
        public ICommand DeleteCategoryCommand { get; }

        public CategoryViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
            LoadCategoriesCommand = new RelayCommand<int>(async (userId) => await LoadCategoriesAsync(userId));
            AddCategoryCommand = new RelayCommand(async () => await AddCategoryAsync());
            DeleteCategoryCommand = new RelayCommand<int>(async (id) => await DeleteCategoryAsync(id));
        }

        private async Task LoadCategoriesAsync(int userId)
        {
            IsLoading = true;
            try
            {
                var categories = await _categoryService.GetCategoriesAsync(userId);
                Categories = new ObservableCollection<CategoryEntity>(categories.ToList());
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
            finally { IsLoading = false; }
        }

        private async Task AddCategoryAsync()
        {
            if (string.IsNullOrWhiteSpace(NewCategoryName))
            {
                ErrorMessage = "分类名称不能为空";
                return;
            }
            if (NewCategoryName.Length > AppConstants.MaxCategoryNameLength)
            {
                ErrorMessage = $"分类名称最长{AppConstants.MaxCategoryNameLength}个字符";
                return;
            }
            var count = await _categoryService.GetCustomCategoryCountAsync(App.CurrentUserId);
            if (count >= AppConstants.MaxCustomCategories)
            {
                ErrorMessage = $"自定义分类已达上限（{AppConstants.MaxCustomCategories}个）";
                return;
            }
            try
            {
                // SelectedType 是 int，Entity.Type 是 RecordType，显式转换
                var category = new CategoryEntity
                {
                    Name = NewCategoryName,
                    Type = (RecordType)SelectedType,
                    Icon = "custom",
                    Color = "#607D8B",
                    UserId = App.CurrentUserId
                };
                await _categoryService.AddCategoryAsync(category);
                await LoadCategoriesAsync(App.CurrentUserId);
                NewCategoryName = string.Empty;
                ErrorMessage = string.Empty;
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
        }

        private async Task DeleteCategoryAsync(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                await LoadCategoriesAsync(App.CurrentUserId);
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
        }
    }
}
