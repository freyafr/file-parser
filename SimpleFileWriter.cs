using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace NsnApp
{
    public class SimpleFileWriter : IFileWriter
    {
        private ICollection<DataRow> _outputRows = new List<DataRow>();
        public void PrepareOutputForSaving(ICollection<DataRow> outputRows)
        {
            foreach(DataRow row in outputRows)
            {
                var rowsFound = _outputRows.Where(r=>r["advertiser"].ToString()==row["advertiser"].ToString()).FirstOrDefault();
                if (rowsFound!=null)
                {
                    rowsFound["ad_spend"] = Convert.ToDouble(rowsFound["ad_spend"])+Convert.ToDouble(row["ad_spend"]);
                }
                else
                {
                    _outputRows.Add(row);
                }
            }
        }

        public void WriteOutputFile(string outputFile)
        {
            using(var writer = new StreamWriter(outputFile,false,Encoding.UTF8))
            {
                var headerstring = string.Join(',',_outputRows.First().Table.Columns.Cast<DataColumn>().ToArray().Select(s=>s.ColumnName));
                        writer.WriteLine(headerstring);
                        writer.Flush();

                foreach (DataRow row in _outputRows)
                {
                    var outputRow = string.Join(",", row.ItemArray.Select(item=>string.Format("\"{0}\"",item)));
                    writer.WriteLine(outputRow);
                    writer.Flush();
                }
            }
        }
    }
}