using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.ViewModels
{
    /// <summary>
    /// MVVM 基类 —— 实现 INotifyPropertyChanged，提供属性变更通知和简化的属性赋值方法。
    /// 所有 ViewModel 继承此类即可获得双向绑定能力。
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>属性变更事件，UI 层订阅此事件以响应 ViewModel 的数据变化</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 触发 PropertyChanged 事件，通知 UI 指定属性已变更。
        /// 使用 CallerMemberName 自动填充调用方属性名，无需手动传字符串。
        /// </summary>
        /// <param name="propertyName">变更的属性名（编译器自动填充）</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 设置属性值并自动触发变更通知。只有当新值与旧值不同时才更新和通知。
        /// </summary>
        /// <typeparam name="T">属性类型</typeparam>
        /// <param name="field">私有字段引用</param>
        /// <param name="value">新值</param>
        /// <param name="propertyName">属性名（编译器自动填充）</param>
        /// <returns>true 表示值已变更并已通知；false 表示新旧值相同，跳过</returns>
        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false; // 值未变，不触发通知
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
