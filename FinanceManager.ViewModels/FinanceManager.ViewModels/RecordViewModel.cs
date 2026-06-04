using FinanceManager.Domain.Entities;
using FinanceManager.Domain.Enums;
using FinanceManager.Domain.Services;
using FinanceManager.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace FinanceManager.ViewModels
{
    /// <summary>
    /// 记账 ViewModel —— 管理记账记录的增删改查、筛选、金额类型等数据绑定。
    /// 提供 LoadRecords / AddRecord / UpdateRecord / DeleteRecord 异步命令。
    /// </summary>
    public class RecordViewModel : BaseViewModel
    {
        /// <summary>记账服务接口</summary>
        private readonly IRecordService _recordService;
        /// <summary>分类服务接口</summary>
        private readonly ICategoryService _categoryService;

        /// <summary>记账记录集合（绑定到 DataGridView）</summary>
        private ObservableCollection<RecordEntity> _records = new ObservableCollection<RecordEntity>();
        /// <summary>当前编辑的金额</summary>
        private decimal _amount;
        /// <summary>当前编辑的类型（支出/收入）</summary>
        private RecordType _type;
        /// <summary>当前选中的分类ID</summary>
        private int _selectedCategoryId;
        /// <summary>当前编辑的日期</summary>
        private DateTime _date = DateTime.Today;
        /// <summary>当前编辑的备注</summary>
        private string _note = string.Empty;
        /// <summary>是否正在加载数据</summary>
        private bool _isLoading;
        /// <summary>错误消息</summary>
        private string _errorMessage = string.Empty;

        /// <summary>记账记录列表（绑定到 UI）</summary>
        public ObservableCollection<RecordEntity> Records
        {
            get => _records;
            set => SetProperty(ref _records, value);
        }

        public decimal Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }

        public RecordType Type
        {
            get => _type;
            set => SetProperty(ref _type, value);
        }

        public int SelectedCategoryId
        {
            get => _selectedCategoryId;
            set => SetProperty(ref _selectedCategoryId, value);
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public string Note
        {
            get => _note;
            set => SetProperty(ref _note, value);
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

        public ICommand AddRecordCommand { get; }
        public ICommand DeleteRecordCommand { get; }
        public ICommand LoadRecordsCommand { get; }

        public RecordViewModel(IRecordService recordService, ICategoryService categoryService)
        {
            _recordService = recordService;
            _categoryService = categoryService;

            AddRecordCommand = new RelayCommand(async () => await AddRecordAsync());
            DeleteRecordCommand = new RelayCommand<int>(async (id) => await DeleteRecordAsync(id));
            LoadRecordsCommand = new RelayCommand<int>(async (userId) => await LoadRecordsAsync(userId));
        }

        public async Task LoadRecordsAsync(int userId)
        {
            IsLoading = true;
            try
            {
                var records = await _recordService.GetRecordsAsync(userId);
                Records = new ObservableCollection<RecordEntity>(records.ToList());
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task AddRecordAsync()
        {
            if (Amount <= 0)
            {
                ErrorMessage = "金额必须大于0";
                return;
            }

            var category = await _categoryService.GetCategoryByIdAsync(SelectedCategoryId);
            if (category == null)
            {
                ErrorMessage = "分类不存在";
                return;
            }

            // category.Type 是 RecordType，直接比较
            if (Type != category.Type)
            {
                ErrorMessage = "分类类型不匹配";
                return;
            }

            try
            {
                var record = new RecordEntity
                {
                    Amount = Type == RecordType.Expense ? -Amount : Amount,
                    Currency = "CNY",
                    Type = Type,
                    CategoryId = SelectedCategoryId,
                    Date = Date,
                    Note = Note,
                    UserId = App.CurrentUserId
                };

                await _recordService.AddRecordAsync(record);
                await LoadRecordsAsync(App.CurrentUserId);

                Amount = 0;
                Note = string.Empty;
                ErrorMessage = string.Empty;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async Task AddRecordAsync(RecordEntity record)
        {
            try
            {
                record.CreatedAt = DateTime.Now;
                record.UpdatedAt = DateTime.Now;
                await _recordService.AddRecordAsync(record);
                await LoadRecordsAsync(App.CurrentUserId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async Task UpdateRecordAsync(RecordEntity record)
        {
            try
            {
                await _recordService.UpdateRecordAsync(record);
                await LoadRecordsAsync(App.CurrentUserId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public async Task DeleteRecordAsync(int id)
        {
            try
            {
                await _recordService.DeleteRecordAsync(id);
                await LoadRecordsAsync(App.CurrentUserId);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }
    }
}
