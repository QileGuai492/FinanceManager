using FinanceManager.Common;
using FinanceManager.Domain.Entities;
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
    /// <summary>预算 ViewModel —— 管理月预算和分类预算的加载、保存和进度计算</summary>
    public class BudgetViewModel : BaseViewModel
    {
        /// <summary>预算服务接口</summary>
        private readonly IBudgetService _budgetService;
        /// <summary>预算列表</summary>
        private ObservableCollection<BudgetEntity> _budgets = new ObservableCollection<BudgetEntity>();
        /// <summary>分类预算列表</summary>
        private ObservableCollection<CategoryBudgetEntity> _categoryBudgets = new ObservableCollection<CategoryBudgetEntity>();
        /// <summary>总预算金额</summary>
        private decimal _totalBudgetAmount;
        /// <summary>选中年份</summary>
        private int _selectedYear;
        /// <summary>选中月份</summary>
        private int _selectedMonth;
        /// <summary>加载状态</summary>
        private bool _isLoading;
        /// <summary>错误消息</summary>
        private string _errorMessage = string.Empty;

        public ObservableCollection<BudgetEntity> Budgets
        {
            get => _budgets;
            set => SetProperty(ref _budgets, value);
        }

        public ObservableCollection<CategoryBudgetEntity> CategoryBudgets
        {
            get => _categoryBudgets;
            set => SetProperty(ref _categoryBudgets, value);
        }

        public decimal TotalBudgetAmount
        {
            get => _totalBudgetAmount;
            set => SetProperty(ref _totalBudgetAmount, value);
        }

        public int SelectedYear
        {
            get => _selectedYear;
            set => SetProperty(ref _selectedYear, value);
        }

        public int SelectedMonth
        {
            get => _selectedMonth;
            set => SetProperty(ref _selectedMonth, value);
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

        public ICommand SaveBudgetCommand { get; }
        public ICommand LoadBudgetsCommand { get; }
        public ICommand LoadCategoryBudgetsCommand { get; }

        public BudgetViewModel(IBudgetService budgetService)
        {
            _budgetService = budgetService;
            var now = DateTime.Now;
            _selectedYear = now.Year;
            _selectedMonth = now.Month;

            SaveBudgetCommand = new RelayCommand(async () => await SaveBudgetAsync());
            LoadBudgetsCommand = new RelayCommand<int>(async (userId) => await LoadBudgetsAsync(userId));
            LoadCategoryBudgetsCommand = new RelayCommand(async () => await LoadCategoryBudgetsAsync());
        }

        private async Task SaveBudgetAsync()
        {
            if (TotalBudgetAmount <= 0)
            {
                ErrorMessage = "预算金额必须大于0";
                return;
            }
            try
            {
                var existing = await _budgetService.GetBudgetByYearMonthAsync(
                    App.CurrentUserId, SelectedYear, SelectedMonth);
                if (existing != null)
                {
                    existing.Amount = TotalBudgetAmount;
                    await _budgetService.UpdateBudgetAsync(existing);
                }
                else
                {
                    await _budgetService.AddBudgetAsync(new BudgetEntity
                    {
                        Amount = TotalBudgetAmount,
                        Month = SelectedMonth,
                        Year = SelectedYear,
                        UserId = App.CurrentUserId
                    });
                }
                await LoadBudgetsAsync(App.CurrentUserId);
                ErrorMessage = string.Empty;
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
        }

        private async Task LoadBudgetsAsync(int userId)
        {
            IsLoading = true;
            try
            {
                var budgets = await _budgetService.GetBudgetsAsync(userId);
                Budgets = new ObservableCollection<BudgetEntity>(budgets.ToList());
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
            finally { IsLoading = false; }
        }

        private async Task LoadCategoryBudgetsAsync()
        {
            try
            {
                var cbList = await _budgetService.GetCategoryBudgetsAsync(
                    App.CurrentUserId, SelectedYear, SelectedMonth);
                CategoryBudgets = new ObservableCollection<CategoryBudgetEntity>(cbList.ToList());
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
        }
    }
}
