using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Domain.Enums
{
    /// <summary>
    /// 记录类型：收入/支出
    /// </summary>
    public enum RecordType
    {
        /// <summary>
        /// 支出
        /// </summary>
        Expense = 0,
        /// <summary>
        /// 收入
        /// </summary>
        Income = 1
    }
}
