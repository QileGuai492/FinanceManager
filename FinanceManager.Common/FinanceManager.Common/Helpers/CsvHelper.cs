using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.Common.Helpers
{
    public static class CsvHelper
    {
        public static string ToCsv(IEnumerable<string[]> rows)
        {
            var sb = new StringBuilder();
            foreach (var row in rows)
            {
                for (int i = 0; i < row.Length; i++)
                {
                    if (i > 0) sb.Append(',');
                    var cell = row[i] ?? string.Empty;
                    if (cell.Contains(",") || cell.Contains("\"") || cell.Contains("\n"))
                        sb.Append('"').Append(cell.Replace("\"", "\"\"")).Append('"');
                    else
                        sb.Append(cell);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public static List<string[]> ParseCsv(string csv)
        {
            var result = new List<string[]>();
            using (var reader = new StringReader(csv))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    result.Add(ParseLine(line));
                }
            }
            return result;
        }

        private static string[] ParseLine(string line)
        {
            var fields = new List<string>();
            var current = new StringBuilder();
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (inQuotes)
                {
                    if (c == '"' && i + 1 < line.Length && line[i + 1] == '"')
                    {
                        current.Append('"'); i++;
                    }
                    else if (c == '"')
                        inQuotes = false;
                    else
                        current.Append(c);
                }
                else
                {
                    if (c == '"') inQuotes = true;
                    else if (c == ',') { fields.Add(current.ToString()); current.Clear(); }
                    else current.Append(c);
                }
            }
            fields.Add(current.ToString());
            return fields.ToArray();
        }
    }
}
