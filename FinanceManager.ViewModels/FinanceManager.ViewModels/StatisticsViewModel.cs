using FinanceManager.Common;
using FinanceManager.Domain.models;
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
    /// 统计 ViewModel —— 管理月度/日度/年度/分类/趋势等维度统计数据加载。
    /// 提供 LoadMonthly / LoadDaily / LoadYearly / LoadCategory / LoadTrend 命令。
    /// </summary>
    public class StatisticsViewModel : BaseViewModel
    {
        /// <summary>统计服务接口</summary>
        private readonly IStatisticsService _statsService;

        /// <summary>月度统计数据</summary>
        private MonthlyStatistics _monthlyStats;
        /// <summary>日度统计数据集合</summary>
        private ObservableCollection<DailyStatistics> _dailyStats = new ObservableCollection<DailyStatistics>();
        /// <summary>年度统计数据集合</summary>
        private ObservableCollection<MonthlyStatistics> _yearlyStats = new ObservableCollection<MonthlyStatistics>();
        /// <summary>分类统计数据集合</summary>
        private ObservableCollection<CategoryStatistics> _categoryStats = new ObservableCollection<CategoryStatistics>();
        /// <summary>趋势数据集合</summary>
        private ObservableCollection<TrendData> _trendData = new ObservableCollection<TrendData>();

        /// <summary>选中的年份</summary>
        private int _selectedYear;
        /// <summary>选中的月份</summary>
        private int _selectedMonth;
        /// <summary>查询起始日期</summary>
        private DateTime _startDate = DateTime.Today;
        /// <summary>查询结束日期</summary>
        private DateTime _endDate = DateTime.Today;
        /// <summary>选中的收支类型（0=支出, 1=收入）</summary>
        private int _selectedType;
        /// <summary>加载状态</summary>
        private bool _isLoading;
        /// <summary>错误消息</summary>
        private string _errorMessage = string.Empty;

        public MonthlyStatistics MonthlyStats
        {
            get => _monthlyStats;
            set => SetProperty(ref _monthlyStats, value);
        }

        public ObservableCollection<DailyStatistics> DailyStats
        {
            get => _dailyStats;
            set => SetProperty(ref _dailyStats, value);
        }

        public ObservableCollection<MonthlyStatistics> YearlyStats
        {
            get => _yearlyStats;
            set => SetProperty(ref _yearlyStats, value);
        }

        public ObservableCollection<CategoryStatistics> CategoryStats
        {
            get => _categoryStats;
            set => SetProperty(ref _categoryStats, value);
        }

        public ObservableCollection<TrendData> TrendData
        {
            get => _trendData;
            set => SetProperty(ref _trendData, value);
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

        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
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

        public ICommand LoadMonthlyCommand { get; }
        public ICommand LoadDailyCommand { get; }
        public ICommand LoadYearlyCommand { get; }
        public ICommand LoadCategoryCommand { get; }
        public ICommand LoadTrendCommand { get; }

        public StatisticsViewModel(IStatisticsService statsService)
        {
            _statsService = statsService;
            var now = DateTime.Now;
            _selectedYear = now.Year;
            _selectedMonth = now.Month;

            LoadMonthlyCommand = new RelayCommand(async () => await LoadMonthlyAsync());
            LoadDailyCommand = new RelayCommand(async () => await LoadDailyAsync());
            LoadYearlyCommand = new RelayCommand(async () => await LoadYearlyAsync());
            LoadCategoryCommand = new RelayCommand(async () => await LoadCategoryAsync());
            LoadTrendCommand = new RelayCommand(async () => await LoadTrendAsync());
        }

        public async System.Threading.Tasks.Task LoadMonthlyAsync()
        {
            IsLoading = true;
            try
            {
                MonthlyStats = await _statsService.GetMonthlyStatisticsAsync(
                    App.CurrentUserId, SelectedYear, SelectedMonth, App.CurrentUserCurrency);
                ErrorMessage = string.Empty;
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
            finally { IsLoading = false; }
        }

        private async System.Threading.Tasks.Task LoadDailyAsync()
        {
            IsLoading = true;
            try
            {
                var data = await _statsService.GetDailyStatisticsAsync(
                    App.CurrentUserId, SelectedYear, SelectedMonth);
                DailyStats = new ObservableCollection<DailyStatistics>(data.ToList());
                ErrorMessage = string.Empty;
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
            finally { IsLoading = false; }
        }

        private async System.Threading.Tasks.Task LoadYearlyAsync()
        {
            IsLoading = true;
            try
            {
                var data = await _statsService.GetYearlyStatisticsAsync(
                    App.CurrentUserId, SelectedYear);
                YearlyStats = new ObservableCollection<MonthlyStatistics>(data.ToList());
                ErrorMessage = string.Empty;
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
            finally { IsLoading = false; }
        }

        private async System.Threading.Tasks.Task LoadCategoryAsync()
        {
            IsLoading = true;
            try
            {
                var data = await _statsService.GetCategoryStatisticsAsync(
                    App.CurrentUserId, SelectedType, StartDate, EndDate);
                CategoryStats = new ObservableCollection<CategoryStatistics>(data.ToList());
                ErrorMessage = string.Empty;
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
            finally { IsLoading = false; }
        }

        private async System.Threading.Tasks.Task LoadTrendAsync()
        {
            IsLoading = true;
            try
            {
                var data = await _statsService.GetTrendDataAsync(
                    App.CurrentUserId, StartDate, EndDate);
                TrendData = new ObservableCollection<TrendData>(data.ToList());
                ErrorMessage = string.Empty;
            }
            catch (Exception ex) { ErrorMessage = ex.Message; }
            finally { IsLoading = false; }
        }
    }
}
