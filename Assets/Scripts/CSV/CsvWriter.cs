using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace CSV
{
    public static class CsvWriter
    {
        private static char SPLIT_CHAR = ',';

        public static void Write(Dictionary<string, Dictionary<string, string>> data, string filePath)
        {
            StreamWriter writer = new StreamWriter(filePath);
            
            StringBuilder content = new StringBuilder();
            
            var rowEnumerator = data.GetEnumerator();

            if (rowEnumerator.MoveNext())
            {
                var columnEnumerator = rowEnumerator.Current.Value.GetEnumerator();
                
                while (columnEnumerator.MoveNext())
                {
                    content.Append(SPLIT_CHAR);
                    content.Append(columnEnumerator.Current.Key);
                }

                content.Append('\n');

                columnEnumerator.Dispose();

                while (rowEnumerator.MoveNext())
                {
                    content.Append(rowEnumerator.Current.Key);

                    columnEnumerator = rowEnumerator.Current.Value.GetEnumerator();

                    while (columnEnumerator.MoveNext())
                    {
                        content.Append(SPLIT_CHAR);

                        if (columnEnumerator.Current.Value.Contains(","))
                        {
                            content.Append('"');
                            content.Append(columnEnumerator.Current.Value.Replace("\n", "  "));
                            content.Append('"');
                        }
                        else
                        {
                            content.Append(columnEnumerator.Current.Value.Replace("\n", "  "));
                        }
                    }

                    content.Append('\n');
                    
                    columnEnumerator.Dispose();
                }
            }
            
            rowEnumerator.Dispose();
            
            writer.Write(content.ToString());
            writer.Close();
        }
    }
}