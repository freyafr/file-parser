using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
                    rowsFound["ad_spend"] = double.Parse(rowsFound["ad_spend"].ToString(),CultureInfo.InvariantCulture)+
                        double.Parse(row["ad_spend"].ToString(),CultureInfo.InvariantCulture);
                }
                else
                {
                    _outputRows.Add(row);
                }
            }
        }

        public void WriteOutputFile(string outputFile)
        {
            if(_outputRows.Count>0)
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