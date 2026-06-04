using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Common.Helpers
{
    public static class CurrencyHelper
    {
        private static readonly Dictionary<string, string> Symbols = new Dictionary<string, string>
        {
            { "CNY", "¥" }, { "USD", "$" }, { "EUR", "€" },
            { "JPY", "¥" }, { "GBP", "£" }, { "HKD", "HK$" }
        };

        public static string GetSymbol(string currencyCode) =>
            Symbols.TryGetValue(currencyCode, out var s) ? s : currencyCode;

        public static string FormatAmount(decimal amount, string currencyCode) =>
            $"{GetSymbol(currencyCode)}{amount:N2}";
    }
}
