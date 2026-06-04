using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Common.Constants
{
    public static class AppConstants
    {
        public const string AppName = "AI个人财务管理系统";
        public const string AppVersion = "1.0.0";
        public const string DatabaseFileName = "FinanceManager.sdf";
        public const string AppFolderName = "FinanceManager";
        public const string DefaultCurrency = "CNY";
        public const int MaxCategoryNameLength = 20;
        public const int MaxTemplateNameLength = 30;
        public const int MaxNoteLength = 500;
        public const int MaxUsernameLength = 20;
        public const int MinPasswordLength = 6;
        public const int MaxCustomCategories = 20;
        public const int MaxTemplates = 100;
        public const decimal AnomalyThresholdMultiplier = 3m;
    }
}
