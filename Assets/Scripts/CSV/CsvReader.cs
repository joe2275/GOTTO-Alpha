using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CSV
{
    public static class CsvReader
    {
        private static string SPLIT = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        private static string LINE_SPLIT = @"\r\n|\n\r|\n|\r";
        private static char[] TRIM_CHARS = { '\"' };

        public static Dictionary<string, Dictionary<string, string>> Read(string csv)
        {
            var data = new Dictionary<string, Dictionary<string, string>>();

            string[] lines = Regex.Split(csv, LINE_SPLIT);

            if (lines.Length <= 1)
            {
                return data;
            }

            string[] headers = Regex.Split(lines[0], SPLIT);

            for (int row = 1; row < lines.Length; row++)
            {
                string[] values = Regex.Split(lines[row], SPLIT);
                if (values.Length == 0 || values[0] == "")
                {
                    continue;
                }

                var entry = new Dictionary<string, string>();

                for (int column = 1; column < headers.Length && column < values.Length; column++)
                {
                    string value = values[column].TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "")
                        .Replace("  ", "\n");
                    entry.Add(headers[column], value);
                }
                
                data.Add(values[0], entry);
            }

            return data;
        }
    }
}